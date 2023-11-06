using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CollisionEvent : MonoBehaviour
{
    [Header("Config")]
    public UnityEvent OnCollision = new UnityEvent();
    public bool Once = false;
    public bool hasCollided = false;
    Prop _prop;
    // Start is called before the first frame update
    private void Awake()
    {
        _prop = GetComponent<Prop>();
    }
    void Start()

    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            if (Once && hasCollided) return;
            if (_prop.IsDamaged)
            {
                Debug.Log("Lantern changed");
                OnCollision.Invoke();
                hasCollided = true;
            }

        }

    }
}
