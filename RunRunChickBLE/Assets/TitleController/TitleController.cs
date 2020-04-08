using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleController : MonoBehaviour
{
    [Header("")]
    public TitleController instance;

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

        CoinObj                     = GameObject.Find("CoinPanelObj");

        LevelFalseTurePanelObj      = GameObject.Find("LevelFalseTurePanelObj");
        LevelSellTrueBtn            = GameObject.Find("LevelTrueButton");
        LevelSellFalseBtn           = GameObject.Find("LevelFalseButton");

        SellFalseTurePanelObj       = GameObject.Find("SellFalseTurePanelObj");
        SellTrueBtn                 = GameObject.Find("SellTrueButton");
        SellFalseBtn                = GameObject.Find("SellFalseButton");

        LevelFalseTurePanelObj      .SetActive(false);
        SellFalseTurePanelObj       .SetActive(false);
        CoinObj                     .SetActive(false);

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
