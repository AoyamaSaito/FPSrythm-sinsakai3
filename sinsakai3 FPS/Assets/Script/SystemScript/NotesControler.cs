using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotesControler : MonoBehaviour
{   
    ShootingPlayer sp;

    RectTransform orizin;
    RectTransform target;

    RectTransform myRect;

    float startTime = 0;
    float finishTimeMag = 2;

    void Awake()
    {
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();

        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.transform.SetParent(canvas.transform);

        orizin = GameObject.FindGameObjectWithTag("orizin").GetComponent<RectTransform>();
        target = GameObject.FindGameObjectWithTag("target").GetComponent<RectTransform>();

        myRect = GetComponent<RectTransform>();
        myRect.localScale = orizin.localScale;
        startTime = Time.timeSinceLevelLoad;
        Destroy(this.gameObject, finishTimeMag);
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
        finishTimeMag = sp.rythm / finishTimeMag;
        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / finishTimeMag;

        myRect.position = Vector3.Lerp(orizin.position, target.position, rate);
    }
}
