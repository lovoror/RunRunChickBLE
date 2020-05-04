using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameTriggerEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("觸碰到切換關卡");
        if (other.CompareTag ("Chaser"))
        {
            print("觸碰到切換關卡--森林關");
            SceneManager.LoadScene("Forest_Scene");
        }
    }
}
