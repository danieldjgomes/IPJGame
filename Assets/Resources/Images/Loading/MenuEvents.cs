using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuEvents : MonoBehaviour
{
    public String sceneName;

    public void Quit()
    {
        Application.Quit(0);
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }



}
