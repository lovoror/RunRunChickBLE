using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightSwitch : MonoBehaviour
{
    public Animation WorldLight;                                        //燈光變暗
    public AnimationClip[] SwitchAnim = new AnimationClip[2];
    public Material[] DayNight = new Material[2];                       //夜晚天空
    public GameObject ChickenLight;                                     //小雞區域光
    public Skybox WorldSkybox;
    public bool SwitchFlag=false;
    public Image thisIcon;
    public Sprite DaySun;
    public Sprite NightMoon;


    public void ToSwitch() {
        if (this.gameObject.GetComponent<Scrollbar>().value == 1)
        {
            WorldLight.clip = SwitchAnim[1];
            WorldLight.Play();
            ChickenLight.SetActive(true);
            WorldSkybox.material = DayNight[1];
            SwitchFlag = true;
            thisIcon.sprite = NightMoon; 
        }
        else if (this.gameObject.GetComponent<Scrollbar>().value == 0)
        {
            WorldLight.clip = SwitchAnim[0];
            WorldLight.Play();
            WorldLight.clip = SwitchAnim[0];
            ChickenLight.SetActive(false);
            WorldSkybox.material = DayNight[0];
            SwitchFlag = false;
            thisIcon.sprite = DaySun;
        }
        else { }
      
    }
}
