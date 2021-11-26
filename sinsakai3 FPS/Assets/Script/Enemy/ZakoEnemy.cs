using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZakoEnemy : EnemyBase
{
    [Header("弾")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;

    ShootingPlayer sp;
    float _timer = 0;

    void Awake()
    {
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();
    }

    public override void Attack()
    {      
        _timer += Time.deltaTime;

        if(sp.rythm * 4 <= _timer)
        {
            Instantiate(bullet, muzzle.position, Quaternion.identity);
            _timer = 0;
        }
    }
}
