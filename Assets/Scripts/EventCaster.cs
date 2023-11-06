using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventCaster : MonoBehaviour
{

    [Header("Config")]
    public string Tag = "Player";
    public UnityEvent OnEnter = new UnityEvent();
    public UnityEvent OnExit = new UnityEvent();
    public bool Once = false;

    bool hasEntered = false;
    bool hasExit = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == Tag)
        {
            if (Once && hasEntered) return;
            OnEnter.Invoke();
            hasEntered = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.tag == Tag)
        {
            if (Once && hasExit) return;
            OnExit.Invoke();
            hasExit = true;
        }
    }
}
