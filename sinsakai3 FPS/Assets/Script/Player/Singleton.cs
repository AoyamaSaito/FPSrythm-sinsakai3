using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public static Singleton playerInstance = default;
    void Awake()
    {
        if (playerInstance)
        {
            // インスタンスが既にある場合は、破棄する
            Destroy(this.gameObject);
        }
        else
        {
            // このクラスのインスタンスが無かった場合は、自分を DontDestroyOnload に置く
            playerInstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
