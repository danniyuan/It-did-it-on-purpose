using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialZone: MonoBehaviour
{
    public bool Water = false;
    public bool Hide = false;
    public AudioClip Disappear;

    public void OnTriggerEnter(Collider collider)
    {
        Prop prop = collider.GetComponentInParent<Prop>();
        if (prop != null)
        {
            if (Water && prop.NotWaterProof)
            {
                if (!prop.WaterIn)
                    prop.AddScore(prop.WaterInScore);

                prop.WaterIn = true;
            }

            if (Hide && prop.HideAble)
            {
                prop.IsHidden = true;
                prop.gameObject.SetActive(false);
                if (Disappear != null)
                {
                    AudioSource.PlayClipAtPoint(Disappear, transform.position);
                }
            }
        }
    }
}
