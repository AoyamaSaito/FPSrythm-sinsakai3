using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] StageClearSystem stageClear = null;
    [SerializeField] bool isTrue = true;
    int isInstantiate = 0;
    void Awake()
    {
        isInstantiate = Random.Range(0, 3);

        if(isTrue == true)
        {
            gameObject.SetActive(true);
            stageClear.Enemys.Add(gameObject);
        }
        else if(isInstantiate == 0)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            stageClear.Enemys.Add(gameObject);
        }
    }
}
