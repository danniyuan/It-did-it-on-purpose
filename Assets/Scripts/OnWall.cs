using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWall : MonoBehaviour
{
    Rigidbody rigidbody;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        rigidbody = GetComponentInParent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rigidbody.useGravity = true;
            Debug.Log("OnWall G on");
            
        }
    }
}
