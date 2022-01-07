using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerがステージ移動を行うためのスクリプト
/// </summary>
public class PlayerStage : MonoBehaviour
{
    StageManager stagemana;

    void Start()
    {
        stagemana = GameObject.Find("StageManager").GetComponent<StageManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("StageChange"))
        {
            stagemana.NextStage();
        }

        if (other.gameObject.CompareTag("StageBack"))
        {
            stagemana.BackStage();
        }
    }
}
