using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionActions : MonoBehaviour
{
    [SerializeField] private Manager_Day dayManager;

    [SerializeField] private TextMeshProUGUI speakFeedbackText;
    [SerializeField] private TextMeshProUGUI pokeFeedbackText;
    [SerializeField] private int feedbackIndex;

    public void SpeakAction()
    {
        if (feedbackIndex < dayManager.currentEntity.actionSpeakFeedback.Length)
        {
            speakFeedbackText.text = dayManager.currentEntity.actionSpeakFeedback[feedbackIndex];
            feedbackIndex ++;
        }
        else
        {
            feedbackIndex = 0;
            speakFeedbackText.text = dayManager.currentEntity.actionSpeakFeedback[feedbackIndex];
            feedbackIndex ++;
        }
    }

    public void PokeAction()
    {
        feedbackIndex = 0;
        pokeFeedbackText.text = dayManager.currentEntity.actionPokeFeedback[feedbackIndex];
    }
}
