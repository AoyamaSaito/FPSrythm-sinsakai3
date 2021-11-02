using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZakoEnemy : EnemyBase
{
    [Header("弾")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] float minShotTime = 2;
    [SerializeField] float maxShotTime = 3;

    ShootingPlayer sp;
    float _timer = 0;
    float shotTime = 0;

    void Awake()
    {
        sp = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();
    }
    public override void Attack()
    {      
        _timer += Time.deltaTime;

        if(sp.rythm * 2 >= _timer)
        {
             shotTime = Random.Range(minShotTime, sp.rythm * 2);
        }

        if(shotTime <= _timer)
        {
            Instantiate(bullet, muzzle.position, Quaternion.identity);
            _timer = 0;
        }
    }
}
