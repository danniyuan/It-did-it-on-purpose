using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestTargetPropsUI : MonoBehaviour
{
    public List<Text> PropTexts = new List<Text>();


    public GameObject TargetUI;
    bool MSAC=false;
    public Text Addscore;
    public int bonusscore = 30;

    public Animator _animator;
    // Start is called before the first frame update
    private void Awake()
    {
        //_animator = TargetUI.GetComponent<Animator>();
    }
    private void Update()
    {
        if (!MSAC) { DamagedDetector(); }

 
    }
    void Start()
    {
        Addscore.text = ("+" + bonusscore);
        if (QuestManager.Instance.CurrentTargetProps.Count == QuestManager.Instance.TargetPropCount)
        {
            for (int i = 0; i < PropTexts.Count; i++)
            {
                PropTexts[i].text = QuestManager.Instance.CurrentTargetProps[i].gameObject.name;
            }
        }
    }
    void DamagedDetector()
    {
        for (int i = 0; i < PropTexts.Count; i++)
        {
            PropTexts[i].text = QuestManager.Instance.CurrentTargetProps[i].gameObject.name;
            if (QuestManager.Instance.CurrentTargetProps[i].IsDamaged|| QuestManager.Instance.CurrentTargetProps[i].WaterIn || QuestManager.Instance.CurrentTargetProps[i].IsHidden)
            {
                ColorUtility.TryParseHtmlString("#3194B4", out Color newcolor);
                PropTexts[i].color = newcolor;
                if (PropTexts[1].color == newcolor&& PropTexts[2].color == newcolor && PropTexts[0].color == newcolor)
                {
                    Debug.Log("MsAc");
                    _animator.SetBool("MSAC", true);
                    Score.TotalScore += bonusscore;
                    MSAC = true;
                }

                    

            }
        }
    }
    
}
