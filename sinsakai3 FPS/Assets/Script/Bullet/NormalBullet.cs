using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ToDO] コメントを入れる
public class NormalBullet : BulletBase
{
    [SerializeField] float bulletSpeed = 5;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public override void Move()
    {
        rb.velocity = TargetPosition.normalized * bulletSpeed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
