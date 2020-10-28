using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{

    public void PlayPressed()
    {
        SceneManager.LoadScene("TurretEnemyScene");
    }


    public void ExitPressed()
    {
        Application.Quit();
        Debug.Log("Exit pressed!");//Delete when release
    }
}
