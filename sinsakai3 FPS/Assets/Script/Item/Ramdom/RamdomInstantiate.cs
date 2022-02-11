using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RamdomInstantiate : MonoBehaviour
{
    [SerializeField, Tooltip("生成するアイテム")] GameObject[] items;

    [Tooltip("生成するアイテムのラベル")]　int number = 0;

    void Start()
    {
        number = Random.Range(0, items.Length - 1);

    }

    public void Pop()
    {
        Instantiate(items[number], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
