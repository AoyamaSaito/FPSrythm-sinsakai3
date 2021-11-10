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

    }

    void Update()
    {
        NotesControl();
        StartCoroutine(Reset());
    }
 
    /// <summary>
    /// Notesの動作の処理
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
