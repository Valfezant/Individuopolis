using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionActions : MonoBehaviour
{
    [SerializeField] private Manager_Day dayManager;
    [SerializeField] private TextWriter textWriter;

    [SerializeField] private TextMeshProUGUI speakFeedbackText;
    [SerializeField] private int speakFeedbackIndex;
    [SerializeField] public TextMeshProUGUI pokeFeedbackText;
    [SerializeField] private int pokeFeedbackIndex;

    [SerializeField] public AudioClip playerSpeakSound;

    void Start()
    {
        Manager_Day.onNextInQueue += ClearIndex;
    }

    void OnDisabled()
    {
        Manager_Day.onNextInQueue -= ClearIndex;
    }

    public void SpeakAction()
    {
        if (speakFeedbackIndex < dayManager.currentEntity.actionSpeakFeedback.Length)
        {
            //speakFeedbackText.text = dayManager.currentEntity.actionSpeakFeedback[speakFeedbackIndex];
            
            textWriter._isActive = false;
            speakFeedbackText.text = "";

            speakFeedbackText.fontSize = dayManager.currentEntity.entityFontSize;
            textWriter._isActive = true;
            StartCoroutine(textWriter.TypeText(speakFeedbackText, dayManager.currentEntity.actionSpeakFeedback[speakFeedbackIndex], dayManager.currentEntity.entityTextSpeed, dayManager.currentEntity.speakSound, dayManager.currentEntity.soundFrequency, dayManager.currentEntity.soundMinPitch, dayManager.currentEntity.soundMaxPitch));
            speakFeedbackIndex ++;
        }
        else
        {
            Debug.Log("No more dialogue");
        }
    }

    public void PokeAction()
    {
        pokeFeedbackIndex = 0;
        pokeFeedbackText.text = "";
        StartCoroutine(textWriter.TypeText(pokeFeedbackText, dayManager.currentEntity.actionPokeFeedback[pokeFeedbackIndex], 0f, playerSpeakSound, 3, 1f, 1f));

        //pokeFeedbackText.text = dayManager.currentEntity.actionPokeFeedback[pokeFeedbackIndex];
    }

    private void ClearIndex()
    {
        speakFeedbackIndex = 0;
        pokeFeedbackIndex = 0;
    }
}
