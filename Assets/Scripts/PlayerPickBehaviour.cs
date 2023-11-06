using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using StarterAssets;

public class PlayerPickBehaviour : MonoBehaviour
{
    [Header("References")]
    public Transform MouthPoint;

    [Header("Settings")]
    public KeyCode Key = KeyCode.E;
    public KeyCode ReleaseKey = KeyCode.R;
    public float HeightTolerance = 0.5f;
    public float StoppingDistance = 0.5f;

    [Header("Data")]
    [SerializeField] Prop pickedProp;
    bool isAnimEnded = false;

    PlayerPropDetector playerPropDetector;
    Player player;
    //NavMeshAgent navMeshAgent;
    CharacterController characterController;
    Animator animator;
    ThirdPersonController thirdPersonController;

    private void Awake()
    {
        playerPropDetector = GetComponent<PlayerPropDetector>();
        characterController = GetComponent<CharacterController>();
        //navMeshAgent = GetComponent<NavMeshAgent>();
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void Update()
    {
        Pick();
        Release();
    }

    void Pick()
    {
        if (!player.IsInteractable) return;

        Prop prop = playerPropDetector.DetectedProp;
        if (Input.GetKeyUp(Key)&&prop!=null)
        {
           
            if (prop.IsPickable)
            {
                if (pickedProp != null)
                    pickedProp.Pick(false, null);

                // transition
                StartCoroutine(IPickBegin(prop));


            }
        }
    }

    IEnumerator IPickBegin(Prop prop)
    {
        float heightOffset = Mathf.Abs(transform.position.y - prop.transform.position.y);
        if (heightOffset > HeightTolerance) yield break;

        player.IsInteractable = false;
        characterController.enabled = false;
        thirdPersonController.enabled = false;

        float distance = Vector3.Distance(transform.position, prop.transform.position);
        if (distance >= StoppingDistance)
        {
            do
            {
                transform.position = Vector3.MoveTowards(transform.position, prop.transform.position, Time.deltaTime * 3);
                transform.forward = Vector3.RotateTowards(transform.forward, prop.transform.position - transform.position, Time.deltaTime * 12, Time.deltaTime);
                animator.SetFloat("Speed", 1);
                animator.SetFloat("MotionSpeed", 1);
                yield return null;
                distance = Vector3.Distance(transform.position, prop.transform.position);
            }
            while (distance > StoppingDistance);
            animator.SetFloat("Speed", 0);
        }

        // pick anim
        animator.SetTrigger("Pick");

        do
        {
            yield return new WaitForEndOfFrame();
        }
        while (!isAnimEnded);
        isAnimEnded = false;

        prop.Pick(true, MouthPoint);
        pickedProp = prop;

        do
        {
            yield return new WaitForEndOfFrame();
        }
        while (!isAnimEnded);
        isAnimEnded = false;

        // reset
        player.IsInteractable = true;
        characterController.enabled = true;
        thirdPersonController.enabled = true;
    }

    public void AnimEnd()
    {
        isAnimEnded = true;
    }

    void Release()
    {
        if (Input.GetKeyUp(ReleaseKey))
        {
            if (pickedProp != null)
                pickedProp.Pick(false, null);
        }
    }
}
