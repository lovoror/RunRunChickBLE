using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonTest : MonoBehaviour
{
    public TitleController _TitleController;
    public void JsonSvaeTest()
    {
        _TitleController.SetJsonData_Android();
    }
    public void CoinAdd()
    {
        GlobalValue.MyCoin= GlobalValue.MyCoin+1;
        _TitleController.CoinValueObj.GetComponent<Text>().text = "" + GlobalValue.MyCoin;


    }
}
