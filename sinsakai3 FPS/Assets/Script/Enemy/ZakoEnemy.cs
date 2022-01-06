using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ZakoEnemy : EnemyBase
{
    [Header("弾")]
    [SerializeField, Tooltip("弾")] GameObject bullet;
    [SerializeField, Tooltip("発射間隔")] int shotInterval = 4;
    [SerializeField, Tooltip("弾の発射位置")] Transform muzzle;

    ShootingPlayer shootingPlayer;
    float _timer = 0;

    void Awake()
    {
        shootingPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingPlayer>();
    }

    public override void Attack()
    {
        transform.LookAt(player.transform.position); //Playerの方向を向く
        _timer += Time.deltaTime;

        if(shootingPlayer.rythm * shotInterval <= _timer) //rythm　×　shotIntervalの間隔で弾を生成
        {
            Instantiate(bullet, muzzle.position, Quaternion.identity);
            _timer = 0;
        }
    }
}
