using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リズムごとにしか行動ができないようにするスクリプト
/// </summary>
public class ShootingPlayer : MonoBehaviour
{
    [SerializeField, Tooltip("ゲーム全体のリズムはこの変数に基づく")] 
    double _rythm = 0.4f;    //リズム
    public double Rythm
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
    double beatInterval;

    void Awake()
    {
        _rythm = 60d / _rythm;

        beforeElapsedTime = AudioSettings.dspTime;

        beatInterval = _rythm;

        Cursor.visible = false; //マウスカーソルを非表示に

        scoreMn = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        mm = GameObject.Find("GunManager").GetComponent<GunManager>();
        pc = GetComponent<PlayerControler>();
    }
        
    void Update()
    {
        Fire();
    }

    void FixedUpdate()
    {
        //RythmUpdate();
        count += Time.deltaTime;
    }

    /// <summary>
    /// リズムの各動作の処理
    /// </summary>
    void Fire()
    {
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
            else if (count >= Rythm - interval / 2 && isRythm2 == false)
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
            else if (count >= Rythm - interval / 2 && isRythm2 == false)
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
            else if (count >= Rythm - interval / 2 && isRythm2 == false)
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
        if (count >= Rythm)
        {
            Debug.Log(count);
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

    [Tooltip("前回の拍の時間")]double beforeElapsedTime;
    bool first = true;

    /// <summary>
    /// 絶対に発生するズレを可能な限り抑えるために、RythmをdspTimeを用いて更新する
    /// </summary>
    void RythmUpdate()
    {
        //現在の経過時間を出す
        double currentElapsedTime = AudioSettings.dspTime;
        //現在の経過時間から前回の経過時間を引き、前回からの経過時間を割り出す
        double elapsedDspTime = currentElapsedTime - beforeElapsedTime;
        //これによって、現在リズムがどれだけずれているかを割り出す

        //最初は次の拍は決まっているので更新しない
        if (first)
        {
            first = false;
        }
        else if (!first)
        {
            //現在の経過時間から、ずれている分だけ拍を縮める
            _rythm = beatInterval - elapsedDspTime;
        }
        Debug.Log(_rythm);
        //今回の経過時間を保存する
        beforeElapsedTime = currentElapsedTime;
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
