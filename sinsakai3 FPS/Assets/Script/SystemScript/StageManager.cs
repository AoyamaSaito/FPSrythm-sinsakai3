using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] GameObject firstStage;
    [SerializeField] GameObject[] stages;

    List<GameObject> inGameStages = new List<GameObject>();
    GameObject[] shuffleStage;

    void Start()
    {
        shuffleStage = stages.OrderBy(i => Guid.NewGuid()).ToArray();
        inGameStages.Add(Instantiate(firstStage, Vector3.zero, Quaternion.identity));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
