﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : ItemBase
{
    [SerializeField] int heal = 20;

    public override void Get()
    {
        base.col.gameObject.GetComponent<PlayerControler>().PlayerHeal(heal);
    }
}