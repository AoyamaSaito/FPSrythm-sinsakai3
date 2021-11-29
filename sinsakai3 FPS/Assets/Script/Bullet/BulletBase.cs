using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] int damage = 10;
    [SerializeField] GameObject destroyEffect;

    Vector3 playerPosition;
    [System.NonSerialized] public Vector3 targetPosition;

    void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>().position;

        targetPosition = playerPosition - this.transform.position; //Playerの座標をtargetとする
    }

    public abstract void Move();


    void OnTriggerEnter(Collider other)
    {
        Hit(other);
    }

    public virtual void Hit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.GetComponent<PlayerControler>().PlayerDamage(damage);
            Destroy(gameObject);

            if (destroyEffect)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }
        }
        Destroy(gameObject, 5f);
    }
}
