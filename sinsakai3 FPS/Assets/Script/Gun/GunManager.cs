using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunManager : MonoBehaviour
{
    [SerializeField] int firstBulletCount = 6;
    [System.NonSerialized] public int currrentBulletCount = 0;

    [SerializeField] int reloadCount = 2;

    [SerializeField] Text fullMagagineText; //マガジンの総数のテキスト
    [SerializeField] Text currentMagagineText;　//残弾のテキスト
    [SerializeField] Text reloadText; //リロードの文字のテキスト

    [SerializeField] PistolAnimation pa;

    public int currentReloadCount = 0;

    // Start is called before the first frame update
    void Start()
    {        
        fullMagagineText.text = firstBulletCount.ToString(); //マガジンの総数をテキストに表示する
        
        currrentBulletCount = firstBulletCount;        
        currentMagagineText.text = currrentBulletCount.ToString();　//現在の残弾をテキストに表示する      
    }

    // Update is called once per frame

    /// <summary>
    /// 弾を射撃で消費する処理
    /// </summary>
    public void Shot()
    {
        if (currrentBulletCount > 0)
        {
            currrentBulletCount--;　//弾を消費する
            currentMagagineText.text = currrentBulletCount.ToString();
        }
    }

    /// <summary>
    /// リロードの処理
    /// </summary>
    public void Reload()
    {
        if (currrentBulletCount != firstBulletCount)
        {
            Debug.Log("Reload");
            currentReloadCount++;
            

            if (currentReloadCount == reloadCount)
            {
                pa.Right();

                currrentBulletCount = firstBulletCount; //残弾をMAXにする

                currentReloadCount = 0; //カウントをリセットする

                currentMagagineText.text = currrentBulletCount.ToString();
            }
            else
            {
                pa.Left();
            }
        }
    }

    /// <summary>
    /// リロードをミスしたときはカウントをゼロに
    /// </summary>
    public void ReloadMiss()
    {
        currentReloadCount = 0;
    }
}
