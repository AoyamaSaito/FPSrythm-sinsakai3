using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    [Header("装備")]
    [SerializeField] GameObject weapon;
    [SerializeField] GameObject headEquip;
    [SerializeField] GameObject bodyEquip;
    [SerializeField] GameObject skill;
    [SerializeField] GameObject ultimate;

    /// <summary>
    /// カプセル化
    /// </summary>
    public GameObject Wepon{ get { return weapon; } set { weapon = value; } }
    public GameObject HeadEquip { get { return headEquip; } set { headEquip = value; } }
    public GameObject BodyEquip { get { return bodyEquip; } set { bodyEquip = value; } }
    public GameObject Skill { get { return skill; } set { skill = value; } }
    public GameObject Ultimate { get { return ultimate; } set { ultimate = value; } }

    /// <summary>
    /// 装備品の確認
    /// </summary>
    void Update()
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
