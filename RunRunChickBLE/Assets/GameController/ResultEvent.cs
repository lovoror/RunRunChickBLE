using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultEvent : MonoBehaviour
{
    public Text MissValue, AllCoinValue, AssessText;
    public GameObject RPEObj, AssessObj, MissCountValueObj;
    public IsportGameController _IsportGameController;
    int AllMiss = 0;
    // Start is called before the first frame update
    void Start()
    {
        AllMiss= _IsportGameController.FastMissing + _IsportGameController.SlowMissing;
        //MissValue.text = "" + AllMiss;
        //AllCoinValue.text = "" + (_IsportGameController.CoinCount + GlobalValue.MyCoin);

        if (AllMiss!=0)
        {
            if (_IsportGameController.FastMissing < _IsportGameController.SlowMissing)
            {
                if (_IsportGameController.SlowMissing == 1)
                {
                    if (_IsportGameController.CoinCount >= 150)
                    {
                        AssessText.text = "視錢如命!!";
                    }
                    else
                    {
                        AssessText.text = "追撞事故!!";
                    }
                }
                else
                {
                    if (_IsportGameController.CoinCount >= 150)
                    {
                        AssessText.text = "視錢如命!!";
                    }
                    else
                    {
                        AssessText.text = "慢郎中!!";
                    }
                }

            }
            else if (_IsportGameController.FastMissing > _IsportGameController.SlowMissing)
            {
                if (_IsportGameController.SlowMissing == 1)
                {
                    if (_IsportGameController.CoinCount >= 150)
                    {
                        AssessText.text = "視錢如命!!";
                    }
                    else
                    {
                        AssessText.text = "追撞事故!!";
                    }
                }
                else
                {
                    if (_IsportGameController.CoinCount >= 150)
                    {
                        AssessText.text = "視錢如命!!";
                    }
                    else
                    {
                        AssessText.text = "急驚風!!";
                    }
                }
            }
            else
            {
                if (_IsportGameController.CoinCount >= 150)
                {
                    AssessText.text = "搶錢大作戰!!";
                }
                else
                {
                    AssessText.text = "跌跌撞撞!!";
                }

            }
        }
        else if (AllMiss == 0)
        {
            if (_IsportGameController.CoinCount >= 150)
            {
                AssessText.text = "成功發大財!";
            }
            else if ((_IsportGameController.CoinCount < 150) && (_IsportGameController.CoinCount >= 120))
            {
                AssessText.text = "酷跑高手!!!";
            }
            else if ((_IsportGameController.CoinCount < 120) && (_IsportGameController.CoinCount >= 90))
            {
                AssessText.text = "飛毛腿!";
            }
            else if ((_IsportGameController.CoinCount < 90) && (_IsportGameController.CoinCount > 10))
            {
                AssessText.text = "異類!";
            }
            else if (_IsportGameController.CoinCount < 10)
            {
                AssessText.text = "視金錢如糞土!!";
            }
        }
        StartCoroutine(SetResultEvent());
    }
    public IEnumerator SetResultEvent()
    {
        _IsportGameController.GetJsonData();
        yield return new WaitForSeconds(0.5f);
        AllCoinValue.text = "" + GlobalValue.MyCoin;
        for (int i = GlobalValue.MyCoin; i <= _IsportGameController.CoinCount + GlobalValue.MyCoin; i++)
        {
            AllCoinValue.text = "" + i;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(1.0f);
        GlobalValue.MyCoin = _IsportGameController.CoinCount + GlobalValue.MyCoin;
        MissValue.text = "" + AllMiss;
        MissCountValueObj.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        AssessObj.SetActive(true);
        _IsportGameController.SetJsonData_Android();
        yield return new WaitForSeconds(2.0f);
        RPEObj.SetActive(true);
    }
}
