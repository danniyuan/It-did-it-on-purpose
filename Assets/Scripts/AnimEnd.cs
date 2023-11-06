using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEnd : MonoBehaviour
{
    public GameObject Anim;
    bool once = false;
    public GameObject BGM2;
    public AudioClip Shocked;
    public AudioClip Warn;
    public AudioClip SadMusic;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)&&!once)
        {
            CheckEnd();
            once = true;
        }
    }
    public void AnimationEnd()
    {
        if (!once) {
            CheckEnd();
        }

    }
    public void CheckEnd()
    {
        
            QuestManager.Instance.StartCamera();
            Anim.gameObject.SetActive(false); 
        BGM2.gameObject.SetActive(true);


    }
    public void Shock()
    {
        AudioSource.PlayClipAtPoint(Shocked, Camera.main.transform.position);
    }
    public void Warning()
    {
        AudioSource.PlayClipAtPoint(Warn, Camera.main.transform.position);
    }
    public void Sad()
    {
        AudioSource.PlayClipAtPoint(SadMusic, Camera.main.transform.position);
    }
}
