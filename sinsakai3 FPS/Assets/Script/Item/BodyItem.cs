using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BodyItem : ItemBase
{
    [SerializeField, Tooltip("UIの変える色")] Color changeColor;
    [Tooltip("ArmorのUI")] Image armorUi;
    [SerializeField, Tooltip("Uiのタグ")] string uiTag = "ArmorUI"; 

    public override void Get()
    {
        Singleton.playerInstance.GetComponent<PlayerControler>().def = 5;
        armorUi = GameObject.FindGameObjectWithTag(uiTag).GetComponent<Image>();
        armorUi.color = changeColor;
        gameObject.SetActive(false);
    }
}
