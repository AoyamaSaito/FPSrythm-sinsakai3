using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalItemTest : ItemBase
{
    [SerializeField] int heal = 20;
    public override void Get()
    {
        base.Col.gameObject.GetComponent<PlayerControler>().PlayerHeal(heal);
    }
}
