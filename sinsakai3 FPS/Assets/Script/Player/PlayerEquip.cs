using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Playerの装備管理
/// </summary>
public class PlayerEquip : MonoBehaviour
{
    [Header("装備")]
    [SerializeField, Tooltip("プレイヤーの武器")] GameObject weapon;
    [SerializeField, Tooltip("プレイヤーの頭装備")] GameObject headEquip;
    [SerializeField, Tooltip("プレイヤーの胴体装備")] GameObject bodyEquip;

    GameObject currentWeapon;
    GameObject currentHead;
    GameObject currentBody;

    //カプセル化
    public GameObject Wepon { get { return weapon; } set { weapon = value; } }
    public GameObject HeadEquip { get { return headEquip; } set { headEquip = value; } }
    public GameObject BodyEquip { get { return bodyEquip; } set { bodyEquip = value; } }


    void Start()
    {
        if (Wepon != null)
        {
            if (currentWeapon != Wepon)
            {
                Wepon.GetComponent<ItemBase>().Get();
                currentWeapon = Wepon;
            }
        }

        if (HeadEquip != null)
        {
            if (currentHead != HeadEquip)
            {
                HeadEquip.GetComponent<ItemBase>().Get();
                currentHead = HeadEquip;
            }
        }

        if (BodyEquip != null)
        {
            if (currentBody != BodyEquip)
            {
                BodyEquip.GetComponent<ItemBase>().Get();
                currentBody = BodyEquip;
            }
        }
    }
    /// <summary>
    /// 装備品の確認
    /// </summary>
    public void Equip()
    {
        if (Wepon != null)
        {
            if (currentWeapon != Wepon)
            {
                ItemBase itemBase = Wepon.GetComponent<ItemBase>();
                itemBase.Out();
                itemBase.Get();
                currentWeapon = Wepon;
            }
        }

        if (HeadEquip != null)
        {
            if (currentHead != HeadEquip)
            {
                ItemBase itemBase = HeadEquip.GetComponent<ItemBase>();
                itemBase.Out();
                itemBase.Get();
                currentHead = HeadEquip;
            }
        }

        if (BodyEquip != null)
        {
            if (currentBody != BodyEquip)
            {
                ItemBase itemBase = BodyEquip.GetComponent<ItemBase>();
                itemBase.Out();
                itemBase.Get();
                currentBody = BodyEquip;
            }
        }
    }
}
