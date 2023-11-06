using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyGo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CountStart()
    {
        QuestManager.Instance.CountingStart();
        Player.Instance.IsInteractable = true;
    }
}
