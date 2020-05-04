using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck.Splines;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class IsportGameController : MonoBehaviour
{
    [Header("玩家目標物件(母雞)")]
    public GameObject[] ParentObjList;
    public GameObject ParentCanvasObj;

    [Header("玩家物件(雛雞)")]
    public GameObject[] PlayerObjList;
    public GameObject PlayerCanvasObj;

    [Header("追擊玩家物件(小狗)")]
    public GameObject[] ChaserObjList;
    public GameObject ChaserCanvasObj;

    public GameObject SpeedObj ,SpeedValueTextObj, CaloriObj, CaloriValueTextObj, CoinValueTextObj;
    
    public GameObject CenterTextObj, ExitButtonObj, ResultPanelObj, FadePanelObj;
    public Animator ParentAnimator, RunnerAnimator, ChaserAnimator;
    public AudioSource ChickenSFX;
    public IsportTimeController _IsportTimeController;
    public AudioClipManagers _AudioClipManagers;
    public int CoinCount, FastMissing, SlowMissing, Rpm;
    public float FollowSpeed;
    public bool SetRpm, DebugFlag;
    //public GameObject[] Chick;
    // Start is called before the first frame update
    void Awake()
    {
        //ParentObjList       = new GameObject[8];
        //PlayerObjList       = new GameObject[8];
        //ChaserObjList       = new GameObject[11];
        //ParentCanvasObj     = GameObject.Find("ParentCanvas");
        //PlayerCanvasObj     = GameObject.Find("PlayerCanvas");
        //ChaserCanvasObj     = GameObject.Find("ChaserCanvas");
        //ParentCanvasObj     .SetActive(false);
        //PlayerCanvasObj     .SetActive(false);
        //ChaserCanvasObj     .SetActive(false);
        //
        //ParentObjList[0]    = GameObject.Find("ParentObj");
        //ParentObjList[1]    = GameObject.Find("Chicken");
        //ParentObjList[2]    = GameObject.Find("Sheep");
        //ParentObjList[3]    = GameObject.Find("Alpaca");
        //ParentObjList[4]    = GameObject.Find("Horse");
        //ParentObjList[5]    = GameObject.Find("PenguinB1");
        //ParentObjList[6]    = GameObject.Find("PenguinB2");
        //ParentObjList[7]    = GameObject.Find("PenguinB3");
        //
        //PlayerObjList[0]    = GameObject.Find("PlayerObj");
        //PlayerObjList[1]    = GameObject.Find("Cute_Bird_00");
        //PlayerObjList[2]    = GameObject.Find("Cute_Bird_01");
        //PlayerObjList[3]    = GameObject.Find("Cute_Bird_03");
        //PlayerObjList[4]    = GameObject.Find("Cute_Bird_05");
        //PlayerObjList[5]    = GameObject.Find("Cute_Bird_10");
        //PlayerObjList[6]    = GameObject.Find("Cute_Bird_12");
        //PlayerObjList[7]    = GameObject.Find("Cute_Bird_14");
        //
        //ChaserObjList[0]    = GameObject.Find("ChaserObj");
        //ChaserObjList[1]    = GameObject.Find("Dog");
        //ChaserObjList[2]    = GameObject.Find("GoatB");
        //ChaserObjList[3]    = GameObject.Find("Pig");
        //ChaserObjList[4]    = GameObject.Find("SheepC");
        //ChaserObjList[5]    = GameObject.Find("Dino04");
        //ChaserObjList[6]    = GameObject.Find("Dino11");
        //ChaserObjList[7]    = GameObject.Find("Dino25");
        //ChaserObjList[8]    = GameObject.Find("DragonSD_32");
        //ChaserObjList[9]    = GameObject.Find("DragonSD_01");
        //ChaserObjList[10]   = GameObject.Find("DragonSD_13");
        //
        //SpeedObj            = GameObject.Find("SpeedObj");
        //SpeedValueTextObj   = GameObject.Find("SpeedValue");
        //CaloriObj           = GameObject.Find("CaloriObj");
        //CaloriValueTextObj  = GameObject.Find("CaloriValue");
        //CoinValueTextObj    = GameObject.Find("CoinValue");
        //CenterTextObj       = GameObject.Find("CenterText");
        //
        //ExitButtonObj       = GameObject.Find("ExitButton");
        SpeedObj            .SetActive(false);
        CaloriObj           .SetActive(false);
        ExitButtonObj       .SetActive(false);
        //
        //FadePanelObj = GameObject.Find("FadePanelObj");
        FadePanelObj.SetActive(false);

        CoinCount = 0;
        FastMissing = 0;
        SlowMissing = 0;
        SetRpm = false;
        DebugFlag = false;

        for (int i = 1; i < 11; i++)
        {
            try
            {
                if (i < 8)
                {
                    PlayerObjList[i].SetActive(false);
                    ParentObjList[i].SetActive(false);
                }
                ChaserObjList[i].SetActive(false);
                
            }
            catch
            {
                //print("PlayerObjList" + i +"is None.");
                //print("ChaserObjList" + i + "is None.");
                //print("ParentObjList" + i + "is None.");
            }
        }
        
        //GetJsonData();
    }

    // Start is called before the first frame update
    void Start()
    {
        ParentObjList[GlobalValue.ParnetCharacter].SetActive(true);
        PlayerObjList[GlobalValue.PlayerCharacter].SetActive(true);
        ChaserObjList[GlobalValue.ChaserCharacter].SetActive(true);
        ParentAnimator = ParentObjList[GlobalValue.ParnetCharacter].GetComponent<Animator>();
        RunnerAnimator = PlayerObjList[GlobalValue.PlayerCharacter].GetComponent<Animator>();
        ChaserAnimator = ChaserObjList[GlobalValue.ChaserCharacter].GetComponent<Animator>();
        StartCoroutine(Start3sec());
    }

    IEnumerator Start3sec()
    {
        CenterTextObj.GetComponent<Text>().text = "3";
        yield return new WaitForSeconds(1);
        CenterTextObj.GetComponent<Text>().text = "2";
        yield return new WaitForSeconds(1);
        CenterTextObj.GetComponent<Text>().text = "1";
        yield return new WaitForSeconds(1);
        CenterTextObj.GetComponent<Text>().text = "體能運動開始!";
        yield return new WaitForSeconds(0.5f);
        CenterTextObj.GetComponent<Text>().text = "";
        ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
        //PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
        ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 5;
        ParentAnimator.SetBool("Run", true);
        RunnerAnimator.SetBool("Run", true);
        ChaserAnimator.SetBool("Run", true);
        _IsportTimeController.CumulativeSwitch = true;
        StartCoroutine(_IsportTimeController.GameStartTimeCumulative());//控制時間開始計時
        SetRpm = true;
        ExitButtonObj.SetActive(true);
        //FadePanelObj.SetActive(false);
    }
    
    public IEnumerator SetGameOver()
    {
        //byte Resistance_byte = Convert.ToByte(GlobalValue.Resistance * 10);
        //byte[] over = { 0x55, 0xA4, 0x01, 0x00, 0x00, 0x90 };
        //SendBytes(over);//傳驅動關閉

        SetRpm = false;
        _IsportTimeController.CumulativeSwitch = false;
        ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 0;
        PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 0;
        ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 0;
        //ParentObjList[0].GetComponent<SplineFollower>().enabled=false;
        //PlayerObjList[0].GetComponent<SplineFollower>().enabled=false;
        //ChaserObjList[0].GetComponent<SplineFollower>().enabled=false;
        ParentAnimator.SetBool("Run", false);
        RunnerAnimator.SetBool("Run", false);
        ChaserAnimator.SetBool("Run", false);
        //ParentAnimator.SetBool("over", true);
        //RunnerAnimator.SetBool("over", true);
        //ChaserAnimator.SetBool("over", true);
        if (_IsportTimeController.AllTime > 0)//被點取離開時的事件處理
        {
            //FadePanelObj.GetComponent<Animation>().clip = ExitPlanelFadeOut;
            FadePanelObj.SetActive(true);
            yield return new WaitForSeconds(2.0f);
            SceneManager.LoadScene("GameTitle");

        }
        else//時間結束自然自然離開時的事件處理
        {
            ExitButtonObj.SetActive(false);
            //ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[5]);
            yield return new WaitForSeconds(2.0f);
        }
    }
    
    // Update is called once per frame
    void Update()//即時參數寫在這裡
    {
        //GlobalValue.MyCoin = CoinCount;
        SpeedValueTextObj   .GetComponent<Text>().text  = ""+ GlobalValue.Rpm;
        CaloriValueTextObj  .GetComponent<Text>().text  = "" + GlobalValue.Watt * 0.23f;
        CoinValueTextObj    .GetComponent<Text>().text  = "" + CoinCount;

        if ((SetRpm)&&(GlobalValue.Rpm>=0)&&(_IsportTimeController.CumulativeSwitch))
        {
            if (DebugFlag==false)
            {
                Rpm = GlobalValue.Rpm;
            }
            FollowSpeed = (float)Rpm / 10;
            PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = FollowSpeed;
            if ((FollowSpeed <= 5.9f) && (FollowSpeed > 4.5f))
            {
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = FollowSpeed;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = FollowSpeed;
            }
            else
            {
                ParentObjList[0].GetComponent<SplineFollower>().followSpeed = 5f;
                ChaserObjList[0].GetComponent<SplineFollower>().followSpeed = 5f;
            }
        }
        
    }

    //離開按鈕
    public void ExitButton() {
        ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
        ExitButtonObj.SetActive(false);
        StartCoroutine(SetGameOver());
    }
    //轉速開關按鈕
    private int RpmSwitchValue=0;
    public void RpmSwitchButton()
    {
        if (SpeedObj.activeSelf == false)
        {
            RpmSwitchValue++;
            if (RpmSwitchValue >= 5)
            {
                //print("轉速表開!");
                ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
                SpeedObj.SetActive(true);
            }
        }
        else
        {
            RpmSwitchValue++;
            if (RpmSwitchValue >= 10)
            {
                RpmSwitchValue = 0;
                //print("轉速表關!");
                ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[5]);
                SpeedObj.SetActive(false);
            }
        }
    }

    //點選自覺量表後離開
    public void RatingGrid_0Level()
    {
        //ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
        ResultPanelObj.SetActive(false);
        SetJsonData_Android();
        SceneManager.LoadScene("GameTitle");
    }
    public void RatingGrid_1Level()
    {
        //ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
        ResultPanelObj.SetActive(false);
        SetJsonData_Android();
        SceneManager.LoadScene("GameTitle");
    }
    public void RatingGrid_2Level()
    {
        //ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
        ResultPanelObj.SetActive(false);
        SetJsonData_Android();
        SceneManager.LoadScene("GameTitle");
    }
    public void RatingGrid_3Level()
    {
        //ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
        ResultPanelObj.SetActive(false);
        SetJsonData_Android();
        SceneManager.LoadScene("GameTitle");
    }
    public void RatingGrid_4Level()
    {
        //ChickenSFX.PlayOneShot(_AudioClipManagers.SFX[4]);
        ResultPanelObj.SetActive(false);
        SetJsonData_Android();
        SceneManager.LoadScene("GameTitle");
    }

    ChickSaleStatus _ChickSaleStatus;
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

    /*//儲存數據事件
    public void GetJsonData()
    {
        string Json = "";
        //JSON字串
        Json = Resources.Load<TextAsset>("ChickData").text;
        _ChickSaleStatus = Newtonsoft.Json.JsonConvert.DeserializeObject<ChickSaleStatus>(Json);
        GlobalValue.MyCoin = _ChickSaleStatus.Coin;
        //print("目前金幣數為:" + _ChickSaleStatus.Coin);
    }
    public void SetJsonData()
    {
        try
        {
            if (_ChickSaleStatus != null)
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
        }
        catch
        {
            //print("jsonApi not work!");
        }
        
    }
    *///儲存數據事件

}
