using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 取得できるアイテムの基底クラス
/// </summary>
public abstract class ItemBase : MonoBehaviour
{
    [Header("アイテムの種別")]
    [SerializeField, Tooltip("アイテムの種類")] ItemPatern ItemState = default;
    [SerializeField, Tooltip("アイテムの取得方法")] GetPatern GetState = default;
    [SerializeField, Tooltip("ゲーム上に表示するUI")] GameObject[] ItemUI = default;

    PlayerEquip playerEquip;
    [NonSerialized] private Collider col;

    public Collider Col { get => col; set => col = value; }

    /// <summary>
    /// 取得したときの処理
    /// </summary>
    public abstract void Get();

    void Start()
    {
        playerEquip = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerEquip>();

        switch (GetState)
        {
            case GetPatern.pickup:
                break;
            case GetPatern.get:
                break;
        }
        switch (ItemState)
        {
            case ItemPatern.normal:
                break;
            case ItemPatern.head:
                break;
            case ItemPatern.body:
                break;
            case ItemPatern.weapon:
                break;
            case ItemPatern.skill:
                break;
            case ItemPatern.ultimate:
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        col = other;
        switch (GetState)
        {
            case GetPatern.pickup:
                Array.ForEach(ItemUI, g => g.SetActive(true)); //Triggerに入ったらUIを表示する
                break;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            switch (ItemState)
            {
                case ItemPatern.normal:
                    NormalItem();
                    break;
                case ItemPatern.head:
                    HeadItem();
                    break;
                case ItemPatern.body:
                    BodyItem();
                    break;
                case ItemPatern.weapon:
                    WeaponItem();
                    break;
                case ItemPatern.skill:
                    SkillItem();
                    break;
                case ItemPatern.ultimate:
                    UltimateItem();
                    break;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                Array.ForEach(ItemUI, g => g.SetActive(false));
                break;
        }
    }

    void NormalItem()
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                if (Input.GetKeyDown("f"))
                {
                    Get();
                }
                break;
            case GetPatern.get:
                    Get();
                break;
        }
    }

    void HeadItem()
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                if (Input.GetKeyDown("f"))
                {
                    playerEquip.HeadEquip = gameObject;
                }
                break;
        }
    }

    void BodyItem()
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                if (Input.GetKeyDown("f"))
                {
                    playerEquip.BodyEquip = gameObject;
                }
                break;
        }
    }

    void WeaponItem()
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                if (Input.GetKeyDown("f"))
                {
                    Get();
                }
                break;
        }
    }

    void SkillItem()
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                if (Input.GetKeyDown("f"))
                {
                    Get();
                }
                break;
        }
    }

    void UltimateItem()
    {
        switch (GetState)
        {
            case GetPatern.pickup:
                if (Input.GetKeyDown("f"))
                {
                    Get();
                }
                break;
        }
    }

    enum GetPatern
    {
        //触れたら入手する
        pickup,
        //ボタンで入手する
        get,

    }

    enum ItemPatern
    {
        //回復薬、コイン、鍵
        normal,
        //頭の装備
        head,
        //胴の装備
        body,
        //武器
        weapon,
        //スキル
        skill,
        //奥義
        ultimate,
    }
}
