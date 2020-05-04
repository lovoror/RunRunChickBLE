using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LRControllerEvent : MonoBehaviour
{
    public static float quickDoubleTabInterval = 0.15f;
    private static float lastTouchTime;//上一次點擊放開的時間
    private static float begainTime = 0f;//最初點擊時間
    private static float intervals;//間隔時間
    public static float holdingTime = 3;//按住多久才會達到滿的狀態

    private static Vector2 startPos = Vector2.zero;//觸碰起始點
    private static Vector2 endPos = Vector2.zero;//觸碰結束點
    private static Vector2 direction = Vector2.zero;//移動方向

    private static Touch lastTouch;//目前沒用到，不果主要是記錄上一次的觸碰

    public Text ShowTouchText;
    public SplineFollower Player;

    public GameObject LeftButton, RightButton;


    private void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            ShowTouchText.text="Touch Position : " + touch.position;
            switch (touch.phase)
            {
                case TouchPhase.Began://點下去的狀態
                    startPos = touch.position;
                    begainTime = Time.realtimeSinceStartup;

                    TouchOn();

                    break;


                case TouchPhase.Moved://手按住滑動的狀態
                    direction = touch.position - startPos;
                    intervals = Time.realtimeSinceStartup - begainTime;
                    Hold();
                    break;


                case TouchPhase.Ended://手離開螢幕時的狀態
                    KeyUp();
                    break;

                case TouchPhase.Stationary://手按住不動的狀態
                    intervals = Time.realtimeSinceStartup - begainTime;
                    Hold();
                    break;
                case TouchPhase.Canceled:
                    //TouchOn2();
                    break;

            }
        }
    }

    void TouchOn()
    {
        if ((Screen.width / 2 < startPos.x) && (Player.motion.offset.x < 1.5f))
        {
            Player.motion.offset = new Vector2(Player.motion.offset.x + Time.deltaTime * 5, 0);
            RightButton.GetComponent<Transform>().localScale = new Vector3(1.25f , 1.25f, 1.25f);
        }
        if ((Screen.width / 2 > startPos.x) && (Player.motion.offset.x > -1.5f))
        {
            Player.motion.offset = new Vector2(Player.motion.offset.x - Time.deltaTime * 5, 0);
            LeftButton.GetComponent<Transform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);
        }
    }

    //判斷按住事件是否成立，成立了以後要做什麼
    void Hold()
    {
        if ((Screen.width / 2 < startPos.x) && (Player.motion.offset.x < 1.5f))
        {
            Player.motion.offset = new Vector2(Player.motion.offset.x + Time.deltaTime * 5, 0);
        }
        if ((Screen.width / 2 > startPos.x) && (Player.motion.offset.x > -1.5f))
        {
            Player.motion.offset = new Vector2(Player.motion.offset.x - Time.deltaTime * 5, 0);
        }
    }
    //判斷放開事件是否成立，成立了以後要做什麼
    void KeyUp()
    {
        LeftButton.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        RightButton.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
    }
}
