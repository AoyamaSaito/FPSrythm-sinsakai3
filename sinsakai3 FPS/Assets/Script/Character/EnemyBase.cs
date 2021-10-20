using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] float hp = 10;

    GameObject[] enemy  = default;
    EnemyBase eb;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
        eb = enemy.ToList().Min().GetComponent<EnemyBase>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(float damage)
    {
        hp -= damage;
    }
}
