using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    [SerializeField, Tooltip("最初に呼び出されるステージ")] GameObject firstStage;
    [SerializeField, Tooltip("SetActiveをfalseにしてください")] GameObject[] stages;
    [SerializeField, Tooltip("Bossステージを設定してください")] GameObject bossStage;
    [SerializeField] int _nowStage = 0;
    [SerializeField] float respawnTime = 0.1f;
    [SerializeField] GameObject fadePanel;

    
    List<GameObject> inGameStages = new List<GameObject>();
    GameObject[] shuffleStage;
    GameObject player;
    Vector3 respawnPoint;
    Animator fadeAnim;

    public int nowStage { get => _nowStage; set => _nowStage = value; }

    void Awake()
    {     
        shuffleStage = stages.OrderBy(i => Guid.NewGuid()).ToArray();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Start()
    {
        inGameStages.Add(Instantiate(firstStage, Vector3.zero, Quaternion.identity));
        for(int i = 0; i < shuffleStage.Length; i++)
        {
            inGameStages.Add(Instantiate(shuffleStage[i], new Vector3(0, 0, 50 + 50 * i), Quaternion.identity));
            if(i == shuffleStage.Length - 1)
            {
                inGameStages.Add(Instantiate(bossStage, new Vector3(0, 0, 50 + 50 * (i + 1)), Quaternion.identity));
            }
        }

        respawnPoint = inGameStages[nowStage].transform.Find("Respawn1").position;
        player.transform.position = respawnPoint;

        fadeAnim = fadePanel.GetComponent<Animator>(); 
    }

    public void NextStage()
    {
        Debug.Log("NextStage");
        nowStage++;
        fadeAnim.Play("FadeAnim");
        inGameStages[nowStage - 1].SetActive(false);
        inGameStages[nowStage].SetActive(true);
        respawnPoint = inGameStages[nowStage].transform.Find("Respawn1").position;
        StartCoroutine(RespawnPlayer());
    }

    public void BackStage()
    {
        Debug.Log("BackStage");
        nowStage--;
        fadeAnim.Play("FadeAnim");
        inGameStages[nowStage + 1].SetActive(false);
        inGameStages[nowStage].SetActive(true);
        respawnPoint = inGameStages[nowStage].transform.Find("Respawn2").position;
        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(respawnTime);
        player.transform.position = respawnPoint;

    }
}
