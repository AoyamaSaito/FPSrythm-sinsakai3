using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ToDO] コメントを入れる
public class NormalBullet : BulletBase
{
    [SerializeField] float bulletSpeed = 5;

    Vector3 target = new Vector3();
    Rigidbody rb;
    void Start()
    {
        target = Singleton.playerInstance.transform.position - this.transform.position; //Playerの座標をtargetとする
        rb = GetComponent<Rigidbody>();
        Move();
    }

    public override void Move()
    {
        rb.velocity = target.normalized * bulletSpeed;
    }
    // Update is called once per frame
}
