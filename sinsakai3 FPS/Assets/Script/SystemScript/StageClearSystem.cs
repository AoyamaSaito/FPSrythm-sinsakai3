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
    [SerializeField, Tooltip("そのステージ内のアイテム")] RamdomInstantiate[] items;
    [SerializeField, Tooltip("そのステージ内のドア")] GameObject doors;
    [SerializeField] string textTag = "StageClear";

    int enemyLength = 0;
    List<GameObject> enemys = new List<GameObject>();
    Text clearText;

    public List<GameObject> Enemys { get => enemys; set => enemys = value; }

    void Start()
    {
        enemyLength = Enemys.Count;
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
        Array.ForEach(items, go => go.Pop());

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
