using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Animations;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [Header("References")]
    public GameObject GameEndUI;
    public GameObject ReadyGo;
    public List<Prop> TargetProps = new List<Prop>();
    public QuestCameraRoot CameraRoot;
    public CinemachineVirtualCamera VirtualCamera;
    public bool TimeCountingStart = false;
    public Material HighlightMat;


    [Header("Settings")]
    public float GameDuration = 300;
    public int TargetPropCount = 3;
    public int ScoreStreakCount = 3;
    public float SocreStreakInterval = 5;//�������
    public float ScoreStreakBonusFactor = 1.5f;//�����ӷֲ���
    public float QuestViewDuration = 3;

    [Header("Data")]
    public int Score = 0;
    public List<Prop> CurrentTargetProps = new List<Prop>();
    public float CurrentGameTime;


    float gameBeginTime;

    float lastScoreTime = 0;
    [SerializeField] int currentScoreStreakCount = 0;

    private void Awake()
    {
        if (!Instance) Instance = this;
    }

    private void Start()
    {

        SelectCurrentTargetProps();

    }

    private void Update()
    {
        if (TimeCountingStart&&!GameEndUI.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape)) 
                {
            #if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
            #else
            Application.Quit();
            #endif
                }
            CurrentGameTime = Time.time - gameBeginTime;
            if (CurrentGameTime >= GameDuration)
            { GameEndUI.SetActive(true);

            }

        }

    }
    public void StartCamera() {
        StartCoroutine(IShowTargets());
    }

    public void AddScore(int score)
    {
        Debug.Log("Ready to add " + score);

        // count streaks
        if (Time.time - lastScoreTime <= SocreStreakInterval)
            currentScoreStreakCount++;
        else
            currentScoreStreakCount = 0;

        // record last core time
        lastScoreTime = Time.time;

        // add score based on streaks
        if (currentScoreStreakCount >= ScoreStreakCount)
            Score += (int)(score * ScoreStreakBonusFactor);
        else
            Score += score;
    }

    void SelectCurrentTargetProps()
    {
        if (TargetProps.Count < TargetPropCount) return;

        while (CurrentTargetProps.Count < TargetPropCount)
        {
            int propIndex = Random.Range(0, TargetProps.Count);
            Prop prop = TargetProps[propIndex];
            prop.QuestProp = true;
            if (!CurrentTargetProps.Contains(prop))
                CurrentTargetProps.Add(prop);
        }
    }

    IEnumerator IShowTargets()
    {
        VirtualCamera.Priority = 11;


        foreach (Prop prop in CurrentTargetProps)
        {
            CameraRoot.Target = prop;
            VirtualCamera.LookAt = prop.transform;

            var lens = VirtualCamera.m_Lens;
            lens.FieldOfView = prop.CameraField;
            VirtualCamera.m_Lens = lens;

            yield return new WaitForSeconds(1);

            // set prop highlight
            Dictionary<MeshRenderer, Material> materialsByMeshRenderer = new();
            if (prop.HightlightInQuestView)
            {
                var meshRenderers = prop.GetComponentsInChildren<MeshRenderer>();
                foreach (var meshRenderer in meshRenderers)
                {
                    materialsByMeshRenderer.Add(meshRenderer, meshRenderer.material);
                    meshRenderer.material = HighlightMat;
                }
            }

            yield return new WaitForSeconds(QuestViewDuration);

           //  remove prop highlight
            if (prop.HightlightInQuestView)
            {
                foreach (var pair in materialsByMeshRenderer)
                   pair.Key.material = pair.Value;
            }
 
        }
        VirtualCamera.Priority = 9;
        CameraRoot.Target = null;

        VirtualCamera.gameObject.SetActive(false);
        ReadyGo.gameObject.SetActive(true);

    }

    public void CountingStart()
    {
        if (!TimeCountingStart)
        {
            gameBeginTime = Time.time;
            TimeCountingStart = true;
            ReadyGo.gameObject.SetActive(false);
        }
    }
}
