using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float isGroundLength = 1.1f; //接地判定をとる長さ

    Vector3 dir;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = Vector3.forward * v + Vector3.right * h;
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (dir == Vector3.zero)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            dir = Camera.main.transform.TransformDirection(dir); //カメラを基準に座標をとる
            dir.y = 0;

            Quaternion rotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime);

            Vector3 velo = dir.normalized * moveSpeed;　//移動
            velo.y = rb.velocity.y;
            rb.velocity = velo;
        }
    }
    public void Jump()
    {
        if (isGround())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }
    bool isGround()
    {
        CapsuleCollider capcol = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + capcol.center;
        Vector3 end = start + Vector3.down * isGroundLength;
        Debug.DrawLine(start, end); 
        bool isGrounded = Physics.Linecast(start, end);
        return isGrounded;
    }
}
