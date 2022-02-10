using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Scoreのマネージャー
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [SerializeField, Tooltip("何回撃ったら倍率が上がるか")] int magnification = 5;
    [SerializeField, Tooltip("追加されるスコア")] int addScore = 100;
    [SerializeField, Tooltip("スコアがTextで変化しきるまでの秒数")] float scoreChangeInterval = 0.2f;

    [SerializeField] Text scoreText;
    [SerializeField] Text magText;

    Magnification state = Magnification.actual;

    int scoreCount = 0;
    int firstScore = 0;
    int currentScore = 0;

    GunManager mm;
    void Start()
    {
        firstScore = addScore;　//最初の加点スコアを保存する
        mm = GameObject.Find("GunManager").GetComponent<GunManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (magText)
        {
            //stateごとの処理
            switch (state)
            {
                case Magnification.actual: //等倍の時の処理
                    addScore = firstScore;
                    magText.text = "✕" + 1;
                    break;

                case Magnification.twice:   //二倍の時の処理
                    addScore = firstScore;
                    addScore *= 2;
                    magText.text = "✕" + 2;
                    break;

                case Magnification.thrice:  //三倍の時の処理
                    addScore = firstScore;
                    addScore *= 3;
                    magText.text = "✕" + 3;
                    break;

                case Magnification.fourTimes:   //四倍の時の処理
                    addScore = firstScore;
                    addScore *= 4;
                    magText.text = "✕" + 4;
                    break;
            }
        }
        else
        {
            Debug.LogError("null");
        }
    }
    /// <summary>
    /// コンボに応じて倍率を切り替える処理
    /// </summary>
    public void Score()
    {
        if (mm.currentBulletCount != 0)
        {
            scoreCount++;
            int beforeScore = currentScore;

            if (scoreCount >= magnification * 3)
            {
                state = Magnification.fourTimes;
                Debug.Log("fourTimes");
            }
            else if (scoreCount >= magnification * 2)
            {
                state = Magnification.thrice;
                Debug.Log("thrice");
            }
            else if (scoreCount >= magnification)
            {
                state = Magnification.twice;
                Debug.Log("twice");
            }
            else if (scoreCount >= 0)
            {
                state = Magnification.actual;
                Debug.Log("actual");
            }

            DOTween.To(() => currentScore, // 変化させる値
                x => currentScore = x, // 変化させた値 x の処理
                currentScore + addScore, // x をどの値まで変化させるか
                scoreChangeInterval)   // 何秒かけて変化させるか
                .OnUpdate(() => scoreText.text = currentScore.ToString("D10"))   // 数値が変化する度に実行する処理を書く
                .OnComplete(() => scoreText.text = currentScore.ToString("D10"));
        }
    }

    /// <summary>
    /// ミスをしたとき倍率をリセットする
    /// </summary>
    public void Miss()
    {
        scoreCount = 0;
        state = Magnification.actual;
        Debug.Log("Reset");
    }

    
    /// <summary>
    /// Scoreの倍率を示すenum
    /// </summary>
    enum Magnification
    {
        //等倍
        actual,
        //二倍
        twice,
        //三倍
        thrice,
        //四倍
        fourTimes,
    }
}
