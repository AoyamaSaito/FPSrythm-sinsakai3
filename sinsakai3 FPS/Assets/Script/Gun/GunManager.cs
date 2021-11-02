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
    [SerializeField] GameObject[] reloadText; //リロードの文字のテキスト

    [SerializeField] Animator gunAnim;

    int reloadTextCount = 0;
    public int currentReloadCount = 0;

    // Start is called before the first frame update
    void Start()
    {        
        fullMagagineText.text = firstBulletCount.ToString(); //マガジンの総数をテキストに表示する
        
        currrentBulletCount = firstBulletCount;        
        currentMagagineText.text = currrentBulletCount.ToString();　//現在の残弾をテキストに表示する
        // = reloadText.Length / reloadCount                                                           
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
            currentReloadCount++;
            ReloadText();      

            if (currentReloadCount == reloadCount)
            {
                gunAnim.SetBool("Reload2", true);

                StartCoroutine(ReloadReset());

                currrentBulletCount = firstBulletCount; //残弾をMAXにする

                currentReloadCount = 0; //カウントをリセットする

                currentMagagineText.text = currrentBulletCount.ToString();
            }
            else
            {
                gunAnim.SetBool("Reload1", true);
            }
        }
    }


    void ReloadText()
    {
        if(reloadTextCount == 0)
        {
            reloadText[0].SetActive(true);
            reloadText[1].SetActive(true);

            reloadTextCount++;
        }
        else
        {
            reloadText[2].SetActive(true);
            reloadText[3].SetActive(true);

            StartCoroutine(ReloadTextCor());

            reloadTextCount = 0;
        }
    }
    IEnumerator ReloadReset()
    {
        gunAnim.SetBool("Reload1", false);
        yield return new WaitForSeconds(0.2f);
        gunAnim.SetBool("Reload2", false);
    }
    IEnumerator ReloadTextCor()
    {
        yield return new WaitForSeconds(0.4f);
        reloadText[0].SetActive(false);
        reloadText[1].SetActive(false);
        reloadText[2].SetActive(false);
        reloadText[3].SetActive(false);
    }
}
