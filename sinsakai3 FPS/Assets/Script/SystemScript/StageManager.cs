using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField, Tooltip("最初に呼び出されるステージ")] GameObject firstStage;
    [SerializeField, Tooltip("SetActiveをfalseにしてください")] GameObject[] stages;

    List<GameObject> inGameStages = new List<GameObject>();
    GameObject[] shuffleStage;

    void Start()
    {
        shuffleStage = stages.OrderBy(i => Guid.NewGuid()).ToArray();
        inGameStages.Add(Instantiate(firstStage, Vector3.zero, Quaternion.identity));
        for(int i = 0; i < shuffleStage.Length; i++)
        {
            inGameStages.Add(Instantiate(shuffleStage[i], new Vector3(0, 0, 50 + 50 * i), Quaternion.identity));
        }
    }

    void NextStage()
    {

    }
}
