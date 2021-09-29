using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesSystem : MonoBehaviour
{
    [SerializeField] public float speed = 2;

    [SerializeField] public GameObject target1;

    [SerializeField] GameObject notes;
    [SerializeField] ShootingPlayer sp;

    float count = 0;
  // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                Instantiate(notes, target1.transform.position, Quaternion.identity);
                count = 0;
            }
        }
        else
        {
            Debug.LogError("null");
        }
    }

}
