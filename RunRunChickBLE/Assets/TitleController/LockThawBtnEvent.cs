using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockThawBtnEvent : MonoBehaviour
{
    public TitleController _TitleController;
    public GameObject Yes_No_BtnObj;
    public Animation CoinPanelObj;
    string SaleList=null;
    int Value=0;
    int Gold=0;

    public void MapThawBtnEvent(int _value)
    {
        Yes_No_BtnObj.SetActive(true);
        SaleList = "Map";
        Value = _value;
        Gold = 200;
        print("此地圖需消耗:"+Gold);
    }

    public void ParentThawBtnEvent(int _value)
    {
        Yes_No_BtnObj.SetActive(true);
        SaleList = "Map";
        Value = _value;
        Gold = 200;
        print("此地圖需消耗:" + Gold);
    }


    public void Yes_No_BtnEvent(bool YesNo)
    {
        if ((YesNo == true)&&(GlobalValue.MyCoin>= Gold))
        {
            if (SaleList == "Map")
            {
                _TitleController._ChickSaleStatus.SaleMapList[Value] = 1;
                GlobalValue.MyCoin = GlobalValue.MyCoin - Gold;
                _TitleController.CoinValueObj.GetComponent<Text>().text = "" + GlobalValue.MyCoin;
                _TitleController.SetJsonData_Android();
                _TitleController.MapLock[Value].gameObject.SetActive(false);
            }
            else if (SaleList == "Parent")
            {
                _TitleController._ChickSaleStatus.SaleParentList[Value] = 1;
                GlobalValue.MyCoin = GlobalValue.MyCoin - Gold;
                _TitleController.CoinValueObj.GetComponent<Text>().text = "" + GlobalValue.MyCoin;
                _TitleController.SetJsonData_Android();
            }
            else if (SaleList == "Player")
            {
                _TitleController._ChickSaleStatus.SalePlayerList[Value] = 1;
                GlobalValue.MyCoin = GlobalValue.MyCoin - Gold;
                _TitleController.CoinValueObj.GetComponent<Text>().text = "" + GlobalValue.MyCoin;
                _TitleController.SetJsonData_Android();
            }
            else if (SaleList == "Chaser")
            {
                _TitleController._ChickSaleStatus.SaleChasertList[Value] = 1;
                GlobalValue.MyCoin = GlobalValue.MyCoin - Gold;
                _TitleController.CoinValueObj.GetComponent<Text>().text = "" + GlobalValue.MyCoin;
                _TitleController.SetJsonData_Android();
            }
            Yes_No_BtnObj.SetActive(false);
        }
        else
        {
            Yes_No_BtnObj.SetActive(false);
        }
        CoinPanelObj.Play("TitleCoinShowAnim");
    }
}
