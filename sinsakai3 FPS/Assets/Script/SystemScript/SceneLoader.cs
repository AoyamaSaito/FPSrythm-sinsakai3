using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを変更したいときにとりあえず呼べるやつ
/// </summary>
public class SceneLoader : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            MySceneLode("MainScene 1");
        }
    }
    public void MySceneLode(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
