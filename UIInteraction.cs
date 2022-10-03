
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIInteraction : MonoBehaviour
{
    public UnityEvent onHitByRaycast;

    public void HitByRayCast()
    {
        onHitByRaycast.Invoke(); 
    }
}
