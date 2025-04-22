using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerAnimator : MonoBehaviour
{
    [SerializeField] private Verdict verdict;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartHitAnimation()
    {
        animator.SetBool("isHitting", true);
    }

    public void HammerImpact()
    {
        verdict.HammerHit();
    }

    public void EndHitAnimation()
    {
        animator.SetBool("isHitting", false);
        verdict.Proceed();
    }
}
