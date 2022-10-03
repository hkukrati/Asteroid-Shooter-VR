// Script that is attached to each laser gun. This script controls shooting animations and generates Raycasts to invoke action methods
// in 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class LaserGunScript : MonoBehaviour
{
    // Raycast origin is set as the child of the gun game object. Position is at the end of the gun barrel.
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private Animator laserAnimator;
    [SerializeField] private AudioClip laserSFX;
    [SerializeField] private GameObject muzzleFlash;

    private AudioSource laserAudioSource;
    private RaycastHit hit;

    private void Awake()
    {
        if (GetComponent<AudioSource>() != null)
        {
            laserAudioSource = GetComponent<AudioSource>();
        }
    }

    public void LaserGunFired()
    {
        // Play gun firing animation, play laser sound and create muzzle flash animation at the end of the gun barrel 
        laserAnimator.SetTrigger("Fire");
        laserAudioSource.PlayOneShot(laserSFX);
        Instantiate(muzzleFlash, raycastOrigin.position, raycastOrigin.rotation, raycastOrigin);

        // Try a Raycast that starts at the end of the gun barrel and extends out by 750m (in the z direction)
        // Asteroid prefabs have an attached "AsteroidHit" script. Invoke asteroidCollision() if we shot an asteroid
        // Start Button has a "UserInterface" script. Invoke shotByGun() if we shot the Start button
        if (Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hit, 750f))
        {
            if (hit.transform.GetComponent<AsteroidHit>() != null)
            {
                hit.transform.GetComponent<AsteroidHit>().asteroidCollision();
            }
            else if (hit.transform.GetComponent<UserInterface>()!=null)
            {
                hit.transform.GetComponent<UserInterface>().shotByGun();
            }
        }
    }
}
