using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Notesを生成するスクリプト
/// </summary>
public class NotesSystem : MonoBehaviour
{

    [SerializeField, Tooltip("Noteを生成する座標")] public GameObject instantiatePosition;

    [SerializeField, Tooltip("生成するNotes 大きいほう")] GameObject notes;
    [SerializeField, Tooltip("生成するNotes 小さいほう")] GameObject miniNotes;
    [SerializeField] ShootingPlayer sp;

    float count = 0;
    bool mini = false;

    void Start()
    {
        mini = false;
    }

    void Update()
    {
        count += Time.deltaTime;
        NotesGenerator();
    }
    /// <summary>
    /// リズムごとにnotesを生成する
    /// </summary>
    void NotesGenerator()
    {
        if (sp)
        {
            if (sp.rythm <= count && !mini)
            {
                Instantiate(notes, instantiatePosition.transform.position, Quaternion.identity, this.transform);
                count = 0;
                mini = true;
            }
            else if(sp.rythm <= count && mini)
            {
                var notes = Instantiate(miniNotes, instantiatePosition.transform.position, Quaternion.identity, this.transform);
                count = 0;
                mini = false;
            }
        }
    }

}
