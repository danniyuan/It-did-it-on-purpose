using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBiteBehaviour : MonoBehaviour
{
    [Header("References")]
    public Transform LeftHandPoint;
    public Transform RightHandPoint;

    [Header("Settings")]
    public KeyCode Key = KeyCode.Q;
    public float BiteRange = 0.1f;
    public float BiteDistance = 0.1f;
    public float BiteStartOffset = 0.1f;

    PlayerPropDetector playerPropDetector;

    CharacterController characterController;

    public bool isBiting = false;

    Animator animator;
    Player player;


    private void Awake()
    {
        playerPropDetector = GetComponent<PlayerPropDetector>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Bite();
        HandleBite();
    }

    void Bite()
    {
        if (Input.GetKeyUp(Key))
        {
            animator.SetTrigger("Scratch");
        }
        /*
        Prop prop = playerPropDetector.DetectedProp;

        if (!player.IsInteractable) return;
        if (prop == null) return;

        if (Input.GetKeyUp(Key))
        {
            if ((prop.IsBitable || prop.IsScratchable) && !prop.IsInUse)
            {
                // temp
                // todo: navigation to bite point
                characterController.enabled = false;
                transform.position = prop.BitePoint.position;
                transform.forward = prop.BitePoint.forward;
                characterController.enabled = true;

                player.IsInteractable = false;
                animator.SetTrigger(prop.IsBitable ? "Bite" : "Scratch");

                // temp
                // todo: use anim events
                prop.Invoke("FinishBite", 0.8f);
                StartCoroutine(IReleaseInteraction());
            }
        }
        */
    }

    IEnumerator IReleaseInteraction()
    {
        // temp
        // todo: use anim events
        yield return new WaitForSeconds(2);
        player.IsInteractable = true;
    }

    public void BiteBegin()
    {
        isBiting = true; 
        Debug.Log("Is Biting");

    }
    public void BiteEnd()
    {

        isBiting = false;
        Debug.Log("Not Biting");
    }



    void HandleBite()
    {
        if (!isBiting) return;

        var hits = Physics.RaycastAll(LeftHandPoint.position + LeftHandPoint.forward * BiteStartOffset, - LeftHandPoint.forward, BiteDistance);

        //var hits = Physics.SphereCastAll(LeftHandPoint.position, BiteRange, - LeftHandPoint.forward, BiteDistance);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("BiteMark"))
            {
                if (hit.point == Vector3.zero) continue;
                hit.collider.GetComponentInParent<BiteMarkCollider>().Bite(hit.point, hit.collider.transform.rotation);
                hit.collider.GetComponentInParent<Prop>().IsDamaged = true;
            }
        }

        hits = Physics.RaycastAll(RightHandPoint.position + LeftHandPoint.forward * BiteStartOffset, - RightHandPoint.forward, BiteDistance);
        foreach (var hit in hits)
        {
            if (hit.collider.CompareTag("BiteMark"))
            {
                if (hit.point == Vector3.zero) continue;
                hit.collider.GetComponentInParent<BiteMarkCollider>().Bite(hit.point, hit.collider.transform.rotation);
                hit.collider.GetComponentInParent<Prop>().IsDamaged = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(LeftHandPoint.position, LeftHandPoint.position - LeftHandPoint.forward * BiteDistance);
        Gizmos.DrawLine(RightHandPoint.position, RightHandPoint.position - LeftHandPoint.forward * BiteDistance);
    }
}
