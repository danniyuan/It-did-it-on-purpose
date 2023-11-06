using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    public Transform Water;
    public Transform WaterZone;
    public Transform Icon;
    public AudioClip Clip;

    Prop prop;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }
    private void Awake()
    {
        prop = GetComponentInParent<Prop>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

public void Cupdown()
    {
        Icon.gameObject.SetActive(false);
        if (Clip!=null)
        AudioSource.PlayClipAtPoint(Clip, transform.position);
        prop.IsDamaged = true;
        if (Player.Instance.transform.position.z - transform.position.z >= 0)
            animator.SetTrigger("Back");
        else
            animator.SetTrigger("Front");
    }

}
