using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[ToDO] ReloadTextCorを完成させる

/// <summary>
/// 銃の残弾管理
/// リロードするためにはreloadCountの回数分決められたボタンを押す
/// </summary>
public class GunManager : MonoBehaviour
{
    [SerializeField, Tooltip("弾の最大数")] int fullBulletCount = 6;
    [System.NonSerialized, Tooltip("現在の残弾")] public int currrentBulletCount = 0;

    [SerializeField, Tooltip("リロードに必要な入力の回数")] int reloadCount = 2;

    [SerializeField, Tooltip("弾の最大数のText")] Text fullMagagineText;
    [SerializeField, Tooltip("現在の残弾のText")] Text currentMagagineText;
    [SerializeField, Tooltip("リロードの文字のText")] GameObject[] reloadText;

    [SerializeField, Tooltip("銃のアニメーター")] Animator _gunAnim;

    int reloadTextCount = 0;
    [System.NonSerialized, Tooltip("現在のリロードの入力の回数")]public int currentReloadCount = 0;

    public int FirstBulletCount { get => fullBulletCount; set => fullBulletCount = value; }
    public int ReloadCount { get => reloadCount; set => reloadCount = value; }
    public Animator gunAnim { get => _gunAnim; set => _gunAnim = value; }

    // Start is called before the first frame update
    void Start()
    {        
        fullMagagineText.text = fullBulletCount.ToString(); //マガジンの総数をテキストに表示する
        
        currrentBulletCount = fullBulletCount;        
        currentMagagineText.text = currrentBulletCount.ToString();　//現在の残弾をテキストに表示する                                                           
    }

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
        //弾を消費していたら
        if (currrentBulletCount != fullBulletCount)
        {
            SoundManager.Instance.UseSound(SoundType.Reload);
            currentReloadCount++;
            ReloadText();      

            //リロードが完了したら
            if (currentReloadCount == reloadCount)
            {
                _gunAnim.SetBool("Reload2", true);

                StartCoroutine(AnimatorBoolReset());

                currrentBulletCount = fullBulletCount; //残弾をMAXにする

                currentReloadCount = 0; //カウントをリセットする

                currentMagagineText.text = currrentBulletCount.ToString();
            }
            else
            {
                _gunAnim.SetBool("Reload1", true);
            }
        }
    }

    void ReloadText()
    {
        if(reloadTextCount == 0)
        {
            reloadText[0].SetActive(true);

            reloadTextCount++;
        }
        else
        {
            reloadText[1].SetActive(true);

            StartCoroutine(ReloadTextCor());

            reloadTextCount = 0;
        }
    }

    /// <summary>
    /// Animatorのboolをリセットするコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator AnimatorBoolReset()
    {
        _gunAnim.SetBool("Reload1", false);
        yield return new WaitForSeconds(0.2f);
        _gunAnim.SetBool("Reload2", false);
    }

    /// <summary>
    /// ReloadのTextを表示するコルーチン
    /// </summary>
    /// <returns></returns>
    IEnumerator ReloadTextCor()
    {
        yield return new WaitForSeconds(0.4f);
        reloadText[0].SetActive(false);
        reloadText[1].SetActive(false);
    }
}
