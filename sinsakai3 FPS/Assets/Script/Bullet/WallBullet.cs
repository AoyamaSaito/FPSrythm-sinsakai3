using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ToDO] コメントを入れる

public class WallBullet : BulletBase
{
    [SerializeField] float bulletSpeed = 7f;

    bool a = false;
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        if(!a)
        {
            transform.LookAt(base.TargetPosition);
            a = true;
        }

        rb.velocity = base.TargetPosition.normalized * bulletSpeed;
    }
}
