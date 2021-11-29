using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : EnemyBase
{
    [Header("ボスの攻撃パターン")]
    [SerializeField] int attackTime = 4;
    [SerializeField] GameObject lateBullet;
    [SerializeField] GameObject fastBullet;
    [SerializeField] GameObject bigBullet;
    [SerializeField] GameObject damageWall;//一定時間出現する壁
    [SerializeField] Transform muzzle;
    [SerializeField] Transform wallInstantiante;

    GameObject player;
    ShootingPlayer sp;
    bool isAttack = true;
    int attackPatern = 3;
    bool a = false;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sp = player.GetComponent<ShootingPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        if (!a)
        {
            StartCoroutine(AttackCor());
            a = true;
        }
    }

    IEnumerator AttackCor()
    {
        while(isAttack)
        {
            yield return new WaitForSeconds(sp.rythm * attackTime);
            int currentAttackPatern = Random.Range(0, attackPatern);

            switch(currentAttackPatern)
            {
                case 0:
                    Debug.Log("Late");
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(sp.rythm);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(sp.rythm);
                    Instantiate(lateBullet, muzzle.position, Quaternion.identity);
                    yield return new WaitForSeconds(sp.rythm);
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

}
