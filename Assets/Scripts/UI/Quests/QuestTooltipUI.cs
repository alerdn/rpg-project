using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Quests;
using System;

namespace RPG.UI.Quests
{
    public class QuestTooltipUI : MonoBehaviour
    {
        [SerializeField] TMP_Text title;
        [SerializeField] Transform objectiveContainer;
        [SerializeField] GameObject objectivePrefab;
        [SerializeField] GameObject objectiveIncompletePrefab;
        [SerializeField] TMP_Text rewardText;

        public void Setup(QuestStatus status)
        {
            Quest quest = status.GetQuest();
            title.text = quest.GetTitle();

            ClearObjectives();
            foreach (var objective in quest.GetObjectives())
            {
                GameObject prefab = status.IsObjectiveComplete(objective.reference) ?
                    objectivePrefab :
                    objectiveIncompletePrefab;

                TMP_Text objectiveText = Instantiate(prefab, objectiveContainer).GetComponentInChildren<TMP_Text>();
                objectiveText.text = objective.descriptoin;
            }
            rewardText.text = GetRewardText(quest);
        }

        private string GetRewardText(Quest quest)
        {
            string rewardText = "";
            foreach (var reward in quest.GetRewards())
            {
                if (rewardText != "")
                {
                    rewardText += ", ";
                }
                if (reward.number > 1)
                {
                    rewardText += reward.number + " ";
                }
                rewardText += reward.item.GetDisplayName();
            }
            if (rewardText == "")
            {
                rewardText = "No reward";
            }

            rewardText += ".";
            return rewardText;
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