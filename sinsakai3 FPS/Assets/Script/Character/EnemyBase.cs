using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] int hp = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// 引数にダメージを設定する
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        hp -= damage;

        if(hp <= 0) //HPがゼロになったらDestroyする
        {
            Destroy(gameObject);
        }
    }
}
