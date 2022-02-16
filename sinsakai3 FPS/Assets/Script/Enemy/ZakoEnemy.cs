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

    Vector3 playerPosition;
    ShootingPlayer shootingPlayer;
    float _timer = 0;

    protected override void Start()
    {
        base.Start();
    }

    public override void Attack()
    {
        playerPosition = Singleton.playerInstance.transform.position;
        transform.LookAt(playerPosition); //Playerの方向を向く
        _timer += Time.deltaTime;

        if(Singleton.playerInstance.GetComponent<ShootingPlayer>().Rythm * shotInterval <= _timer) //rythm　×　shotIntervalの間隔で弾を生成
        {
            Instantiate(bullet, muzzle.position, Quaternion.identity);
            _timer = 0;
        }
    }
}
