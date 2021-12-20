using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cm;
    [SerializeField] GameObject PausePanel;

    bool isPause = false;
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PausePanel.SetActive(!isPause);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cm.enabled = !cm.enabled;
            isPause = !isPause;
        }
    }
}
