using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCameraRoot : MonoBehaviour
{
    public Prop Target;
    void Update()
    {
        if (Target)
        {
            transform.position = Target.transform.position;
            transform.localScale = Target.QuestViewScale * Vector3.one;
        }
    }
}
