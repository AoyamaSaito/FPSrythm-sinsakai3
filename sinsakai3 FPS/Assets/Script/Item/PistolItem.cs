using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolItem : ItemBase
{
    [SerializeField] int firstBullet = 6;
    [SerializeField] int reloadCount = 2;

    bool first = false;
    GunManager gunmana;
    Animator anim;
    Transform instantiatePositon;
    private void Start()
    {
       
    }

    public override void Get()
    {
        if(!first)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerEquip>().Wepon = gameObject;

            instantiatePositon = GameObject.FindGameObjectWithTag("GunPosition").transform;
            gunmana = GameObject.FindGameObjectWithTag("GunManager").GetComponent<GunManager>();
            Transform vcam = GameObject.FindGameObjectWithTag("Vcam1").transform;

            GameObject go = Instantiate(gameObject, instantiatePositon.localPosition, Quaternion.identity);
            go.transform.SetParent(vcam);
            anim = go.GetComponent<Animator>();

            gunmana.firstBulletCount = firstBullet;
            gunmana.reloadCount = reloadCount;
            player.GetComponent<PlayerControler>().GunAnim = anim;
            gunmana.gunAnim = anim;

            first = false;
        }
    }
}
