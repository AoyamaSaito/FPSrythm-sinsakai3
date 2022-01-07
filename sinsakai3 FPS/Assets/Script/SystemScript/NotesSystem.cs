using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Notesを生成するスクリプト
/// </summary>
public class NotesSystem : MonoBehaviour
{

    [SerializeField, Tooltip("Noteを生成する座標")] public GameObject instantiatePosition;

    [SerializeField, Tooltip("生成するNotes")] GameObject notes;
    [SerializeField] ShootingPlayer sp;

    float count = 0;

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
            if (sp.rythm <= count)
            {
                Instantiate(notes, instantiatePosition.transform.position, Quaternion.identity);
                count = 0;
            }
        }
        else
        {
            Debug.LogError("null");
        }
    }

}
