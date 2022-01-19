using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ItemのUIが常にプレイヤーの方向を向くようにする
/// </summary>
public class ItemUIRotate : MonoBehaviour
{
    [SerializeField, Tooltip("PlayerのTag")] string playerTag = "Player";

    [Tooltip("ItemのRectTransform")] Transform myRectTransform;
    [Tooltip("Playerの座標を入れるための変数")] Vector3 playerPosition;
    void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }

    
    void Update()
    {
        //Payerの座標を取得してその方向を向く
        playerPosition = GameObject.FindGameObjectWithTag(playerTag).GetComponent<Transform>().position;

        Vector3 vector3 = playerPosition - this.transform.position;
        vector3.y = 0f;
        Quaternion quaternion = Quaternion.LookRotation(vector3);
        myRectTransform.rotation = quaternion;
    }
}
