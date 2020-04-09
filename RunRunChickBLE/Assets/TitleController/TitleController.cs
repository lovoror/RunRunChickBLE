using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [Header("控件參數取用代理名稱")]
    public TitleController instance;

    [Header("玩家目標物件(母雞)")]
    public GameObject[] ParentObjList;
    public GameObject ParentCanvasObj;

    [Header("玩家物件(雛雞)")]
    public GameObject[] PlayerObjList;
    public GameObject PlayerCanvasObj;

    [Header("追擊玩家物件(小狗)")]
    public GameObject[] ChaserObjList;
    public GameObject ChaserCanvasObj;


    [Header("金幣顯示物件")]
    public GameObject CoinObj;

    [Header("空白的山羊按鈕")]
    public GameObject GoatBtn;

    [Header("確認選單-更換角色模組")]
    public GameObject SellFalseTurePanelObj;
    [Header("確認腳色模組選單的是否按鈕")]
    public GameObject SellTrueBtn, SellFalseBtn;

    [Header("確認選單-更換場景模組")]
    public GameObject LevelFalseTurePanelObj;
    [Header("確認場景選單的是否按鈕")]
    public GameObject LevelSellTrueBtn, LevelSellFalseBtn;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null) { instance = this; }

        ParentObjList               = new GameObject[5];
        PlayerObjList               = new GameObject[5];
        ChaserObjList               = new GameObject[5];
        ParentCanvasObj             = GameObject.Find("ParentChangeBtn");
        PlayerCanvasObj             = GameObject.Find("PlayerChangeBtn");
        ChaserCanvasObj             = GameObject.Find("ChaserChangeBtn");
        

        ParentObjList[0]            = GameObject.Find("ParentObj");
        ParentObjList[1]            = GameObject.Find("Chicken");
        ParentObjList[2]            = GameObject.Find("Sheep");
        ParentObjList[3]            = GameObject.Find("Alpaca");
        ParentObjList[4]            = GameObject.Find("Horse");
        ParentObjList[2]            .SetActive(false);
        ParentObjList[3]            .SetActive(false);
        ParentObjList[4]            .SetActive(false);

        PlayerObjList[0]            = GameObject.Find("PlayerObj");
        PlayerObjList[1]            = GameObject.Find("Chick");
        PlayerObjList[2]            = GameObject.Find("Chick");
        PlayerObjList[3]            = GameObject.Find("Chick");
        PlayerObjList[4]            = GameObject.Find("Chick");

        ChaserObjList[0]            = GameObject.Find("ChaserObj");
        ChaserObjList[1]            = GameObject.Find("Dog");
        ChaserObjList[2]            = GameObject.Find("GoatB");
        ChaserObjList[3]            = GameObject.Find("Pig");
        ChaserObjList[4]            = GameObject.Find("SheepC");
        ChaserObjList[2]            .SetActive(false);
        ChaserObjList[3]            .SetActive(false);
        ChaserObjList[4]            .SetActive(false);

        CoinObj                     = GameObject.Find("CoinPanelObj");

        LevelFalseTurePanelObj      = GameObject.Find("LevelFalseTurePanelObj");
        LevelSellTrueBtn            = GameObject.Find("LevelTrueButton");
        LevelSellFalseBtn           = GameObject.Find("LevelFalseButton");

        SellFalseTurePanelObj       = GameObject.Find("SellFalseTurePanelObj");
        SellTrueBtn                 = GameObject.Find("SellTrueButton");
        SellFalseBtn                = GameObject.Find("SellFalseButton");


        //ParentCanvasObj             .SetActive(false);
        //PlayerCanvasObj             .SetActive(false);
        //ChaserCanvasObj             .SetActive(false);
        LevelFalseTurePanelObj      .SetActive(false);
        SellFalseTurePanelObj       .SetActive(false);
        //CoinObj                     .SetActive(false);
        
    }

    public void StartGameEvent()
    {

        print("開始遊戲");

    }

    /*▼▼▼▼▼購買選單事件▼▼▼▼▼*/

    public void SelectSellEvent()
    {
        print("開啟購買選單");
        SellFalseTurePanelObj.SetActive(true);
    }
    public void SelectSellTrue()
    {
        ParentCanvasObj.SetActive(true);
        //PlayerCanvasObj.SetActive(true);
        ChaserCanvasObj.SetActive(true);
        SellFalseTurePanelObj.SetActive(false);
    }
    public void SelectSellFalse()
    {
        SellFalseTurePanelObj.SetActive(false);
    }
    public void SelectSellChange_Player()
    {
        print("Player換角色");
    }
    public void SelectSellChange_Parent()
    {
        if (ParentObjList[1].activeSelf == true)
        {
            ParentObjList[1].SetActive(false);
            ParentObjList[2].SetActive(true);
        }
        else if (ParentObjList[2].activeSelf == true)
        {
            ParentObjList[2].SetActive(false);
            ParentObjList[3].SetActive(true);
        }
        else if (ParentObjList[3].activeSelf == true)
        {
            ParentObjList[3].SetActive(false);
            ParentObjList[4].SetActive(true);
        }
        else if (ParentObjList[4].activeSelf == true)
        {
            ParentObjList[4].SetActive(false);
            ParentObjList[1].SetActive(true);
        }
        print("Parent換角色");
    }
    public void SelectSellChange_Chaser()
    {
        if (ChaserObjList[1].activeSelf == true)
        {
            ChaserObjList[1].SetActive(false);
            ChaserObjList[2].SetActive(true);
        }
        else if (ChaserObjList[2].activeSelf == true)
        {
            ChaserObjList[2].SetActive(false);
            ChaserObjList[3].SetActive(true);
        }
        else if (ChaserObjList[3].activeSelf == true)
        {
            ChaserObjList[3].SetActive(false);
            ChaserObjList[4].SetActive(true);
        }
        else if (ChaserObjList[4].activeSelf == true)
        {
            ChaserObjList[4].SetActive(false);
            ChaserObjList[1].SetActive(true);
        }
        print("Chaser換角色");
    }

    /*▲▲▲▲▲購買選單事件▲▲▲▲▲*/


    /*▼▼▼▼▼關卡選單事件▼▼▼▼▼*/

    public void SelectLevelEvent()
    {
        print("開啟購買選單");
        LevelFalseTurePanelObj.SetActive(true);
    }
    public void SelectLevelTrue()
    {
        LevelFalseTurePanelObj.SetActive(false);
    }
    public void SelectLevelFalse()
    {
        LevelFalseTurePanelObj.SetActive(false);
    }
    public void SelectLevelChange()
    {
        LevelFalseTurePanelObj.SetActive(false);
    }
    /*▲▲▲▲▲關卡選單事件▲▲▲▲▲*/


    // Start is called before the first frame update
    void Start()
    {
        ParentObjList[1].GetComponent<Animator>().SetBool("Run", true);
        PlayerObjList[1].GetComponent<Animator>().SetBool("Run", true);
        ChaserObjList[1].GetComponent<Animator>().SetBool("Run", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
