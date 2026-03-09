using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSpeed : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        animator.speed = GameManager.Instance.gameSpeed / GameManager.Instance.initialGameSpeed;
        animator.SetBool("isRunning", GameManager.Instance.gameSpeed > 0);
    }
}
