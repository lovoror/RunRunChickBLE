using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggerEvent : MonoBehaviour
{
    public IsportGameController _IsportGameController;
    public GameObject ChickObj, MissingPointObj, MissingEffect, TouchParentTextObj, TouchChaserTextObj;
    public Animation CoinTextObj;
    public Collider RunnerCollider;
    public AudioClipManagers _AudioClipManagers;
    public AudioSource TouchChickenSFX;

    void Awake()
    {
        //RunnerCollider = this.GetComponent<SphereCollider>();
        //MissingEffect = GameObject.Find("dizzy_effect");
        TouchParentTextObj.SetActive(false);
        TouchChaserTextObj.SetActive(false);
        CoinTextObj.enabled = false;
        MissingEffect.SetActive(false);
    }
    private void Start()
    {
        ChickObj = _IsportGameController.PlayerObjList[0];
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            StartCoroutine(EatCoinEvent(other.gameObject));
            _IsportGameController.CoinCount++;

        }
        if (other.tag == "Parent")
        {
            StartCoroutine(Pause2SecondRunnerFastEvent());
            _IsportGameController.FastMissing++;
            print("發生碰撞母雞");

        }
        if (other.tag == "Chaser")
        {
            StartCoroutine(Pause2SecondRunnerSlowEvent());
            _IsportGameController.SlowMissing++;
            print("發生碰撞小狗");

        }

    }
    IEnumerator EatCoinEvent(GameObject TheCoin)
    {
        TouchChickenSFX.PlayOneShot(_AudioClipManagers.SFX[2]);
        TheCoin.GetComponent<MeshRenderer>().enabled = false;
        TheCoin.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(3f);
        TheCoin.GetComponent<MeshRenderer>().enabled = true;
        TheCoin.GetComponent<SphereCollider>().enabled = true;
    }

    IEnumerator Pause2SecondRunnerFastEvent()//撞到母雞
    {
        TouchChickenSFX.PlayOneShot(_AudioClipManagers.SFX[0]);
        RunnerCollider.enabled = false;
        _IsportGameController.SetRpm = false;
        if (_IsportGameController.CoinCount - 10 <= 0)
        {
            _IsportGameController.CoinCount = 0;
        }
        else
        {
            _IsportGameController.CoinCount = _IsportGameController.CoinCount - 10;
        }
        ChickObj.transform.Translate(Vector3.up * Time.deltaTime * 20, MissingPointObj.transform);
        MissingEffect.SetActive(true);
        TouchParentTextObj.SetActive(true);
        CoinTextObj.enabled = true;
        _IsportGameController.RunnerAnimator.SetTrigger("Jump");
        _IsportGameController.PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 0;
        yield return new WaitForSeconds(0.25f);
        TouchChickenSFX.PlayOneShot(_AudioClipManagers.SFX[3]);
        _IsportGameController.PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 5;//測試用
        yield return new WaitForSeconds(1.75f);
        TouchParentTextObj.SetActive(false);
        CoinTextObj.enabled = false;
        MissingEffect.SetActive(false);
        _IsportGameController.SetRpm = true;
        RunnerCollider.enabled = true;
    }
    IEnumerator Pause2SecondRunnerSlowEvent()//撞到小狗
    {
        TouchChickenSFX.PlayOneShot(_AudioClipManagers.SFX[1]);
        RunnerCollider.enabled = false;
        _IsportGameController.SetRpm = false;
        if (_IsportGameController.CoinCount - 10 <= 0)
        {
            _IsportGameController.CoinCount = 0;
        }
        else
        {
            _IsportGameController.CoinCount = _IsportGameController.CoinCount - 10;
        }
        ChickObj.transform.Translate(Vector3.up * Time.deltaTime * 20, MissingPointObj.transform);
        MissingEffect.SetActive(true);
        TouchChaserTextObj.SetActive(true);
        CoinTextObj.enabled = true;
        _IsportGameController.RunnerAnimator.SetTrigger("Jump");
        _IsportGameController.PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 10;
        yield return new WaitForSeconds(0.25f);
        TouchChickenSFX.PlayOneShot(_AudioClipManagers.SFX[3]);
        _IsportGameController.PlayerObjList[0].GetComponent<SplineFollower>().followSpeed = 5;//測試用
        yield return new WaitForSeconds(1.75f);
        TouchChaserTextObj.SetActive(false);
        CoinTextObj.enabled = false;
        MissingEffect.SetActive(false);
        _IsportGameController.SetRpm = true;
        RunnerCollider.enabled = true;
    }
}
