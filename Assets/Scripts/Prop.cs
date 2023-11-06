using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Prop : MonoBehaviour
{
    [Header("Settings")]
    public bool IsPickable = false;
    public bool IsBitable = false;
    public bool IsScratchable = false;
    public bool NotWaterProof = false;
    public bool HideAble = false;
    public bool QuestProp = false;
    public float QuestViewScale = 1;
    public bool HightlightInQuestView = false;
    public int CameraField = 40;
    public AudioClip FallSound;
    public float _volume = 1;

    [Header("Score")]
    public int BasicScore = 0;
    public int WaterInScore = 0;
    public int HideScore = 0;


    [Header("Pick")]
    public Vector3 PickLocalPosition = Vector3.zero;
    public Vector3 PickLocalRotation = Vector3.zero;
    public UnityEvent OnPickBegin = new UnityEvent();
    public UnityEvent OnPickEnd = new UnityEvent();

    [Header("Damage")]
    public bool IsDamaged = false;
    public bool WaterIn = false;
    public bool IsHidden = false;

    [Header("Data")]
    public bool IsInUse = false;
    public bool IsBitten = false;

    Rigidbody rigidbody;
    MeshCollider Mesh_Collider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        Mesh_Collider = GetComponentInChildren<MeshCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PropManager.Instance.AddProp(this);
    }

    // Update is called once per frame
    void Update()
    {   
        if (IsDamaged || IsHidden || WaterIn)
        {
            ScoreCounter();
        }
    }


    public void Pick(bool enabled, Transform parent)
    {
        if (enabled)
        {
            transform.SetParent(parent);
            transform.localPosition = PickLocalPosition;
            transform.localEulerAngles = PickLocalRotation;
            rigidbody.isKinematic = true;
            IsInUse = true;
            OnPickBegin.Invoke();
        }
        else
        {
            transform.SetParent(null);
            rigidbody.isKinematic = false;
            IsInUse = false;
            OnPickEnd.Invoke();
        }
        
    }

    public void FinishBite()
    {
        //BiteMark.SetActive(true);
        //IsBitten = true;
    }
    public void ScoreCounter()
    {
        if (IsDamaged)
        {
            Score.TotalScore += BasicScore;
            BasicScore = 0;
        }
        if (IsHidden)
        {
            Score.TotalScore += HideScore;
            HideScore = 0;
        }
        if (WaterIn)
        {
            Score.TotalScore += WaterInScore;
            WaterInScore = 0;
        }
    }

    public void AddScore(int score)
    {
        QuestManager.Instance.AddScore(score);
    }

    public void AddScore()
    {
        QuestManager.Instance.AddScore(BasicScore);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > 5)
        {
            Debug.Log(gameObject.name+"Fallen");
            IsDamaged = true;
            if(FallSound!=null)
            AudioSource.PlayClipAtPoint(FallSound, transform.position,_volume);
        }
    }
}
