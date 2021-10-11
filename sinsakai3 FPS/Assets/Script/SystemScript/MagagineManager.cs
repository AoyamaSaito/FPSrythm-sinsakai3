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
        fullMagagineText.text = ""+ firstBulletCount;
        currrentBulletCount = firstBulletCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot()
    {
        if(currrentBulletCount > 0)
        {
            currrentBulletCount--;
        }
    }

    public void Reload()
    {
        if (Input.GetKeyDown("Fire2"))
        {
            currentReloadCount++;
            if(currentReloadCount == reloadCount)
            {
                currrentBulletCount = firstBulletCount;
            }
        }
    }
}
