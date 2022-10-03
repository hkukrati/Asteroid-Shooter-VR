// Script that is attached to the Score Popup Canvas prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    // Face the popup in the players direction and destroy after 0.8 seconds
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        Destroy(gameObject, 0.8f);
    }
}
