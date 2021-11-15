using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest _quest;
    [SerializeField]
    private Player _player;
    [SerializeField]
    private GameObject _questWindow;
    [SerializeField]
    private Text _titleText;
    [SerializeField]
    private Text _descriptionText;

    private void OpenQuestWindow()
    {
        _questWindow.SetActive(true);
        _titleText.text = _quest.title;
        _descriptionText.text = _quest.description;
    }
    
    private void CloseQuestWindow()
    {
        _questWindow.SetActive(false);
    }
    
    public void AcceptQuest()
    {
        _questWindow.SetActive(false);
        _quest.isActive = true;
        _player.quest = _quest;
        _quest.goal.currentAmount = 1;
    }

    private void OnTriggerEnter(Collider other)
    {
        OpenQuestWindow();
    }
    
    private void OnTriggerExit(Collider other)
    {
        CloseQuestWindow();
    }
}