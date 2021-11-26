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
            anim = GetComponent<Animator>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerEquip>().Wepon = gameObject;
            player.GetComponent<PlayerControler>().GunAnim = anim;

            instantiatePositon = GameObject.FindGameObjectWithTag("GunPosition").transform;
            gunmana = GameObject.FindGameObjectWithTag("GunManager").GetComponent<GunManager>();
            GameObject.FindGameObjectWithTag("Vcam1").transform.SetParent(gameObject.transform);

            Instantiate(gameObject, instantiatePositon.localPosition, Quaternion.identity);
            gunmana.firstBulletCount = firstBullet;
            gunmana.reloadCount = reloadCount;
            gunmana.gunAnim = anim;
            first = false;
        }
    }
}
