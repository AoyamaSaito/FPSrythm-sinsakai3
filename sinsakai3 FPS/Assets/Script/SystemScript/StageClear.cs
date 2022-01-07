using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステージクリアの判定を行うスクリプト
/// </summary>
public class StageClear : MonoBehaviour
{
    [SerializeField, Tooltip("そのステージ内の敵")] GameObject[] enemys;
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
    /// もしステージ内の敵が0になったらTextを表示する
    /// </summary>
    public void IsStageClear()
    {
        enemyLength--;
        if(enemyLength == 0)
        {
            StartCoroutine(TextCor());
        }
    }

    IEnumerator TextCor()
    {
        clearText.enabled = true;
        yield return new WaitForSeconds(0.5f);
        clearText.enabled = false;
        Destroy(gameObject);
    }
}
