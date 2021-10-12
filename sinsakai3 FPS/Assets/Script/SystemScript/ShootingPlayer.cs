using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayer : MonoBehaviour
{
    [SerializeField] public float rythm = 0.8f;    //リズム
    [SerializeField] float interval = 0.3f;     //譜面の猶予時間
    [SerializeField] ScoreManager scoreMn;

    MagagineManager mm;

    float count;

    bool isShot1 = false;
    bool isShot2 = false;

    // Start is called before the first frame update
    void Start()
    {
        mm = GameObject.Find("MagagineManager").GetComponent<MagagineManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    /// <summary>
    /// クリックで射撃する処理
    /// </summary>
    void Fire()
    {
        //Debug.Log(count);
        count += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            //リズムのインターバルの間だけ射撃できる
            if (count <= interval / 2 && isShot1 == false && isShot2 == false)
            {
                Debug.Log("shot");

                scoreMn.Score();
                mm.Shot();

                isShot1 = true;
            }
            else if(count >= rythm - interval / 2 && isShot2 == false)
            {
                Debug.Log("shot");

                scoreMn.Score();
                mm.Shot();
                isShot2 = true;
            }
            else
            {
                scoreMn.Miss();
                Debug.Log("miss");
            }
        }

        if (Input.GetButtonDown("Fire2"))
        {
            //リズムのインターバルの間だけリロードできる
            if (count <= interval / 2 && isShot1 == false && isShot2 == false)
            {
                mm.Reload();

                isShot1 = true;
            }
            else if (count >= rythm - interval / 2 && isShot2 == false)
            {
                mm.Reload();

                isShot2 = true;
            }
            else
            {
                scoreMn.Miss();
                mm.ReloadMiss();

                Debug.Log("miss");
            }
        }

        //リズムごとにカウントをリセットする
        if (count >= rythm)
        {
            count = 0f;
        }
        else if(count >= interval / 2)
        {
            isShot1 = false;
            isShot2 = false;
        }
    }
}
