using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IsportTimeController : MonoBehaviour
{
    public int AllTime;//表示微秒數
    public Text ValueText;
    public bool CumulativeSwitch;
    int Mins, Secs;//分鐘, 秒數, 微秒數
    string MinsText, SecsText;//數字補0
    public IsportGameController _IsportGameController;
    public Collider _PlayerTriggerEventTheCollider;
    public GameObject SetResultController;
    // Start is called before the first frame update
    void Start()
    {
        AllTime = GlobalValue.ExerciseTime;
        Mins = 0;
        Secs = 0;
        CumulativeSwitch = true;
        AllTime2TextData();
        ValueText.text = "" + MinsText + ":" + SecsText;
        if ((Mins == 0) && (Secs <= 0))
        {
            RunGameOver();
            CumulativeSwitch = false;
        }
    }

    public IEnumerator GameStartTimeCumulative()
    {
        while (CumulativeSwitch)
        {
            yield return new WaitForSeconds(1f);
            AllTime = AllTime - 1;
            AllTime2TextData();
            ValueText.text = "" + MinsText + ":" + SecsText;
            
            if ((Mins == 0) && (Secs <= 0))
            {
                RunGameOver();
                CumulativeSwitch = false;

            }
        }
    }

    public void AllTime2TextData()//時間轉換碼錶表示法(列排行榜時會Call到)
    {
        Mins = AllTime / 60;          //微秒換算成分鐘
        Secs = AllTime % 60;    //微秒換算成秒數
        if (Mins < 10)
        {
            MinsText = "0" + Mins;
        }
        else
        {
            MinsText = "" + Mins;
        }

        if (Secs < 10)
        {
            SecsText = "0" + Secs;
        }
        else
        {
            SecsText = "" + Secs;
        }

    }

    public void RunGameOver()
    {
        SetResultController.SetActive(true);
        _PlayerTriggerEventTheCollider.enabled=false;
        StartCoroutine(_IsportGameController.SetGameOver());
    }

}
