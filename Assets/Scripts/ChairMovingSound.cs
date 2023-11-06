using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairMovingSound : MonoBehaviour
{
    public bool SoundIsPlaying = false;
    Rigidbody _rigidbody;
    public AudioClip Clip;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.Sleep();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rigidbody.IsSleeping()&&!SoundIsPlaying)
        {
            PlaySound();
        }



    }
    public void PlaySound()
    {
        SoundIsPlaying = true;
        AudioSource.PlayClipAtPoint(Clip, transform.position);
        Invoke("SoundEnd", 1.5f);
    }
    public void SoundEnd()
    {
        SoundIsPlaying = false;
    }
}
