using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] int hp = 10;
    [SerializeField] float chaseSpeed = 4;
    [SerializeField] float navSpeed = 6;
    [SerializeField] Renderer r;
    [SerializeField] Color damageColor = default;
    [Header("高さ")]
    [SerializeField] float minHeight = 3;
    [SerializeField] float maxHeight = 5;
    [SerializeField] float wanderHeight = 4f;
    [Header("距離")]
    [SerializeField] float chaseDistance = 1.7f;
    [SerializeField] float wanderWidth = 14f;
    [SerializeField] GameObject navTarget = default;
    [Header("プレイヤー追跡パターン")]
    [SerializeField] MovePatern moveState = default;
    
    RaycastHit hit;
    Rigidbody rb;
    Collider col;
    NavMeshAgent navAgent;
    GameObject player;
    Color defaultColor;
    float kyori = 0;
    float chaseHeight = 0;
    float targetX = 0;
    float targetZ = 0;
    Vector3 beforeTarget = default;

    void Start()
    {
        defaultColor = r.material.color;

        switch (moveState)
        {
            case MovePatern.chase:
                player = GameObject.FindGameObjectWithTag("Player");
                rb = GetComponent<Rigidbody>();
                navAgent = GetComponent<NavMeshAgent>();
                navAgent.enabled = false;
                chaseHeight = Random.Range(minHeight, maxHeight);
                transform.position = new Vector3(gameObject.transform.position.x, chaseHeight, gameObject.transform.position.z);
                break;
            case MovePatern.wander:
                rb = GetComponent<Rigidbody>();
                navAgent = GetComponent<NavMeshAgent>();
                rb.isKinematic = false;
                navAgent.enabled = true;
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
        transform.LookAt(player.transform.position);

        switch (moveState)
        {
            case MovePatern.wander:
                navAgent.speed = Mathf.PerlinNoise(gameObject.transform.position.x, gameObject.transform.position.z) * navSpeed;
                break;
        }

        Attack();
    }

    void FixedUpdate()
    {
        switch (moveState)
        {
            case MovePatern.chase:
                Chase();
                break;
        }
    }

    public abstract void Attack();

    /// <summary>
    /// 引数にダメージを設定する
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        Debug.Log($"Damage{damage}");
        hp -= damage;
        StartCoroutine(DamageColor());

        if (hp <= 0) //HPがゼロになったら死ぬ処理をする
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーとの距離が一定以内なら追跡する
    /// </summary>
    void Chase()
    {

        Vector3 playerPosition = new Vector3(player.transform.position.x, chaseHeight, player.transform.position.z);
        Vector3 myPosition = new Vector3(gameObject.transform.position.x, chaseHeight, gameObject.transform.position.z);

        if (Physics.Linecast(gameObject.transform.position, player.transform.position, out hit))
        {
            kyori = hit.distance;
        }

        if(kyori >= chaseDistance)
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

    IEnumerator DamageColor()
    {
        r.material.color = damageColor;
        yield return new WaitForSeconds(0.5f);
        r.material.color = defaultColor;
    }
    enum MovePatern
    {
        //プレイヤーの座標を追う
        chase,
        //あたりをうろつく
        wander,
    }
}
