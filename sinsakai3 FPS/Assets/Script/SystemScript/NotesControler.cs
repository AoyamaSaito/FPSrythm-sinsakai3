using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Notesの動作の処理
/// </summary>
public class NotesControler : MonoBehaviour
{   
    ShootingPlayer shootingPlayer;

    RectTransform orizin;
    RectTransform target;

    RectTransform myRectTr;

    float startTime = 0;
    float finishTimeMag = 4;

    void Start()
    {
        shootingPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();

        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.transform.SetParent(canvas.transform);

        orizin = GameObject.FindGameObjectWithTag("orizin").GetComponent<RectTransform>();
        target = GameObject.FindGameObjectWithTag("target").GetComponent<RectTransform>();

        myRectTr = GetComponent<RectTransform>();
        myRectTr.localScale = orizin.localScale;
        startTime = Time.timeSinceLevelLoad;
        Destroy(this.gameObject, finishTimeMag);
        finishTimeMag = shootingPlayer.rythm * finishTimeMag;
    }

    void Update()
    {
        NotesControl();
    }
 
    /// <summary>
    /// Notesの動作の処理
    /// </summary>
    public void NotesControl()
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / finishTimeMag;

        myRectTr.position = Vector3.Lerp(orizin.position, target.position, rate); //targetに段々と向かう
    }
}
