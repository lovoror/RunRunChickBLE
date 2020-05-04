using Dreamteck.Splines;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public ChickSaleStatus _ChickSaleStatus;

    [Header("控件參數取用代理名稱")]
    public TitleController instance;

    [Header("玩家目標物件(母雞)")]          //ParentObjList[0]
    public GameObject[] ParentObjList;      //PlayerObjList[0]
    public GameObject ParentCanvasObj;      //ChaserObjList[0]

    [Header("玩家物件(雛雞)")]
    public GameObject[] PlayerObjList;
    public GameObject PlayerCanvasObj;

    [Header("追擊玩家物件(小狗)")]
    public GameObject[] ChaserObjList;
    public GameObject ChaserCanvasObj;


    [Header("金幣顯示物件")]
    public GameObject CoinObj, CoinValueObj;

    [Header("解鎖圖示物件")]
    public GameObject[] MapLock, ParentLock, PlayerLock, ChaserLock;

    [Header("門物件")]
    public GameObject LevelChangeEventObj;//當門關掉時就必須開啟動畫
    public GameObject DoorAnimEvent;

    [Header("開始遊戲按鈕物件")]
    public GameObject StartGameBtn_Go, StartGameBtn_Goback;

    // Start is called before the first frame update
    void Awake()
    {

        GetJsonData();
        
        //if (_ChickSaleStatus.GameLevel == 0)
        //{
        //   for (int i = 1; i < 11; i++)
        //   {
        //       if (i < 8)
        //       {
        //           ParentObjList[i].GetComponent<Animator>().SetBool("Run", true);
        //           PlayerObjList[i].GetComponent<Animator>().SetBool("Run", true);
        //           ChaserObjList[i].GetComponent<Animator>().SetBool("Run", true);
        //           ParentObjList[i].SetActive(false);
        //           PlayerObjList[i].SetActive(false);
        //           ChaserObjList[i].SetActive(false);
        //       }
        //       else
        //       {
        //           ChaserObjList[i].GetComponent<Animator>().SetBool("Run", true);
        //           ChaserObjList[i].SetActive(false);
        //       }
        //      
        //   }
        if (_ChickSaleStatus.GameLevel >= 1)
        {
            //開啟選關卡功能
            DoorAnimEvent.GetComponent<Animation>().enabled = true;
        }
    }

    void Start()
    {
        //開始開關(預先開啟GO)
        //StartGameBtn_Go.SetActive(false);
        AllRunkEvent();
        StartGameBtn_Goback.SetActive(false);

    }
    public void StartGameEvent()
    {

        print("開始遊戲");
        ParentCanvasObj.SetActive(false);
        PlayerCanvasObj.SetActive(false);
        ChaserCanvasObj.SetActive(false);
        StartGameBtn_Go.SetActive(false);
        StartGameBtn_Goback.SetActive(false);
        switch (GlobalValue.Map)
        {
            case 0:
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
                PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
                print("白天森林關卡");
                break;
            case 1:
                ParentObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                PlayerObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                ChaserObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                print("火山關卡");
                break;
            case 2:
                ParentObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                PlayerObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                ChaserObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                print("冰原關卡");
                break;
            case 3:
                ParentObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                PlayerObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                ChaserObjList[0].GetComponent<SplineFollower>().spline = GameObject.Find("Spline2").GetComponent<SplineComputer>();
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 6;
                print("沙漠關卡");
                break;
            case 4:
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
                PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
                print("晚上森林關卡");
                break;

        }
        StartCoroutine(SetStartToMap());
    }
    public IEnumerator SetStartToMap()
    {
        StartGameBtn_Go.SetActive(false);
        StartGameBtn_Goback.SetActive(false);
        yield return new WaitForSeconds(3f);//給小雞們跑出畫面的時間
        switch (GlobalValue.Map)
        {
            case 0:
                SceneManager.LoadScene("Forest_Scene");
                //print("白天森林關卡");
                break;
            case 1:
                yield return new WaitForSeconds(3f);//給小雞們跑出畫面的時間
                SceneManager.LoadScene("LavaMaze_Scene");
                //print("火山關卡");
                break;
            case 2:
                yield return new WaitForSeconds(3f);//給小雞們跑出畫面的時間
                SceneManager.LoadScene("FrostwindMaze");
                //print("冰原關卡");
                break;
            case 3:
                yield return new WaitForSeconds(3f);//給小雞們跑出畫面的時間
                SceneManager.LoadScene("DesertMaze_Scene");
                //print("沙漠關卡");
                break;
            case 4:
                SceneManager.LoadScene("ForestNight_Scene");
                //print("晚上森林關卡");
                break;

        }
    }

    /*▼▼▼▼▼購買角色選單事件▼▼▼▼▼*/
    //GlobalValue.ParnetCharacter=1;
    //GlobalValue.PlayerCharacter=1;
    //GlobalValue.ChaserCharacter=1;

    public void SelectSellChange_Player()//玩家(小雞)
    {
        if (PlayerObjList[1].activeSelf == true)
        {
            PlayerObjList[1].SetActive(false);
            PlayerObjList[2].SetActive(true);
            GlobalValue.PlayerCharacter = 2;
        }
        else if (PlayerObjList[2].activeSelf == true)
        {
            PlayerObjList[2].SetActive(false);
            PlayerObjList[3].SetActive(true);
            GlobalValue.PlayerCharacter = 3;
        }
        else if (PlayerObjList[3].activeSelf == true)
        {
            PlayerObjList[3].SetActive(false);
            PlayerObjList[4].SetActive(true);
            GlobalValue.PlayerCharacter = 4;
        }
        else if (PlayerObjList[4].activeSelf == true)
        {
            PlayerObjList[4].SetActive(false);
            PlayerObjList[5].SetActive(true);
            GlobalValue.PlayerCharacter = 5;
        }
        else if (PlayerObjList[5].activeSelf == true)
        {
            PlayerObjList[5].SetActive(false);
            PlayerObjList[6].SetActive(true);
            GlobalValue.PlayerCharacter = 6;
        }
        else if (PlayerObjList[6].activeSelf == true)
        {
            PlayerObjList[6].SetActive(false);
            PlayerObjList[7].SetActive(true);
            GlobalValue.PlayerCharacter = 7;
        }
        else if (PlayerObjList[7].activeSelf == true)
        {
            PlayerObjList[7].SetActive(false);
            PlayerObjList[1].SetActive(true);
            GlobalValue.PlayerCharacter = 1;
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
            GlobalValue.ParnetCharacter = 2;
        }
        else if (ParentObjList[2].activeSelf == true)
        {
            ParentObjList[2].SetActive(false);
            ParentObjList[3].SetActive(true);
            GlobalValue.ParnetCharacter = 3;
        }
        else if (ParentObjList[3].activeSelf == true)
        {
            ParentObjList[3].SetActive(false);
            ParentObjList[4].SetActive(true);
            GlobalValue.ParnetCharacter = 4;
        }
        else if (ParentObjList[4].activeSelf == true)
        {
            ParentObjList[4].SetActive(false);
            ParentObjList[5].SetActive(true);
            GlobalValue.ParnetCharacter = 5;
        }
        else if (ParentObjList[5].activeSelf == true)
        {
            ParentObjList[5].SetActive(false);
            ParentObjList[6].SetActive(true);
            GlobalValue.ParnetCharacter = 6;
        }
        else if (ParentObjList[6].activeSelf == true)
        {
            ParentObjList[6].SetActive(false);
            ParentObjList[7].SetActive(true);
            GlobalValue.ParnetCharacter = 7;
        }
        else if (ParentObjList[7].activeSelf == true)
        {
            ParentObjList[7].SetActive(false);
            ParentObjList[1].SetActive(true);
            GlobalValue.ParnetCharacter = 1;
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
            GlobalValue.ChaserCharacter = 2;
        }
        else if (ChaserObjList[2].activeSelf == true)
        {
            ChaserObjList[2].SetActive(false);
            ChaserObjList[3].SetActive(true);
            GlobalValue.ChaserCharacter = 3;
        }
        else if (ChaserObjList[3].activeSelf == true)
        {
            ChaserObjList[3].SetActive(false);
            ChaserObjList[4].SetActive(true);
            GlobalValue.ChaserCharacter = 4;
        }
        else if (ChaserObjList[4].activeSelf == true)
        {
            ChaserObjList[4].SetActive(false);
            ChaserObjList[5].SetActive(true);
            GlobalValue.ChaserCharacter = 5;
        }
        else if (ChaserObjList[5].activeSelf == true)
        {
            ChaserObjList[5].SetActive(false);
            ChaserObjList[6].SetActive(true);
            GlobalValue.ChaserCharacter = 6;
        }
        else if (ChaserObjList[6].activeSelf == true)
        {
            ChaserObjList[6].SetActive(false);
            ChaserObjList[7].SetActive(true);
            GlobalValue.ChaserCharacter = 7;
        }
        else if (ChaserObjList[7].activeSelf == true)
        {
            ChaserObjList[7].SetActive(false);
            ChaserObjList[8].SetActive(true);
            GlobalValue.ChaserCharacter = 8;
        }
        else if (ChaserObjList[8].activeSelf == true)
        {
            ChaserObjList[8].SetActive(false);
            ChaserObjList[9].SetActive(true);
            GlobalValue.ChaserCharacter = 9;
        }
        else if (ChaserObjList[9].activeSelf == true)
        {
            ChaserObjList[9].SetActive(false);
            ChaserObjList[10].SetActive(true);
            GlobalValue.ChaserCharacter = 10;
        }
        else if (ChaserObjList[10].activeSelf == true)
        {
            ChaserObjList[10].SetActive(false);
            ChaserObjList[1].SetActive(true);
            GlobalValue.ChaserCharacter = 1;
        }
        AllRunkEvent();
        print("Chaser換角色");
    }

    /*▲▲▲▲▲購買角色選單事件▲▲▲▲▲*/


    /*▼▼▼▼▼關卡選單事件▼▼▼▼▼*/

    public void SelectLevelEvent()
    {
        if (_ChickSaleStatus.GameLevel >= 0)
        {

            print("開啟購買關卡選單");

            LevelChangeEventObj.SetActive(true);
            for (int i = 0; i <= 3; i++)
            {
                if (_ChickSaleStatus.SaleMapList[i] == 1)
                {
                    MapLock[i].gameObject.SetActive(false);
                }
            }
            LevelChangeEventObj.GetComponent<LevelSellEvent>().VolcanicBtn.GetComponent<Button>().enabled = true;
            LevelChangeEventObj.GetComponent<LevelSellEvent>().IceFieldBtn.GetComponent<Button>().enabled = true;
            LevelChangeEventObj.GetComponent<LevelSellEvent>().DesertBtn.GetComponent<Button>().enabled = true;
            LevelChangeEventObj.GetComponent<LevelSellEvent>().NightBtn.GetComponent<Button>().enabled = true;
            LevelChangeEventObj.GetComponent<LevelSellEvent>().DayBtn.GetComponent<Button>().enabled = true;
        }

    }

    /*▲▲▲▲▲關卡選單事件▲▲▲▲▲*/


    private void AllRunkEvent()
    {
        for (int i = 1; i <= 10; i++)
        {
            
            try
            {
                if (ChaserObjList[i].activeSelf==true)
                {
                    ChaserObjList[i].GetComponent<Animator>().SetBool("Run", true);
                }
                if (PlayerObjList[i].activeSelf == true)
                {
                    PlayerObjList[i].GetComponent<Animator>().SetBool("Run", true);
                }
                if (ParentObjList[i].activeSelf == true)
                {
                    ParentObjList[i].GetComponent<Animator>().SetBool("Run", true);
                }
            }
            catch
            {
                print("Player&Parent" +i+"=None.");
            }
        }
    }

    //儲存數據事件
     public void GetJsonData()
     {
          try
          {//適用於Android的存檔機智;再編輯器中直接存取catch段
              StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, "SaveChickData"));
              string loadJson = file.ReadToEnd();
              file.Close();
              _ChickSaleStatus = JsonUtility.FromJson<ChickSaleStatus>(loadJson);
            print("Application.persistentDataPath裡取出的資料!!");
          }
          catch
          {//如果讀檔不成功;從Resources資料夾裡取出初始資料
              string Json = "";
              Json = Resources.Load<TextAsset>("ChickData").text;
              _ChickSaleStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<ChickSaleStatus>(Json);
            print("Resources裡取出的資料!!");
              
          }
          
          GlobalValue.MyCoin = _ChickSaleStatus.Coin;
          print("目前金幣數為:" + _ChickSaleStatus.Coin);
          print("目前進程:" + _ChickSaleStatus.GameLevel);
          CoinValueObj.GetComponent<Text>().text = "" + GlobalValue.MyCoin;


     }

    public void SetJsonData_Android()//Android版JSON存取方案
    {
        //將_RankListState轉換成json格式的字串
        _ChickSaleStatus.Coin = GlobalValue.MyCoin;
        string saveString = JsonUtility.ToJson(_ChickSaleStatus);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, "SaveChickData"));
        file.Write(saveString);
        file.Close();
    }


    public void SetJsonData()//PC版JSON存取方案
     {
         try
         {
             if (_ChickSaleStatus != null)
             {
                try
                {
                    _ChickSaleStatus.Coin = GlobalValue.MyCoin;
                    string NewData = Newtonsoft.Json.JsonConvert.SerializeObject(_ChickSaleStatus);
                    string NewDataPath = Application.dataPath + @"/Resources/ChickData.json";
                    FileInfo file = new FileInfo(NewDataPath);
                    StreamWriter sw = file.CreateText();
                    sw.WriteLine(NewData);
                    sw.Close();
                    sw.Dispose();

#if UNITY_EDITOR
                    UnityEditor.AssetDatabase.Refresh();
#endif
                    print("輸出成功,金幣數累計至:" + GlobalValue.MyCoin);
                }
                catch
                {
                    print("輸出失敗!!");
                }


             }
         }
         catch
         {
             //print("jsonApi not work!");
         }

     }
     //儲存數據事件

}

[Serializable]
public class ChickSaleStatus
{
    [SerializeField]
    public int Coin;
    public int GameLevel;//關卡進程
    public int[] SalePlayerList =new int[7];
    public int[] SaleParentList = new int[7];
    public int[] SaleChasertList = new int[10];
    public int[] SaleMapList = new int[4];
}
