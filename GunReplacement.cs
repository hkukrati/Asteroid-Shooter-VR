// Gun replacement script- if the player picks up and lets go of the gun, position of the gun is reset and fixed in place in front of the player so it can be picked up again

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunReplacement : MonoBehaviour
{
    [SerializeField] private Pose originPose;
    private XRGrabInteractable gunHandle;

    void Awake()
    {
        gunHandle = GetComponent<XRGrabInteractable>();
        originPose.position = transform.position;
        originPose.rotation = transform.rotation;
    }

    private void OnEnable()
    {
        gunHandle.selectExited.AddListener(GunReleased);
    }

    private void OnDisable()
    {
        gunHandle.selectExited.RemoveListener(GunReleased);
    }

    private void GunReleased(SelectExitEventArgs arg0)
    {
        transform.position = originPose.position;
        transform.rotation = originPose.rotation;
    }
}
