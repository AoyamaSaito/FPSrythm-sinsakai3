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
    [SerializeField, Tooltip("Playerが今いるステージ")] int _nowStage = 0;
    [SerializeField, Tooltip("Playerがステージ移動するまでの時間")] float teleportTime = 0.1f;
    [SerializeField] Animator fadePanel;


    [Tooltip("ゲーム内のステージを格納するList")]List<GameObject> inGameStages = new List<GameObject>();
    GameObject[] shuffleStage;
    GameObject player;
    Vector3 respawnPoint;
    PlayerControler playerControler;

    public int nowStage { get => _nowStage; set => _nowStage = value; }

    void Awake()
    {     
        //ステージをシャッフルする
        shuffleStage = stages.OrderBy(i => Guid.NewGuid()).ToArray();
    }

    void Start()
    {
        playerControler = Singleton.playerInstance.GetComponent<PlayerControler>();

        //最初のステージを生成
        inGameStages.Add(Instantiate(firstStage, Vector3.zero, Quaternion.identity ,this.transform));
        //その後のステージをshuffleStageに基づいて、50m間隔で生成
        for(int i = 0; i < shuffleStage.Length; i++)
        {
            inGameStages.Add(Instantiate(shuffleStage[i], new Vector3(0, 0, 50 + 50 * i), Quaternion.identity, this.transform));
            //最後にBossStageを生成する
            if(i == shuffleStage.Length - 1)
            {
                inGameStages.Add(Instantiate(bossStage, new Vector3(0, 0, 50 + 50 * (i + 1)), Quaternion.identity, this.transform));
            }
        }

        respawnPoint = inGameStages[nowStage].transform.Find("Respawn1").position;
        playerControler.PlayerTransform(respawnPoint);
    }

    /// <summary>
    /// 次のステージに移動する関数
    /// </summary>
    public void NextStage()
    {
        fadePanel.Play("FadeAnim");

        nowStage++;
        inGameStages[nowStage - 1].SetActive(false);
        inGameStages[nowStage].SetActive(true);
        respawnPoint = inGameStages[nowStage].transform.Find("Respawn1").position;
        playerControler.PlayerTransform(respawnPoint);
    }

    /// <summary>
    /// 前のステージに移動する関数
    /// </summary>
    public void BackStage()
    {
        fadePanel.Play("FadeAnim");

        nowStage--;
        inGameStages[nowStage + 1].SetActive(false);
        inGameStages[nowStage].SetActive(true);
        respawnPoint = inGameStages[nowStage].transform.Find("Respawn2").position;
        playerControler.PlayerTransform(respawnPoint);
    }

}
