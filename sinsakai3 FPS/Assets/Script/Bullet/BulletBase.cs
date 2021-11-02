using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] GameObject destroyEffect;

    Vector3 playerPosition;
    [System.NonSerialized] public Vector3 targetPosition;

    void Awake()
    {
        playerPosition = GameObject.FindWithTag("Player").GetComponent<Transform>().position;

        targetPosition = playerPosition - this.transform.position; //Playerの座標をtargetとする
    }

    public abstract void Attack();

    void OnTriggerEnter(Collider other)
    {      
        if (other.gameObject.tag == "Player")
        {
            if (destroyEffect)
            {
                Instantiate(destroyEffect, this.transform.position, Quaternion.identity);
            }           
        }
        Destroy(this.gameObject);
    }
}
