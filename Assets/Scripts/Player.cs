using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public bool IsInteractable = false;
    public static Player Instance;

    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        Instance = this;
    }

    private void Update()
    {
        animator.SetFloat("TailSpeed", 1 + QuestManager.Instance.Score / 50f);
    }
}
