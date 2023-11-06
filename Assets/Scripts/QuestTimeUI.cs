using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTimeUI : MonoBehaviour
{
    Image image;
    Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = QuestManager.Instance.CurrentGameTime / QuestManager.Instance.GameDuration;
        if (image.fillAmount ==1)
            {
            animator.SetBool("Ismoving", false);
            Player.Instance.IsInteractable = false;


        }
    }
}
