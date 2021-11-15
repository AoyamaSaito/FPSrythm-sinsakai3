using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] ItemPatern ItemState = default;
    [SerializeField] GetPatern GetState = default;
    [SerializeField] GameObject[] ItemUI = default;

    public Collider col;
    public abstract void Get();

    void Start()
    {
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

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        col = other;
        switch (GetState)
        {
            case GetPatern.pickup:
                Array.ForEach(ItemUI, g => g.SetActive(true));
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
                    Get();
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
                    Get();
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
        //体で拾う
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
