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
    Magnification state = Magnification.actual;
    int scoreCount = 0;
    int firstScore = 0;
    int currentScore = 0;
    void Start()
    {
        firstScore = addScore;
    }

    // Update is called once per frame
    void Update()
    {
        //stateごとの処理
        switch(state)
        {
            case Magnification.actual:
                addScore = firstScore;
                break;

            case Magnification.twice:
                addScore = firstScore;
                addScore *= 2;
                break;

            case Magnification.thrice:
                addScore = firstScore;
                addScore *= 3;
                break;

            case Magnification.fourTimes:
                addScore = firstScore;
                addScore *= 4;
                break;
        }
    }
    /// <summary>
    /// コンボに応じて倍率を切り替える処理
    /// </summary>
    public void Score()
    {
        scoreCount++;
        if(scoreCount >= 0)
        {
            state = Magnification.actual;
            Debug.Log("actual");
        }
        if (scoreCount >= magnification)
        {
            state = Magnification.twice;
            Debug.Log("twice");
        }
        if (scoreCount >= magnification * 2)
        {
            state = Magnification.thrice;
            Debug.Log("thrice");
        }
        if (scoreCount >= magnification * 3)
        {
            state = Magnification.fourTimes;
            Debug.Log("fourTimes");
        }

        currentScore += addScore;

        scoreText.text = $"{currentScore}";
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
