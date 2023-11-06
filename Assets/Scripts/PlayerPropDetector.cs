using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropDetector : MonoBehaviour
{
    [Header("References")]
    public Transform DetectionPoint;

    [Header("Settings")]
    public float DetectionRange = 1;
    public LayerMask DetectionMask;

    [Header("Data")]
    public Prop DetectedProp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Detect();
    }

    void Detect()
    {
        Collider[] colliders = Physics.OverlapSphere(DetectionPoint.position, DetectionRange, DetectionMask);
        List<Prop> detected = new List<Prop>();
        foreach (Collider collider in colliders)
        {
            Prop prop = collider.GetComponentInParent<Prop>();
            if (prop != null && !prop.IsInUse)
            {
                detected.Add(prop);
                //DetectedProp = prop;
                //return;
            }
        }

        if (detected.Count > 0)
        {
            detected.Sort((x, y) => Vector3.Distance(x.transform.position, DetectionPoint.transform.position).CompareTo(Vector3.Distance(y.transform.position, DetectionPoint.transform.position)));
            DetectedProp = detected[0];
        }
        else
            DetectedProp = null;
    }
}
