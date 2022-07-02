using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestListUI : MonoBehaviour
    {
        [SerializeField] QuestItemUI questPrefab;
        QuestList questList;

        // Start is called before the first frame update
        void Start()
        {
            questList = GameObject.FindGameObjectWithTag("Player").GetComponent<QuestList>();
            questList.OnQuestListUpdated += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            ClearChildren();
            foreach (QuestStatus status in questList.GetStatuses())
            {
                QuestItemUI questItem = Instantiate(questPrefab, transform).GetComponent<QuestItemUI>();
                questItem.Setup(status);
            }
        }

        private void ClearChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}