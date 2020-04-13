using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("門物件")]
    public GameObject LevelChangeEventObj;//當門關掉時就必須開啟動畫

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

        ParentObjList               = new GameObject[8];
        PlayerObjList               = new GameObject[8];
        ChaserObjList               = new GameObject[11];
        ParentCanvasObj             = GameObject.Find("ParentChangeBtn");
        PlayerCanvasObj             = GameObject.Find("PlayerChangeBtn");
        ChaserCanvasObj             = GameObject.Find("ChaserChangeBtn");
        

        ParentObjList[0]            = GameObject.Find("ParentObj");
        ParentObjList[1]            = GameObject.Find("Chicken");
        ParentObjList[2]            = GameObject.Find("Sheep");
        ParentObjList[3]            = GameObject.Find("Alpaca");
        ParentObjList[4]            = GameObject.Find("Horse");
        ParentObjList[5]            = GameObject.Find("PenguinB1");
        ParentObjList[6]            = GameObject.Find("PenguinB2");
        ParentObjList[7]            = GameObject.Find("PenguinB3");

        PlayerObjList[0]            = GameObject.Find("PlayerObj");
        PlayerObjList[1]            = GameObject.Find("Cute_Bird_00");
        PlayerObjList[2]            = GameObject.Find("Cute_Bird_01");
        PlayerObjList[3]            = GameObject.Find("Cute_Bird_03");
        PlayerObjList[4]            = GameObject.Find("Cute_Bird_05");
        PlayerObjList[5]            = GameObject.Find("Cute_Bird_10");
        PlayerObjList[6]            = GameObject.Find("Cute_Bird_12");
        PlayerObjList[7]            = GameObject.Find("Cute_Bird_14");

        ChaserObjList[0]            = GameObject.Find("ChaserObj");
        ChaserObjList[1]            = GameObject.Find("Dog");
        ChaserObjList[2]            = GameObject.Find("GoatB");
        ChaserObjList[3]            = GameObject.Find("Pig");
        ChaserObjList[4]            = GameObject.Find("SheepC");
        ChaserObjList[5]            = GameObject.Find("Dino04");
        ChaserObjList[6]            = GameObject.Find("Dino11");
        ChaserObjList[7]            = GameObject.Find("Dino25");
        ChaserObjList[8]            = GameObject.Find("DragonSD_32");
        ChaserObjList[9]            = GameObject.Find("DragonSD_01");
        ChaserObjList[10]           = GameObject.Find("DragonSD_13");


        CoinObj                     = GameObject.Find("CoinPanelObj");

        LevelFalseTurePanelObj      = GameObject.Find("LevelFalseTurePanelObj");
        LevelSellTrueBtn            = GameObject.Find("LevelTrueButton");
        LevelSellFalseBtn           = GameObject.Find("LevelFalseButton");

        SellFalseTurePanelObj       = GameObject.Find("SellFalseTurePanelObj");
        SellTrueBtn                 = GameObject.Find("SellTrueButton");
        SellFalseBtn                = GameObject.Find("SellFalseButton");
        GoatBtn                     = GameObject.Find("GoatSellBtn");

        LevelChangeEventObj         = GameObject.Find("LevelChangeEvent");
        


        //ParentCanvasObj             .SetActive(false);
        //PlayerCanvasObj             .SetActive(false);
        //ChaserCanvasObj             .SetActive(false);
        LevelFalseTurePanelObj      .SetActive(false);
        SellFalseTurePanelObj       .SetActive(false);
        GoatBtn                     .SetActive(false);
        //CoinObj                     .SetActive(false);

    }

    public void StartGameEvent()
    {

        print("開始遊戲");

    }

    /*▼▼▼▼▼購買選單事件▼▼▼▼▼*/

    public void SelectSellEvent()
    {
        print("開啟購買角色選單");
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


    /*▼▼▼▼▼購買角色選單事件▼▼▼▼▼*/

    public void SelectSellChange_Player()//玩家(小雞)
    {
        if (PlayerObjList[1].activeSelf == true)
        {
            PlayerObjList[1].SetActive(false);
            PlayerObjList[2].SetActive(true);
        }
        else if (PlayerObjList[2].activeSelf == true)
        {
            PlayerObjList[2].SetActive(false);
            PlayerObjList[3].SetActive(true);
        }
        else if (PlayerObjList[3].activeSelf == true)
        {
            PlayerObjList[3].SetActive(false);
            PlayerObjList[4].SetActive(true);
        }
        else if (PlayerObjList[4].activeSelf == true)
        {
            PlayerObjList[4].SetActive(false);
            PlayerObjList[5].SetActive(true);
        }
        else if (PlayerObjList[5].activeSelf == true)
        {
            PlayerObjList[5].SetActive(false);
            PlayerObjList[6].SetActive(true);
        }
        else if (PlayerObjList[6].activeSelf == true)
        {
            PlayerObjList[6].SetActive(false);
            PlayerObjList[7].SetActive(true);
        }
        else if (PlayerObjList[7].activeSelf == true)
        {
            PlayerObjList[7].SetActive(false);
            PlayerObjList[1].SetActive(true);
        }

        AllRunkEvent();
        print("Player換角色");
    }
    public void SelectSellChange_Parent()//跟蹤者(母雞)
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
            ParentObjList[5].SetActive(true);
        }
        else if (ParentObjList[5].activeSelf == true)
        {
            ParentObjList[5].SetActive(false);
            ParentObjList[6].SetActive(true);
        }
        else if (ParentObjList[6].activeSelf == true)
        {
            ParentObjList[6].SetActive(false);
            ParentObjList[7].SetActive(true);
        }
        else if (ParentObjList[7].activeSelf == true)
        {
            ParentObjList[7].SetActive(false);
            ParentObjList[1].SetActive(true);
        }
        AllRunkEvent();
        print("Parent換角色");
    }
    public void SelectSellChange_Chaser()//追擊者(小狗)
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
            ChaserObjList[5].SetActive(true);
        }
        else if (ChaserObjList[5].activeSelf == true)
        {
            ChaserObjList[5].SetActive(false);
            ChaserObjList[6].SetActive(true);
        }
        else if (ChaserObjList[6].activeSelf == true)
        {
            ChaserObjList[6].SetActive(false);
            ChaserObjList[7].SetActive(true);
        }
        else if (ChaserObjList[7].activeSelf == true)
        {
            ChaserObjList[7].SetActive(false);
            ChaserObjList[8].SetActive(true);
        }
        else if (ChaserObjList[8].activeSelf == true)
        {
            ChaserObjList[8].SetActive(false);
            ChaserObjList[9].SetActive(true);
        }
        else if (ChaserObjList[9].activeSelf == true)
        {
            ChaserObjList[9].SetActive(false);
            ChaserObjList[10].SetActive(true);
        }
        else if (ChaserObjList[10].activeSelf == true)
        {
            ChaserObjList[10].SetActive(false);
            ChaserObjList[1].SetActive(true);
        }
        AllRunkEvent();
        print("Chaser換角色");
    }

    /*▲▲▲▲▲購買角色選單事件▲▲▲▲▲*/


    /*▼▼▼▼▼關卡選單事件▼▼▼▼▼*/

    public void SelectLevelEvent()
    {
        print("開啟購買關卡選單");
        LevelChangeEventObj.SetActive(true);
        LevelChangeEventObj.GetComponent<LevelSellEvent>().VolcanicBtn  .GetComponent<Button>().enabled = true;
        LevelChangeEventObj.GetComponent<LevelSellEvent>().IceFieldBtn  .GetComponent<Button>().enabled = true;
        LevelChangeEventObj.GetComponent<LevelSellEvent>().DesertBtn    .GetComponent<Button>().enabled = true;
        LevelChangeEventObj.GetComponent<LevelSellEvent>().NightBtn     .GetComponent<Button>().enabled = true;
        LevelChangeEventObj.GetComponent<LevelSellEvent>().DayBtn       .GetComponent<Button>().enabled = true;
        //LevelFalseTurePanelObj.SetActive(true);
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


    private void AllRunkEvent()
    {
        for (int i = 1; i <= 10; i++)
        {
            ChaserObjList[i].GetComponent<Animator>().SetBool("Run", true);
            try
            {
                PlayerObjList[i].GetComponent<Animator>().SetBool("Run", true);
                ParentObjList[i].GetComponent<Animator>().SetBool("Run", true);
                
            }
            catch
            {
                print("Player&Parent" +i+"=None.");
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i < 5; i++)
        {
            ParentObjList[i].GetComponent<Animator>().SetBool("Run", true);
            PlayerObjList[i].GetComponent<Animator>().SetBool("Run", true);
            ChaserObjList[i].GetComponent<Animator>().SetBool("Run", true);
        }
        ParentObjList[2].SetActive(false);
        ParentObjList[3].SetActive(false);
        ParentObjList[4].SetActive(false);
        ParentObjList[5].SetActive(false);
        ParentObjList[6].SetActive(false);
        ParentObjList[7].SetActive(false);

        PlayerObjList[2].SetActive(false);
        PlayerObjList[3].SetActive(false);
        PlayerObjList[4].SetActive(false);
        PlayerObjList[5].SetActive(false);
        PlayerObjList[6].SetActive(false);
        PlayerObjList[7].SetActive(false);

        ChaserObjList[2].SetActive(false);
        ChaserObjList[3].SetActive(false);
        ChaserObjList[4].SetActive(false);
        ChaserObjList[5].SetActive(false);
        ChaserObjList[6].SetActive(false);
        ChaserObjList[7].SetActive(false);
        ChaserObjList[8].SetActive(false);
        ChaserObjList[9].SetActive(false);
        ChaserObjList[10].SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
