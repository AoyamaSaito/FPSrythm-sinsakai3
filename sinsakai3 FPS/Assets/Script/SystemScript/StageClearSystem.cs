using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージクリアの判定を行うスクリプト
/// </summary>
public class StageClearSystem : MonoBehaviour
{
    [SerializeField, Tooltip("そのステージ内の敵")] GameObject[] enemys;
    [SerializeField, Tooltip("そのステージ内のドア")] GameObject doors;
    [SerializeField] string textTag = "StageClear";

    int enemyLength = 0;
    Text clearText;

    void Start()
    {
        enemyLength = enemys.Length;
        clearText = GameObject.Find(textTag).GetComponent<Text>();
        clearText.enabled = false;
    }

    /// <summary>
    /// もしステージ内の敵が0になったらStageClear()を呼ぶ
    /// </summary>
    public void IsStageClear()
    {
        enemyLength--;
        if(enemyLength == 0)
        {
            StageClear();
        }
    }

    /// <summary>
    /// Stageをクリアしたときの処理
    /// </summary>
    void StageClear()
    {
        StartCoroutine(TextCor());

        Destroy(doors);
    }
    IEnumerator TextCor()
    {
        clearText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        clearText.enabled = false;
        Destroy(gameObject);
    }
}
