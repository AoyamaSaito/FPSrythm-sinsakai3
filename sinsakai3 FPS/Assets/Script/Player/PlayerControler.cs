using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;


//[ToDo] Playerのステータスを構造体にする
//[ToDo] とにかく相互干渉をしない
/// <summary>
/// Playerの動作関係
/// </summary>
public class PlayerControler : MonoBehaviour
{
    [Header("基本動作")]
    [SerializeField, Tooltip("Playerの移動速度")] float moveSpeed = 5;
    [SerializeField, Tooltip("Playerのジャンプの高さ")] float jumpPower = 5f;
    [SerializeField, Tooltip("Playerの回避距離")] float dodgePower = 20f;
    [Header("ステータス")]
    [SerializeField, Tooltip("Playerのヘルス")] int hp = 100;
    [SerializeField, Tooltip("Playerのヘルス最大値のText")] Text fullHpText;
    [SerializeField, Tooltip("Playerの現在のヘルスのText")] Text currentHpText;
    [Header("ダメージ")]
    [SerializeField, Tooltip("Playerの射撃のダメージ")] int shotDamage = 2;
    [Header("レイヤー")]
    [SerializeField, Tooltip("接地判定をとる高さ")] float isGroundLength = 1.1f;
    [SerializeField, Tooltip("射撃のHit判定をとる長さ")] float isHitLength = 50f;
    [SerializeField, Tooltip("Enemyのレイヤー")] LayerMask enemyLayer;
    [Header("ヒットエフェクト")]
    [SerializeField, Tooltip("着弾エフェクト")] GameObject hitEffect;
    [SerializeField, Tooltip("エフェクトをDestroyする時間")] float effectDestroy = 0.5f;
    [Header("アニメーター")]
    [SerializeField, Tooltip("銃のアニメーター")] Animator gunAnim;
    [SerializeField, Tooltip("ダメージを受けた時のPanelのアニメーター")] Animator damagePanel;

    //初期値を保存
    float firstSpeed = 0;
    int firstHp = 0;

    GameObject[] enemy;
    float damageColor;
    Vector3 dir;
    Rigidbody rb;
    Vector3 hitPoint;
    UIMove uiAnim;
    GameObject ui;

    public Animator GunAnim { get => gunAnim; set => gunAnim = value; }
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //ダメージの時に赤くなるUIの処理
        ui = GameObject.Find("MoveUI");
        uiAnim = ui.GetComponent<UIMove>();
        damageColor = uiAnim.GetComponent<Image>().color.a;

        firstHp = hp;
        firstSpeed = moveSpeed;

        PlayerHP();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        dir = Vector3.forward * v + Vector3.right * h;

        Jump();

        dir = Camera.main.transform.TransformDirection(dir);
        dir.y = 0;
        if (dir != Vector3.zero) this.transform.forward = dir;

        Vector3 start = Camera.main.transform.position;
        Vector3 end = start + Camera.main.transform.forward * isHitLength;
        Debug.DrawLine(start, end, Color.red);
    }

    void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// プレイヤーの基本移動
    /// </summary>
    void Move()
    {
        if (dir == Vector3.zero)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            GunAnim.SetBool("Move", false);
        }
        else
        {
            Vector3 velo = dir.normalized * moveSpeed;　//移動
            velo.y = rb.velocity.y;
            rb.velocity = velo;

            Quaternion rotation = Quaternion.LookRotation(dir);
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, Time.deltaTime);

            GunAnim.SetBool("Move", true);
        }
    }

    /// <summary>
    /// 射撃が敵に当たった時の処理
    /// </summary>
    public void Shot()
    {
        uiAnim.AnimPlay();
        GunAnim.SetTrigger("Shot");

        if (enemyLayer == 0) //enemyLayerが設定されていないと
        {
            Debug.LogError("LayerにEnemyを設定してください");
        }

        RaycastHit isHit;
        Vector3 start = Camera.main.transform.position;
        Vector3 end = Camera.main.transform.forward * isHitLength;
        if (Physics.Raycast(start, end, out isHit, isHitLength, enemyLayer))
        {
            GameObject hitEnemy = isHit.collider.gameObject;
            hitEnemy.GetComponent<EnemyBase>().enemyDamage(shotDamage);
        }

        HitEffect();
    }

    /// <summary>
    /// ジャンプの処理
    /// </summary>
    public void Jump()
    {
        if (isGround() && Input.GetButtonDown("Jump"))
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
    }

    //public void Ultimate()
    //{
    //    if (Input.GetKeyDown("q"))
    //    {
    //        uiAnim.AnimPlay();
    //        enemy = GameObject.FindGameObjectsWithTag("Enemy");

    //        enemy.Where(go => go != null).ToList().ForEach(go => go.GetComponent<EnemyBase>().Damage(ultDamage));
    //    }
    //}

    void PlayerHP()
    {
        if (currentHpText && fullHpText)
        {
            currentHpText.text = hp.ToString();
            fullHpText.text = hp.ToString();
        }
    }

    public void PlayerDamage(int damage)
    {
        damagePanel.Play("DamageAnim");

        DOTween.To(() => hp, // 変化させる値
                x => hp = x, // 変化させた値 x の処理
                hp - damage, // x をどの値まで変化させるか
                0.05f)   // 何秒かけて変化させるか
                .OnUpdate(() => currentHpText.text = hp.ToString());
    }

    public void PlayerHeal(int heal)
    {
        DOTween.To(() => hp, // 変化させる値
                x => hp = x, // 変化させた値 x の処理
                Mathf.Min(hp + heal, firstHp), // x をどの値まで変化させるか
                0.05f)   // 何秒かけて変化させるか
                .OnUpdate(() => currentHpText.text = hp.ToString());
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
    /// 着弾点にエフェクトを出す処理
    /// </summary>
    void HitEffect()
    {
        RaycastHit hit;
        Vector3 start = Camera.main.transform.position;
        Vector3 end = Camera.main.transform.forward * isHitLength;
        if (Physics.Raycast(start, end, out hit, isHitLength, enemyLayer))
        {
            hitPoint = hit.point;
        }
        else
        {
            hitPoint = Vector3.zero;
        }

        if (hitPoint != Vector3.zero)
        {
            if (hitEffect)
            {
                Destroy(Instantiate(hitEffect, hitPoint, Quaternion.identity), effectDestroy);
            }
        }
    }


    /// <summary>
    /// 回避で急加速する処理
    /// </summary>
    /// <returns></returns>
    IEnumerator DodgeSpeed()
    {
        uiAnim.AnimPlay();
        moveSpeed = dodgePower;
        yield return new WaitForSeconds(0.1f);
        moveSpeed = firstSpeed;
    }
}
