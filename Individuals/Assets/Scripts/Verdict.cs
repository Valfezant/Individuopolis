using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Verdict : MonoBehaviour
{
    [SerializeField] private Manager_Day dayManager;
    [SerializeField] private Displayer displayer;

    [SerializeField] private HammerAnimator hammerAnimator;
    [SerializeField] private SpriteAnimator spriteAnimator;

    [SerializeField] private AudioSource audioSource;

    public void SpareVerdict()
    {
        //Play animation
        spriteAnimator.StartLeavingAnimation();

        Debug.Log("Person.");
        //displayer.spriteRend.sprite = null;
        //dayManager.NextEntityInQueue();
    }

    public void HasLeft()
    {
        displayer.spriteRend.sprite = null;
    }

    public void DestroyVerdict()
    {
        //Play animation
        hammerAnimator.StartHitAnimation();

        Debug.Log("OBJECT.");
    }

    public void HammerHit()
    {
        if (dayManager.currentEntity.killSound != null)
        {
            audioSource.PlayOneShot(dayManager.currentEntity.killSound, 1f);
        }

        displayer.spriteRend.sprite = null;
    }

    public void Proceed()
    {
        dayManager.NextEntityInQueue();
    }
}
