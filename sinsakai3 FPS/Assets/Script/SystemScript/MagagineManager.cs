using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagagineManager : MonoBehaviour
{
    [SerializeField] Text fullMagagineText; //マガジンの総数のテキスト
    [SerializeField] Text currentMagagineText;　//残弾のテキスト
    [SerializeField] Text reloadText; //リロードの文字のテキスト

    [SerializeField] int firstBulletCount = 6;
    int currrentBulletCount = 0;

    [SerializeField] int reloadCount = 2;
    int currentReloadCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        //if (fullMagagineText)
        //{
        fullMagagineText.text = firstBulletCount.ToString(); //マガジンの総数をテキストに表示する
        //}

        currrentBulletCount = firstBulletCount;
        //if (currentMagagineText)
        //{
        currentMagagineText.text = currrentBulletCount.ToString();　//現在の残弾をテキストに表示する
        //}
    }

    // Update is called once per frame

    public void Shot()
    {
        if (currrentBulletCount > 0)
        {
            currrentBulletCount--;
            currentMagagineText.text = currrentBulletCount.ToString();
        }
    }

    public void Reload()
    {
        if (currrentBulletCount != firstBulletCount)
        {
            Debug.Log("Reload");
            currentReloadCount++;

            if (currentReloadCount == reloadCount)
            {
                currrentBulletCount = firstBulletCount; //残弾をMAXにする

                currentReloadCount = 0;

                currentMagagineText.text = currrentBulletCount.ToString();
            }
        }
    }
}
