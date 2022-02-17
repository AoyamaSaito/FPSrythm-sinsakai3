using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//色々説明
//仕組み
//1,音を鳴らすGameObjectをゲームを始まる一番最初にMaxCountの数生成する
//2,AudioSorceが入ったプレハブのOnOffを切り替えて音を鳴らす

//音を追加したいとき
//1,SoundManager内のSoundTypeに、追加したい音の名前の列挙子を追加する
//2,空のGameObjectにAudioSourceをアタッチし、流したい音を入れ、PlayOnAwakeにチェックしてPrefab化
//3,SoundManagerのインスペクター内で１で追加した列挙子のPrefabに、２のGameObjectを追加する

//音を使いたいとき
//Singletonクラスになっているので、SoundManager.Instance.UseSound(鳴らしたい音の列挙子)で鳴らせる

/// <summary>
/// サウンドを管理するスクリプト
/// </summary>
public class SoundManager : SingletonMonoBehaviour<SoundManager>
{
    [SerializeField, Tooltip("OnOffを切り替える時間")] float onTime = 1;
    [SerializeField, Tooltip("各種設定を保存するList")] SoundPoolParams soundObjParam = default;

    [Tooltip("生成したオブジェクトを保存するPoolクラスのList")] List<Pool> pool = new List<Pool>();
    [Tooltip("今どのSoundTypeを生成しているかCountする変数")]int poolCountIndex = 0;

    protected override void OnAwake()
    {
        poolCountIndex = 0;
        CreatePool();
    }

    /// <summary>各種Prefabを生成する</summary>
    private void CreatePool()
    {
        //全ての生成が終わったらreturnして生成を止める
        if (poolCountIndex >= soundObjParam.Params.Count)
        {
            return;
        }

        //設定してあるPrefabを生成する
        for (int i = 0; i < soundObjParam.Params[poolCountIndex].MaxCount; i++)
        {
            var bullet = Instantiate(soundObjParam.Params[poolCountIndex].Prefab, this.transform);
            bullet.SetActive(false);
            SavePool(bullet, soundObjParam.Params[poolCountIndex].Type);
        }

        poolCountIndex++;　//一つ生成が終わったらcountを増やして次のParamに行く
        CreatePool(); //再起呼び出し
    }

    /// <summary>
    /// 音を流すときに呼び出す関数
    /// </summary>
    /// <param name="soundType">流したいサウンドの種類</param>
    /// <returns></returns>
    public GameObject UseSound(SoundType soundType)
    {
        foreach (var pool in pool)
        {
            //SetActiveがFalseで、SoundTypeが同じものをTrueにする
            if (pool.Object.activeSelf == false && pool.Type == soundType)
            {
                StartCoroutine(OnOffCor(pool.Object));
                return pool.Object;
            }
        }

        //もし生成してある分で足らなくなったら、新しく生成する
        var newSound = Instantiate(soundObjParam.Params.Find(x => x.Type == soundType).Prefab, this.transform);
        StartCoroutine(OnOffCor(newSound));
        SavePool(newSound, soundType);
        return newSound;
    }

    /// <summary>OnOffを一定時間ごとに切り替えるコルーチン</summary>
    /// <param name="go"></param>
    /// <returns></returns>
    IEnumerator OnOffCor(GameObject go)
    {
        go.SetActive(true);
        yield return new WaitForSeconds(onTime);
        go.SetActive(false);
    }

    /// <summary>生成したオブジェクトをListに追加して保存する関数</summary>
    /// <param name="go">生成したオブジェクト</param>
    /// <param name="type">生成したオブジェクトのSoundType</param>
    void SavePool(GameObject go, SoundType type)
    {
        //Listに追加して保存する
        pool.Add(new Pool { Object = go, Type = type });
    }

    /// <summary>生成した音を保存しておく為のクラス</summary>
    private class Pool
    {
        public GameObject Object { get; set; }
        public SoundType Type { get; set; }
    }
}

/// <summary>音の種類の列挙型</summary>
public enum SoundType
{
    //ここに音の列挙型を追加する
    Shot,
    Reload,
    Dodge,
    Damage,
    Tambarin
}

/// <summary>SoundPoolParamのList</summary>
[System.Serializable]
public class SoundPoolParams
{
    [SerializeField] public List<SoundPoolParam> Params = new List<SoundPoolParam>();
}

/// <summary>Soundの各種パラメータ</summary>
[System.Serializable]
public class SoundPoolParam
{
    [SerializeField] public SoundType Type = default;

    [SerializeField, Tooltip("Soundのプレハブ")] public GameObject Prefab;

    [SerializeField, Tooltip("Prefabの生成数")] public int MaxCount;
}
