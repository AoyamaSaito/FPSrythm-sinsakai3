using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClear : MonoBehaviour
{
    [SerializeField] GameObject[] enemys;
    [SerializeField] string textTag = "StageClear";

    int enemyLength = 0;
    Text clearText;

    void Start()
    {
        enemyLength = enemys.Length;
        clearText = GameObject.Find(textTag).GetComponent<Text>();
        clearText.enabled = false;
    }

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
