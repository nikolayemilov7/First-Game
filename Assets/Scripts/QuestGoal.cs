using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public Quest quest;
    public int requiredAmount;
    public int currentAmount;

    public bool isReached()
    {
        return (currentAmount >= requiredAmount);
    }

    /*public void EnemyKilled()
    {
        if (goalType == GoalType.Kill)
            currentAmount++;
    }
    
    public void EnemyCollected()
    {
        if (goalType == GoalType.Collect)
            currentAmount++;
    }*/
}
public enum GoalType
{
    Kill,
    Collect
}
