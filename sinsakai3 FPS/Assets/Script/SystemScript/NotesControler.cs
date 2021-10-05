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
    float finishTime = 0;

    void Start()
    {
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();

        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.transform.SetParent(canvas.transform);

        orizin = GameObject.FindGameObjectWithTag("orizin").GetComponent<RectTransform>();
        target = GameObject.FindGameObjectWithTag("target").GetComponent<RectTransform>();

        myRect = GetComponent<RectTransform>();

        startTime = Time.timeSinceLevelLoad;

    }

    void Update()
    {
        NotesControl();
        StartCoroutine(Reset());
    }
 
    /// <summary>
    /// target1からtarget2まで動かす
    /// </summary>
    public void NotesControl()
    {
        finishTime = sp.rythm * 2;

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / finishTime;

        myRect.position = Vector3.Lerp(orizin.position, target.position, rate);
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(finishTime);
        Destroy(this.gameObject);
    }
}
