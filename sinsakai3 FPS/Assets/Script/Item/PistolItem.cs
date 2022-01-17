using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ItemBaseを継承したPistolのアイテム
public class PistolItem : ItemBase
{
    [SerializeField] int firstBullet = 6;
    [SerializeField] int reloadCount = 2;

    GunManager gunmana;
    Animator anim;
    Transform instantiatePositon;
    [Tooltip("初回か")] bool first = false;


    public override void Get()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerEquip>().Wepon = gameObject;

        instantiatePositon = GameObject.FindGameObjectWithTag("GunPosition").transform;
        gunmana = GameObject.FindGameObjectWithTag("GunManager").GetComponent<GunManager>();
        Transform vcam = GameObject.FindGameObjectWithTag("Vcam1").transform;

        GameObject go = Instantiate(gameObject, instantiatePositon.localPosition, Quaternion.identity); //インスタンス化
        go.transform.SetParent(vcam);
        anim = go.GetComponent<Animator>();

        gunmana.FirstBulletCount = firstBullet;
        gunmana.ReloadCount = reloadCount;
        player.GetComponent<PlayerControler>().GunAnim = anim;
        gunmana.gunAnim = anim;
    }
}
