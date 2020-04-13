using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSellEvent : MonoBehaviour
{
    [Header("控件參數取用代理名稱")]
    public LevelSellEvent instance;
    [Header("動畫控件")]
    public AnimationClip ShowOut, ShowRemove;//自行初始化

    [Header("門物件")]
    public GameObject DoorObj, DoorEffectObj;

    [Header("按鈕物件")]
    public GameObject VolcanicBtn, IceFieldBtn, DesertBtn, NightBtn, DayBtn;

    [Header("夜晚白天按鈕圖控件")]
    public Sprite ForestDay, ForestNight;//自行初始化
    [Header("燈光物件")]
    public GameObject DayLight, NightLight;
    [Header("螢火蟲與棉絮物件")]
    public GameObject FirefliesObj;
    public GameObject PollenObj;

    void Awake()
    {
        if (instance == null) { instance = this; }
        
        VolcanicBtn     = GameObject.Find("volcanicBtn");
        IceFieldBtn     = GameObject.Find("IceFieldBtn");
        DesertBtn       = GameObject.Find("desertBtn");
        NightBtn        = GameObject.Find("nightBtn");
        DayBtn          = GameObject.Find("DayBtn");

        DoorObj         = GameObject.Find("doorobj");
        DoorEffectObj   = GameObject.Find("magic_ring_04");

        DayLight        = GameObject.Find("DayDirectionalLight");
        NightLight      = GameObject.Find("NightDirectionalLight");

        FirefliesObj    = GameObject.Find("Fireflies");
        PollenObj       = GameObject.Find("pollenparticle");

    }

    void Start()
    {
        NightLight.SetActive(false);
        DoorEffectObj.SetActive(false);
        DayBtn.SetActive(false);
        FirefliesObj.SetActive(false);
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    public void ChangeLevelButtonEvent(int Level)
    {
        VolcanicBtn     .GetComponent<Button>().enabled = false;
        IceFieldBtn     .GetComponent<Button>().enabled = false;
        DesertBtn       .GetComponent<Button>().enabled = false;
        NightBtn        .GetComponent<Button>().enabled = false;
        switch (Level)
        {
            case 0:
                GlobalValue.Level = Level;
                StartCoroutine(SetTimeFalseActive());
                print("更換了白天森林");
                break;
            case 1:
                GlobalValue.Level = Level;
                StartCoroutine(SetTimeFalseActive());
                print("更換了火山");
                break;
            case 2:
                GlobalValue.Level = Level;
                StartCoroutine(SetTimeFalseActive());
                print("更換了冰原");
                break;
            case 3:
                GlobalValue.Level = Level;
                StartCoroutine(SetTimeFalseActive());
                print("更換了沙漠");
                break;
            case 4:
                GlobalValue.Level = Level;
                StartCoroutine(SetTimeFalseActive());
                print("更換了晚上森林");
                break;

        }
    }

    IEnumerator SetTimeFalseActive()
    {
        this.GetComponent<Animation>().clip = ShowRemove;
        this.GetComponent<Animation>().Play();
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<Animation>().clip = ShowOut;
        this.gameObject.SetActive(false);
        if ((GlobalValue.Level == 1) || ((GlobalValue.Level == 2)) || (GlobalValue.Level == 3))
        {

            DayBtn.SetActive(false);//按鈕切換晚上鍵
            NightBtn.SetActive(true);

            DayLight.SetActive(true);//燈光切換白天
            NightLight.SetActive(false);

            PollenObj.SetActive(true);//開啟棉絮
            FirefliesObj.SetActive(false);//關閉螢火蟲

            DoorObj.SetActive(false);//門消失
            DoorEffectObj.SetActive(true);//開啟傳送門效果
        }
        else if (GlobalValue.Level == 0)
        {

            DayBtn.SetActive(false);//按鈕切換晚上鍵
            NightBtn.SetActive(true);

            DayLight.SetActive(true);//燈光切換白天
            NightLight.SetActive(false);

            PollenObj.SetActive(true);//開啟棉絮
            FirefliesObj.SetActive(false);//關閉螢火蟲

            DoorObj.SetActive(true);//門開啟
            DoorEffectObj.SetActive(false);//傳送門效果關閉
        }
        else if (GlobalValue.Level == 4)
        {
            DayBtn.SetActive(true);//按鈕切換白天鍵
            NightBtn.SetActive(false);

            DayLight.SetActive(false);//燈光切換晚上
            NightLight.SetActive(true);

            PollenObj.SetActive(false);//關閉棉絮
            FirefliesObj.SetActive(true);//開啟螢火蟲

            DoorObj.SetActive(true);//門開啟
            DoorEffectObj.SetActive(false);//傳送門效果關閉
        }

    }

}
