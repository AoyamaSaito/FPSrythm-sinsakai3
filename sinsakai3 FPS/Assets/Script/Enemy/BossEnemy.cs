using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    [Header("ボスの攻撃パターン")]
    [SerializeField, Tooltip("攻撃間隔")] int attackTime = 4;
    [SerializeField] GameObject normalBullet;
    [SerializeField] GameObject lateBullet;
    [SerializeField] GameObject fastBullet;
    [SerializeField] GameObject bigBullet;
    [SerializeField] GameObject damageWall;　//一定時間出現する壁
    [Header("各種座標")]
    [SerializeField] Transform muzzle;
    [SerializeField] Transform wallInstantiate;

    ShootingPlayer shootingPlayer;
    bool isAttack = true;
    int attackPatern = 3;
    bool a = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootingPlayer = player.GetComponent<ShootingPlayer>();
    }

    public override void Attack()
    {
        if (!a)
        {
            StartCoroutine(AttackCor());
            StartCoroutine(NormalAttackCor());
            a = true;
        }
    }

    /// <summary>
    /// Bossの攻撃分岐のコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator AttackCor()
    {
        while(isAttack)
        {
            yield return new WaitForSeconds(shootingPlayer.Rythm * attackTime);
            int currentAttackPatern = Random.Range(0, attackPatern);

            switch(currentAttackPatern)
            {
                case 0: //遅い球を4回生成する
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootingPlayer.Rythm * 2);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootingPlayer.Rythm * 2);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootingPlayer.Rythm * 2);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    break;
                case 1:　//速い球を生成する
                    Instantiate(fastBullet, muzzle.position, Quaternion.identity);
                    break;
                case 2:　//ダメージを受ける壁を生成する
                    Instantiate(damageWall, wallInstantiate.position, Quaternion.identity);
                    break;
                case 3:　//大きい球を生成する
                    Instantiate(bigBullet, muzzle.position, Quaternion.identity);
                    break;
            }
        } 
    }

    /// <summary>
    /// 普通の弾を常時打ち出すコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator NormalAttackCor()
    {
        while (isAttack)
        {
            yield return new WaitForSeconds(shootingPlayer.Rythm);
            Instantiate(normalBullet, muzzle.position, Quaternion.identity);
        }
    }
}
