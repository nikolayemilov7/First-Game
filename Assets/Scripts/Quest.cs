using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Quest
{
    public QuestGoal goal;

    public string title;
    public string description;
    public bool isActive;


    public void Complete()
    {
        isActive = false;
    }

}
