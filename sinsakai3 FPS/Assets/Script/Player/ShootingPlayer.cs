﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リズムごとにしか行動ができないようにするスクリプト
/// </summary>
public class ShootingPlayer : MonoBehaviour
{
    [SerializeField] float _rythm = 0.4f;    //リズム
    public float rythm
    {
        get { return _rythm; }
    }

    [SerializeField, Tooltip("譜面の猶予時間")] float interval = 0.3f;
    [Header("MissText")]
    [SerializeField, Tooltip("Missしたときに画面に出るText")] GameObject missText;
    [SerializeField, Tooltip("MissTextを表示しておく時間")] float waitTime = 0.2f;

    ScoreManager scoreMn;
    GunManager mm;
    PlayerControler pc;
    float count;
    bool isRythm1 = false;
    bool isRythm2 = false;
    bool isShot = true;
    bool isReload = true;
    bool isDodge = true;

    void Awake()
    {
        Cursor.visible = false; //マウスカーソルを非表示に

        scoreMn = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        mm = GameObject.Find("GunManager").GetComponent<GunManager>();
        pc = GetComponent<PlayerControler>();
    }

    void Update()
    {
        Fire();
    }

    /// <summary>
    /// リズムの各動作の処理
    /// </summary>
    void Fire()
    {
        count += Time.deltaTime;

        if (Input.GetButtonDown("Fire1") && mm.currentBulletCount != 0 && mm.currentReloadCount == 0 && isShot == true)　//リズムのインターバルの間だけ射撃できる
        {
            isReload = false;
            isDodge = false;

            if (count <= interval / 2 && isRythm1 == false && isRythm2 == false)
            {
                pc.Shot();
                scoreMn.Score();
                mm.Shot();

                isRythm1 = true;
            }
            else if (count >= rythm - interval / 2 && isRythm2 == false)
            {
                pc.Shot();
                scoreMn.Score();
                mm.Shot();

                isRythm2 = true;
            }
            else
            {
                scoreMn.Miss();
                MissText();
            }
        }
        
        if (Input.GetButtonDown("Fire2") && isReload == true)　//リズムのインターバルの間だけリロードできる
        {
            isShot = false;
            isDodge = false;

            if (count <= interval / 2 && isRythm1 == false && isRythm2 == false)
            {
                mm.Reload();

                isRythm1 = true;
            }
            else if (count >= rythm - interval / 2 && isRythm2 == false)
            {
                mm.Reload();

                isRythm2 = true;
            }
            else
            {
                scoreMn.Miss();
                MissText();
            }
        }

        if (Input.GetKeyDown("left shift") && isDodge == true)　//リズムのインターバルの間だけ回避できる
        {
            isShot = false;
            isReload = false;

            if (count <= interval / 2 && isRythm1 == false && isRythm2 == false)
            {
                pc.Dodge();

                isRythm1 = true;
            }
            else if (count >= rythm - interval / 2 && isRythm2 == false)
            {
                pc.Dodge();

                isRythm2 = true;
            }
            else
            {
                MissText();
            }
        }

        //リズムごとにカウントをリセットする
        if (count >= rythm)
        {
            count = 0f;
        }
        else if (count >= interval / 2)
        {
            isRythm1 = false;
            isRythm2 = false;

            isShot = true;
            isReload = true;
            isDodge = true;
        }       
    }

    void MissText()
    {
        StartCoroutine(MissTextCor());
    }

    /// <summary>
    /// リズム外で動作をしたときにMissTextを表示するコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator MissTextCor()
    {
        missText.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        missText.SetActive(false);
    }
}
