using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// シーンを変更したいときにとりあえず呼べるやつ
/// </summary>
public class SceneLoader : MonoBehaviour
{
    public void MySceneLode(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
