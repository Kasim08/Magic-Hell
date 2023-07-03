using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseViseble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
