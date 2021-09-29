using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] int magnification = 5;
    [SerializeField] int addScore = 100;

    [SerializeField] Text scoreText;
    [SerializeField] Text magText;

    Magnification state = Magnification.actual;

    int scoreCount = 0;
    int firstScore = 0;
    int currentScore = 0;
    void Start()
    {
        firstScore = addScore;　//最初の加点スコアを保存する
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
        scoreCount++;

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

        currentScore += addScore;
        if (scoreText)
        {
            scoreText.text = $"{currentScore}";
        }
    }

    public void Miss()
    {
        scoreCount = 0;
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
