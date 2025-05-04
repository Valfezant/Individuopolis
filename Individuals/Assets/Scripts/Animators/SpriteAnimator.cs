using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Verdict verdict;
    [SerializeField] private Displayer displayer;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //Exit
    public void StartLeavingAnimation()
    {
        animator.SetBool("isLeaving", true);
    }

    public void HasLeftAnimation()
    {
        verdict.HasLeft();
        animator.SetBool("isLeaving", false);
    }

    public void EndLeavingAnimation()
    {
        verdict.Proceed();
    }

    //Enter
    public void StartEnteringAnimation()
    {
        animator.SetBool("isEntering", true);
    }

    public void HasEnteredAnimation()
    {
        displayer.DisplayEntityIntroText();
        animator.SetBool("isEntering", false);
    }

    //Crushing
    public void StartCrushingAnimation()
    {
        animator.SetBool("isCrushed", true);
    }
    
    public void EndCrushingAnimation()
    {
        animator.SetBool("isCrushed", false);
    }
}
