using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rb.velocity = targetPosition.normalized * bulletSpeed;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
}
