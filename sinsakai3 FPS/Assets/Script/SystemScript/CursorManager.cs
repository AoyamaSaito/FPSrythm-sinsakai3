using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    bool a = true;
    [SerializeField] CinemachineVirtualCamera cm;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            cm.enabled = !cm.enabled;
        }
    }
}
