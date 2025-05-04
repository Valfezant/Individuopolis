using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Verdict : MonoBehaviour
{
    [SerializeField] private Manager_Day dayManager;
    [SerializeField] private Displayer displayer;
    [SerializeField] private InteractionActions interactions;
    [SerializeField] private ScreenShaker screenShaker;

    [Header("Animators")]
    [SerializeField] private HammerAnimator hammerAnimator;
    [SerializeField] private SpriteAnimator spriteAnimator;

    [SerializeField] private AudioSource audioSource;

    [Header("Harvest")]
    [SerializeField] private Slider harvestSlider;
    [SerializeField] public int harvestCounter;

    //PERSON
    public void SpareVerdict()
    {
        //Play animation
        spriteAnimator.StartLeavingAnimation();
    }

    public void HasLeft()
    {
        displayer.spriteRend.sprite = null;
        displayer.introDialogueText.text = "";
    }

    //OBJECT
    public void DestroyVerdict()
    {
        //Play animation
        hammerAnimator.StartHitAnimation();
    }

    public void HammerHit()
    {
        if (dayManager.currentEntity.killSound != null)
        {
            audioSource.PlayOneShot(dayManager.currentEntity.killSound, 1f);
        }

        screenShaker.StartShaking();

        //displayer.spriteRend.sprite = null;
        spriteAnimator.StartCrushingAnimation();
        displayer.introDialogueText.text = "";
    }

    //GENERAL
    public void Proceed()
    {
        harvestCounter ++;
        harvestSlider.value = harvestCounter;
        
        dayManager.NextEntityInQueue();
        interactions.pokeFeedbackText.text = "";
    }
}
