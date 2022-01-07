using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ToDo]

/// <summary>
/// Bulletの基底クラス
/// </summary>
public abstract class BulletBase : MonoBehaviour
{
    [Header("弾の各種ステータス")]
    [SerializeField, Tooltip("与えるダメージ")] int damage = 10;
    [Header("エフェクト")]
    [SerializeField, Tooltip("破壊されるときのエフェクト")] GameObject destroyEffect;

    Vector3 playerPosition;
    [System.NonSerialized, Tooltip("Playerの座標を代入する変数")] Vector3 targetPosition;

    public Vector3 TargetPosition { get => targetPosition; set => targetPosition = value; }

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        TargetPosition = playerPosition - this.transform.position; //Playerの座標をtargetとする
    }

    public abstract void Move();

    /// <summary>
    /// 引数だけ所得してHit関数で処理を行う
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    /// <summary>
    /// 着弾したときの処理
    /// </summary>
    /// <param name="col"></param>
    public virtual void Hit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerControler>().PlayerDamage(damage); //PlayerのPlayerDamage関数でダメージを与える
            Destroy(gameObject);

            if (destroyEffect)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);　//エフェクトを生成
            }
        }
        Destroy(gameObject, 5f);
    }
}
