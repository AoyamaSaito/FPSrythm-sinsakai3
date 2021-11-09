using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMove : MonoBehaviour
{
    Animator ui;
    void Awake()
    {
        ui = GetComponent<Animator>();
    }


    public void AnimPlay()
    {
        ui.Play("UIMove");
    }
}
