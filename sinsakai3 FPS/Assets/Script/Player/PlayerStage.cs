using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerがステージ移動を行うためのスクリプト
/// </summary>
public class PlayerStage : MonoBehaviour
{
    [SerializeField] StageManager stageManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("StageChange"))
        {
            stageManager.NextStage();
        }

        if (other.gameObject.CompareTag("StageBack"))
        {
            stageManager.BackStage();
        }
    }
}
