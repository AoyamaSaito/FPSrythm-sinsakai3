using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestCount : MonoBehaviour
{


    //譜面情報
    private List<string[]> SONG_SCORE = new List<string[]>();
    private TextAsset SONG_FILE;
    private string SONG_TITLE;
    private string SONG_ARTIST;
    private float SONG_BPM = 115;

    private double _metronomeStartDspTime;
    private double _buffer = 2 / 60d;
    private int Count = 0;

    //音楽ファイル
    [SerializeField] AudioSource[] BGM;

    //譜面情報TEXTオブジェクト
    private Text[] TITLE_A;

    private bool test = false;

    // Start is called before the first frame update
    void Start()
    {
        //確認用
        _metronomeStartDspTime = AudioSettings.dspTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        double nxtRng = NextRingTime();

        if (nxtRng < AudioSettings.dspTime + _buffer)
        {
            if (test == false)
            { 
                BGM[0].Play(); test = true; 
            }

            BGM[1].PlayScheduled(nxtRng);
        }

    }

    //CSVファイルから譜面情報を取得
    void GetGameScore()
    {
        SONG_FILE = Resources.Load("Score/TestMusic") as TextAsset;
        StringReader reader = new StringReader(SONG_FILE.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            SONG_SCORE.Add(line.Split(','));
        }

        //TITLE、ARTISを抽出
        SONG_TITLE = SONG_SCORE[0][0].Replace("TITLE:", "");
        SONG_ARTIST = SONG_SCORE[1][0].Replace("ARTIST:", "");
        SONG_BPM = float.Parse(SONG_SCORE[2][0].Replace("BPM:", ""));
    }

    double NextRingTime()
    {
        double beatInterval = 60d / SONG_BPM;
        double elapsedDspTime = AudioSettings.dspTime - _metronomeStartDspTime;
        double beats = System.Math.Floor(elapsedDspTime / beatInterval);

        return _metronomeStartDspTime + (beats + 1d) * beatInterval;
    }

}
