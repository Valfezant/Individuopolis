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
    [SerializeField] private TextWriter textWriter;

    private Evaluator evaluator;

    [Header("Animators")]
    [SerializeField] private HammerAnimator hammerAnimator;
    [SerializeField] private SpriteAnimator spriteAnimator;

    [SerializeField] private AudioSource audioSource;

    [Header("Harvest")]
    [SerializeField] private Slider harvestSlider;

    private void Awake()
    {
        evaluator = GameObject.FindWithTag("Evaluator").GetComponent<Evaluator>();
    }

    //PERSON
    public void SpareVerdict()
    {
        textWriter._isActive = false;
        displayer.introDialogueText.text = "";

        textWriter._isActive = true;
        StartCoroutine(textWriter.TypeText(displayer.introDialogueText, dayManager.currentEntity.sparedDialogue[0], dayManager.currentEntity.entityTextSpeed / 2, dayManager.currentEntity.speakSound, dayManager.currentEntity.soundFrequency, dayManager.currentEntity.soundMinPitch, dayManager.currentEntity.soundMaxPitch));

        if (evaluator != null)
        {
            evaluator.CountEntity(dayManager.currentEntity);
        }
        
        //Play animation
        spriteAnimator.StartLeavingAnimation();
    }

    public void HasLeft()
    {
        displayer.spriteRend.sprite = null;
        //displayer.introDialogueText.text = "";
        textWriter._isActive = false;
    }

    //OBJECT
    public void DestroyVerdict()
    {
        textWriter._isActive = false;
        displayer.introDialogueText.text = "";
        
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

        dayManager.harvestCounter ++;
        harvestSlider.value = dayManager.harvestCounter;
    }

    //GENERAL
    public void Proceed()
    {        
        displayer.introDialogueText.text = "";
        dayManager.NextEntityInQueue();
        //Invoke("NextEntityInQueue", 1f);
        interactions.pokeFeedbackText.text = "";
    }
}
