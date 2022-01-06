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
    [SerializeField] Transform wallInstantiante;

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
            Debug.Log("Attack");
            StartCoroutine(AttackCor());
            StartCoroutine(NormalAttackCor());
            a = true;
        }
    }

    IEnumerator AttackCor()
    {
        while(isAttack)
        {
            yield return new WaitForSeconds(shootingPlayer.rythm * attackTime);
            int currentAttackPatern = Random.Range(0, attackPatern);

            switch(currentAttackPatern)
            {
                case 0:
                    Debug.Log("Late");
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootingPlayer.rythm * 2);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootingPlayer.rythm * 2);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(shootingPlayer.rythm * 2);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    break;
                case 1:
                    Debug.Log("Fast");
                    Instantiate(fastBullet, muzzle.position, Quaternion.identity);
                    break;
                case 2:
                    Debug.Log("Wall");
                    Instantiate(damageWall, wallInstantiante.position, Quaternion.identity);
                    break;
                case 3:
                    Debug.Log("Big");
                    Instantiate(bigBullet, muzzle.position, Quaternion.identity);
                    break;
            }
        } 
    }

    /// <summary>
    /// 弾を常時打ち出すコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator NormalAttackCor()
    {
        while (isAttack)
        {
            yield return new WaitForSeconds(shootingPlayer.rythm);
            Instantiate(normalBullet, muzzle.position, Quaternion.identity);
        }
    }
}
