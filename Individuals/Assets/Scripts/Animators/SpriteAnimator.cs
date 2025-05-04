using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    [SerializeField] private Verdict verdict;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartLeavingAnimation()
    {
        animator.SetBool("isLeaving", true);
    }

    public void HasLeftAnimation()
    {
        verdict.HasLeft();
    }

    public void EndLeavingAnimation()
    {
        animator.SetBool("isLeaving", false);
        verdict.Proceed();
    }
}
