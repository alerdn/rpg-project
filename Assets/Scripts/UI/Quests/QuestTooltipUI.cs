using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Quests;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TMP_Text title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;

        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();

            ClearObjectives();
            foreach (string objective in quest.GetObjectives())
            {
                GameObject prefab = status.IsObjectiveComplete(objective) ?
                    objectivePrefab :
                    objectiveIncompletePrefab;

                TMP_Text objectiveText = Instantiate(prefab, objectiveContainer).GetComponentInChildren<TMP_Text>();
                objectiveText.text = objective;
            }
        }

        private void ClearObjectives()
        {
            foreach (Transform child in objectiveContainer)
            {
                Destroy(child.gameObject);
            }
        }
    }
}