using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconOnHead : MonoBehaviour
{
    public GameObject icon;
    Prop prop;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void Awake()
    {
        prop = GetComponentInParent<Prop>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void IconOn()
    {
        if (prop.QuestProp&&!prop.IsDamaged)
        icon.gameObject.SetActive(true);
    }

    public void IconOff()
    {
        if (prop.QuestProp&&!prop.IsDamaged)
            icon.gameObject.SetActive(false);
    }

}
