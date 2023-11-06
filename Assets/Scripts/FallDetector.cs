using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour
{
    public Vector3 OriginalPosition= new Vector3(0,0,0);
    public Vector3 OriginalRotation = new Vector3(0, 0, 0);
    public AudioClip FallSound;
    public float LimitHeight = 0;
    public float LimitAngle = 80;
    Prop prop;
    // Start is called before the first frame update
    void Start()
    {
        OriginalPosition = gameObject.transform.position;
        OriginalRotation = gameObject.transform.eulerAngles;
    }
    private void Awake()
    {
        prop = GetComponentInParent<Prop>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            CheckPosition();
            CheckRotation();
        }

    }
    public void CheckPosition()
    {
        if ((gameObject.transform.position.z - OriginalPosition.z) >= LimitHeight)
        {
            prop.IsDamaged = true;
            AudioSource.PlayClipAtPoint(FallSound, transform.position);
        }
    }
    public void CheckRotation()
    {
        if ((gameObject.transform.eulerAngles.x - OriginalRotation.x )>= LimitAngle)
        {
            prop.IsDamaged = true;
            AudioSource.PlayClipAtPoint(FallSound, transform.position);
        }
    }
}
