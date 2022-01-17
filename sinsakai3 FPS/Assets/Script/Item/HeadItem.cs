using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadItem : ItemBase
{
    [SerializeField, Tooltip("UIの変える色")] Color changeColor;
    [Tooltip("HelmのUI")] Image HelmUi;

    public override void Get()
    {
        HelmUi = GameObject.FindGameObjectWithTag("HelmUI").GetComponent<Image>();
        HelmUi.color = changeColor;
        gameObject.SetActive(false);
    }
}
