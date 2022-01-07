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
    [SerializeField, Tooltip("プレイヤーのスキル")] GameObject skill;
    [SerializeField, Tooltip("プレイヤーの奥義")] GameObject ultimate;

    //カプセル化
    public GameObject Wepon { get { return weapon; } set { weapon = value; } }
    public GameObject HeadEquip { get { return headEquip; } set { headEquip = value; } }
    public GameObject BodyEquip { get { return bodyEquip; } set { bodyEquip = value; } }
    public GameObject Skill { get { return skill; } set { skill = value; } }
    public GameObject Ultimate { get { return ultimate; } set { ultimate = value; } }


    void Start()
    {
        if (Wepon != null)
        {
            Wepon.GetComponent<ItemBase>().Get();
        }

        if (HeadEquip != null)
        {
            HeadEquip.GetComponent<ItemBase>().Get();
        }

        if (BodyEquip != null)
        {
            BodyEquip.GetComponent<ItemBase>().Get();
        }

        if (Skill != null)
        {
            Skill.GetComponent<ItemBase>().Get();
        }

        if (Ultimate != null)
        {
            Ultimate.GetComponent<ItemBase>().Get();
        }
    }
    /// <summary>
    /// 装備品の確認
    /// </summary>
    public void Equip() 
    {
        if(Wepon != null)
        {
            Wepon.GetComponent<ItemBase>().Get();
        }

        if (HeadEquip != null)
        {
            HeadEquip.GetComponent<ItemBase>().Get();
        }

        if (BodyEquip != null)
        {
            BodyEquip.GetComponent<ItemBase>().Get();
        }

        if (Skill != null)
        {
            Skill.GetComponent<ItemBase>().Get();
        }

        if (Ultimate != null)
        {
            Ultimate.GetComponent<ItemBase>().Get();
        }
    }
}
