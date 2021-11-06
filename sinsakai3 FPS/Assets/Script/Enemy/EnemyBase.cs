using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class EnemyBase : MonoBehaviour
{
    [Header("ステータス")]
    [SerializeField] int hp = 10;
    [SerializeField] float chaseSpeed = 4;
    [Header("高さ")]
    [SerializeField] float minHeight = 3;
    [SerializeField] float maxHeight = 5;
    [Header("近づく距離")]
    [SerializeField] float chaseDistance = 1.7f;
    [Header("プレイヤー追跡パターン")]
    [SerializeField] MovePatern moveState = default;
    

    GameObject[] targets;
    RaycastHit hit;
    Rigidbody rb;
    NavMeshAgent navAgent;
    GameObject player;
    float kyori = 0;
    float timer = 0;
    float chaseHeight = 0;
    int beforeIndex = 0;
    int targetIndex = 0;

    void Start()
    {
        switch (moveState)
        {
            case MovePatern.chase:
                rb = GetComponent<Rigidbody>();
                navAgent = GetComponent<NavMeshAgent>();
                navAgent.enabled = false;
                player = GameObject.FindGameObjectWithTag("Player");
                chaseHeight = Random.Range(minHeight, maxHeight);
                transform.position = new Vector3(gameObject.transform.position.x, chaseHeight, gameObject.transform.position.z);
                break;
            case MovePatern.prowl:
                navAgent = GetComponent<NavMeshAgent>();
                navAgent.enabled = true;
                targets = GameObject.FindGameObjectsWithTag("NavTarget");
                targetIndex = Random.Range(0, targets.Length - 1);
                navAgent.destination = targets[targetIndex].transform.position;
                break;
        }            
    }

    void Update()
    {
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
        hp -= damage;

        if(hp <= 0) //HPがゼロになったら死ぬ処理をする
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// プレイヤーとの距離が一定以内なら追跡する
    /// </summary>
    void Chase()
    {
        timer += Time.deltaTime;

        Vector3 playerPosition = new Vector3(player.transform.position.x, chaseHeight, player.transform.position.z);
        Vector3 myPosition = new Vector3(gameObject.transform.position.x, chaseHeight, gameObject.transform.position.z);

        transform.LookAt(playerPosition);

        if (Physics.Linecast(gameObject.transform.position, player.transform.position, out hit))
        {
            kyori = hit.distance;
            if(3 <= timer)
            {
                Debug.Log(kyori);
                timer = 0;
            }
        }

        if(kyori >= chaseDistance)
        {
            Debug.Log("chase");
            rb.velocity = (playerPosition - myPosition).normalized * chaseSpeed;
        }
        else
        {
            Debug.Log("not chase");
            rb.velocity = new Vector3(0, 0, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Wander(other);
    }
    void Wander(Collider collision)
    {
        navAgent.destination = targets[targetIndex].transform.position;

        if (collision.gameObject.tag == "NavTarget")
        {
            beforeIndex = targetIndex;
            targetIndex = Random.Range(0, targets.Length - 1);
            navAgent.destination = targets[Judge(targetIndex)].transform.position;
            Debug.Log(targetIndex);
        }
    }
    
    int Judge(int n)
    {
        if(n != beforeIndex)
        {
            return n;
        }
        else
        {
            return Judge(Random.Range(0, targets.Length - 1));
        }
    }

    enum MovePatern
    {
        //プレイヤーの座標を追う
        chase,
        //あたりをうろつく
        prowl,
    }
}
