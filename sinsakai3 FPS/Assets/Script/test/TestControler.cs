using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestControler : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float jumpPower = 5f;
    [SerializeField] float dodgePower = 20f;
    [SerializeField] float isGroundLength = 1.1f; //接地判定をとる長さ
    [SerializeField] float isHitLength = 50f;
    [SerializeField] GameObject hitEffect;
    [SerializeField] LayerMask enemyLayer;
 
    float firstSpeed = 0;
    Vector3 dir;
    Rigidbody rb;
    Vector3 hitPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        firstSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = Vector3.forward * v + Vector3.right * h;

        Move();

        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if (Input.GetKeyDown("left shift"))
        {
            Dodge();
        }
    }

    /// <summary>
    /// プレイヤーの基本移動
    /// </summary>
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
            this.transform.rotation = rotation;

            Vector3 velo = dir.normalized * moveSpeed;　//移動
            velo.y = rb.velocity.y;
            rb.velocity = velo;
        }
    }

    /// <summary>
    /// 射撃が敵に当たった時の処理
    /// </summary>
    public void Shot()
    {
        if (isHit())
        {
            Debug.Log("HIT!!");
        }
    }

    /// <summary>
    /// ジャンプの処理
    /// </summary>
    public void Jump()
    {
        if (isGround())
        {
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
    }

    /// <summary>
    /// 回避の処理
    /// </summary>
    public void Dodge()
    {
        StartCoroutine(DodgeSpeed());
        Debug.Log("Dodge");
    }

    /// <summary>
    /// LineCastを使った接地判定
    /// </summary>
    /// <returns></returns>
    bool isGround()
    {
        CapsuleCollider capcol = GetComponent<CapsuleCollider>();
        Vector3 start = this.transform.position + capcol.center;
        Vector3 end = start + Vector3.down * isGroundLength;
        Debug.DrawLine(start, end); 
        bool isGrounded = Physics.Linecast(start, end);
        return isGrounded;
    }

    /// <summary>
    /// 射撃が敵に当たったか判定する処理
    /// </summary>
    /// <returns></returns>
    bool isHit()
    {

        if (enemyLayer == 0)
        {
            Debug.LogError("LayerにEnemyを設定してください");
        }
        Vector3 start = Camera.main.transform.position;
        Vector3 end = start + Camera.main.transform.forward * isHitLength;
        Debug.DrawLine(start, end, Color.red);
        bool isHit = Physics.Linecast(start, end , enemyLayer);
        if(isHit)
        {
            Debug.Log("HIT!");
        }
        return isHit;
    }

    /// <summary>
    /// 着弾点にエフェクトを出す処理
    /// </summary>
    void HitEffect()
    {
        RaycastHit hit;
        Vector3 start = Camera.main.transform.position;
        Vector3 end = Camera.main.transform.forward * isHitLength;
        if(Physics.Raycast(start, end, out hit, isHitLength))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = Vector3.zero;
        }

        if(hitPoint != Vector3.zero)
        {
            if(hitEffect)
            {
                Instantiate(hitEffect, hitPoint, Quaternion.identity);
            }            
        }
    }

    /// <summary>
    /// 回避で急加速する処理
    /// </summary>
    /// <returns></returns>
    IEnumerator DodgeSpeed()
    {
        moveSpeed = dodgePower;
        yield return new WaitForSeconds(0.1f);
        moveSpeed = firstSpeed;
    }
}
