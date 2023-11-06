using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baryCenter : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform tf;

    void Update()
    {

        GetComponent<Rigidbody>().centerOfMass = tf.localPosition;

    }
}
