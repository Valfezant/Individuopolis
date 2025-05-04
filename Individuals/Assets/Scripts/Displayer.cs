using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Displayer : MonoBehaviour
{
    [SerializeField] private Manager_Day dayManager;
    
    [SerializeField] public SpriteRenderer spriteRend;

    [SerializeField] public TextMeshProUGUI introDialogueText;
    [SerializeField] private int introDialogueTextIndex;
    
    void Start()
    {
        Manager_Day.onNextInQueue += DisplayEntity;
    }

    void OnDisabled()
    {
        Manager_Day.onNextInQueue -= DisplayEntity;
    }

    private void DisplayEntity()
    {
        if (dayManager.currentEntity.entitySprite != null)
        {
            
            spriteRend.sprite = dayManager.currentEntity.entitySprite;
        }

        introDialogueTextIndex = 0;
        DisplayEntityIntroText();
    }

    private void DisplayEntityIntroText()
    {
        introDialogueText.text = dayManager.currentEntity.introduction[introDialogueTextIndex];

        //Not working?
        /*if (dayManager.currentEntity.introduction.Length > 1)
        {
            introDialogueTextIndex ++;

            DisplayEntityIntroText();
        }*/
    }
}
