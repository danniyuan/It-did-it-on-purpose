using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public static PropManager Instance;
    public int Score = 0;
    [SerializeField] List<Prop> props = new List<Prop>();

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void AddProp(Prop prop)
    {
        props.Add(prop);
    }

    [ContextMenu("Test Count Score")]
    void CountScore()
    {
        Score = 0;
        foreach (var prop in props)
        {
            // temp
            if (prop.IsBitten)
            {
                Score += 1;
            }
        }
    }

}
