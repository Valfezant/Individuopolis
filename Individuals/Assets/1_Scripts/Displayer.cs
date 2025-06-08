using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Displayer : MonoBehaviour
{
    [SerializeField] private Manager_Day dayManager;
    [SerializeField] private SpriteAnimator spriteAnimator;
    [SerializeField] private TextWriter textWriter;
    
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
            spriteAnimator.StartEnteringAnimation();
            spriteRend.sprite = dayManager.currentEntity.entitySprite;
        }

        introDialogueTextIndex = 0;
        //DisplayEntityIntroText();
    }

    public void DisplayEntityIntroText()
    {
        //introDialogueText.text = dayManager.currentEntity.introduction[introDialogueTextIndex];
        
        textWriter._isActive = true;
        introDialogueText.text = "";
        StartCoroutine(textWriter.TypeText(introDialogueText, dayManager.currentEntity.introduction[introDialogueTextIndex], dayManager.currentEntity.entityTextSpeed, dayManager.currentEntity.speakSound, dayManager.currentEntity.soundFrequency, dayManager.currentEntity.soundMinPitch, dayManager.currentEntity.soundMaxPitch));
    }
}
