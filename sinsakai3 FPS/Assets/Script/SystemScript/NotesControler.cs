using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesControler : MonoBehaviour
{   
    NotesSystem notesS;
    ShootingPlayer sp;

    RectTransform orizin;
    RectTransform target;

    void Start()
    {
        notesS = GameObject.Find("NotesManager").GetComponent<NotesSystem>();
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();
        GameObject canvas = GameObject.Find("Canvas");
        gameObject.transform.transform.SetParent(canvas.transform);
        target = GameObject.FindGameObjectWithTag("target").GetComponent<RectTransform>();
    }

    void Update()
    {
        NotesControl();
    }
    // Update is called once per frame
    /// <summary>
    /// target1からtarget2まで動かす
    /// </summary>
    public void NotesControl()
    {
        transform.position = Vector3.MoveTowards(gameObject.transform.position, target.position, notesS.speed * Time.deltaTime);
        Destroy(this.gameObject, sp.rythm);
    }
}
