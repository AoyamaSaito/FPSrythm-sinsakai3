using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

//[ToDo] Wanderが未完成

/// <summary>
/// Enemyの基底クラス
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField, Tooltip("EnemyのHP")] int hp = 10;
    [SerializeField, Tooltip("Chaseの追尾速度")] float chaseSpeed = 4;
    [SerializeField, Tooltip("NavMeshの追尾速度")] float navSpeed = 6;
    [Header("ダメージエフェクト")]
    [SerializeField, Tooltip("ダメージを受けた時に変化させるRenderer")] Renderer changeRenderer;
    [SerializeField, Tooltip("ダメージを受けた時に変化させる色")] Color damageColor = default;
    [Header("高さ")]
    [SerializeField, Tooltip("リスポーンするときの最低の高さ")] float minHeight = 3;
    [SerializeField, Tooltip("リスポーンするときの最大の高さ")] float maxHeight = 5;
    [SerializeField, Tooltip("NavMeshの高さ")] float wanderHeight = 4f;
    [Header("距離")]
    [SerializeField, Tooltip("プレイヤーに近づける距離")] float chaseDistance = 1.7f;
    [SerializeField, Tooltip("NavMeshが動ける幅")] float wanderWidth = 14f;
    [SerializeField, Tooltip("目標地点の生成オブジェクト")] GameObject navTarget = default;
    [Header("プレイヤー追跡パターン")]
    [SerializeField, Tooltip("プレイヤー追跡パターンの列挙型")] MovePatern moveState = default;
    [SerializeField, Tooltip("falseで動作を止める")] bool isMove = true;
    [Header("StageClear")]
    [SerializeField] StageClearSystem stageClear;
    
    RaycastHit hit;
    Rigidbody rb;
    Collider col;
    NavMeshAgent navAgent;
    protected GameObject player;
    Color defaultColor;
    float kyori = 0;
    float chaseHeight = 0;
    float targetX = 0;
    float targetZ = 0;
    Vector3 beforeTarget = default;

    void Start()
    {
        defaultColor = changeRenderer.material.color;
        player = GameObject.FindGameObjectWithTag("Player");

        switch (moveState)
        {
            case MovePatern.chase:　//chaseはRigidBody依存で動作を行うのでそれ以外のコンポーネントをオフにする
                rb = GetComponent<Rigidbody>();
                navAgent = GetComponent<NavMeshAgent>();
                navAgent.enabled = false;
                chaseHeight = Random.Range(minHeight, maxHeight);
                transform.position = new Vector3(gameObject.transform.position.x, chaseHeight, gameObject.transform.position.z);
                break;
            case MovePatern.wander:　//wanderはNavMesh依存で動作を行うのでそれ以外のコンポーネントをオフにする
                rb = GetComponent<Rigidbody>();
                navAgent = GetComponent<NavMeshAgent>();
                rb.isKinematic = false;
                navAgent.enabled = true;
                int nowStageIndex = GameObject.FindGameObjectWithTag("StageManager").GetComponent<StageManager>().nowStage * 50;
                wanderWidth += nowStageIndex;
                targetX = Random.Range(-wanderWidth, wanderWidth);
                targetZ = Random.Range(-wanderWidth, wanderWidth);
                beforeTarget = new Vector3(targetX, wanderHeight, targetZ);
                navAgent.destination = new Vector3(targetX, wanderHeight, targetZ);
                Instantiate(navTarget, beforeTarget, Quaternion.identity);
                break;
        }            
    }

    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        switch (moveState)
        {
            case MovePatern.wander:
                if(isMove)
                {
                    //PerlineNoiseで追尾速度を不規則に
                    navAgent.speed = Mathf.PerlinNoise(gameObject.transform.position.x, gameObject.transform.position.z) * navSpeed;
                }
                break;
        }

        Attack();
    }

    void FixedUpdate()
    {
        switch (moveState)
        {
            case MovePatern.chase:
                if(isMove)
                {
                    Chase();
                }
                break;
        }
    }

    public abstract void Attack();

    /// <summary>
    /// 引数にダメージを設定する
    /// </summary>
    /// <param name="damage"></param>
    public void enemyDamage(int damage)
    {
        hp -= damage;
        StartCoroutine(DamageColor());

        if (hp <= 0) //HPがゼロになったら死ぬ処理をする
        {
            Destroy(gameObject);
            stageClear.IsStageClear();
        }
    }

    /// <summary>
    /// プレイヤーとの距離がchaseDistanceより遠ければ追跡する
    /// </summary>
    void Chase()
    {
        //Playerと自分の現在の座標をchaseHeightに応じて出す
        Vector3 playerPosition = new Vector3(player.transform.position.x, chaseHeight, player.transform.position.z);
        Vector3 myPosition = new Vector3(gameObject.transform.position.x, chaseHeight, gameObject.transform.position.z);

        //Playerと自分の座標との距離を求める
        if (Physics.Linecast(gameObject.transform.position, player.transform.position, out hit))
        {
            kyori = hit.distance;
        }

        //Playerと自分の座標との距離がプレイヤーに近づける距離(chaseDistance)より長かったらプレイヤーの方向に向かう
        if (kyori >= chaseDistance)
        {
            Vector3 dir = playerPosition - myPosition;
            rb.velocity = dir.normalized * Mathf.PerlinNoise(this.transform.position.x, this.transform.position.z) * chaseSpeed;
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Wander(other);
    }

    /// <summary>
    /// NavMeshAgentを使った、指定範囲内を不規則に移動する関数
    /// </summary>
    /// <param name="other"></param>
    void Wander(Collider other)
    {     
        if (other.gameObject.CompareTag("NavTarget"))
        {
            Destroy(other.gameObject);

            targetX = Random.Range(-wanderWidth, wanderWidth);
            targetZ = Random.Range(-wanderWidth, wanderWidth);
            beforeTarget = new Vector3(targetX, wanderHeight, targetZ);
            navAgent.destination = new Vector3(targetX, wanderHeight, targetZ);

            Instantiate(navTarget, beforeTarget, Quaternion.identity);

            Debug.Log(beforeTarget);
        }
    }

    /// <summary>
    /// ダメージを受けた時に色を変えるコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator DamageColor()
    {
        changeRenderer.material.color = damageColor;
        yield return new WaitForSeconds(0.5f);
        changeRenderer.material.color = defaultColor;
    }

    /// <summary>
    /// 動作の種類の列挙型
    /// </summary>
    enum MovePatern
    {
        [Tooltip("プレイヤーの座標を追う")] chase,
        [Tooltip("あたりをうろつく")] wander,
        [Tooltip("その場で止まる")] stop,
    }
}
