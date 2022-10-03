// Script that is attached to the MuzzleFlash animation prefab

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlashScript : MonoBehaviour
{
    // Make the muzzle flash 20% of its prefab size
    void Awake()
    {
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
    }
}
