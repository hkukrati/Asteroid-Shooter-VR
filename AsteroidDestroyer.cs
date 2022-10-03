// Script for the "Asteroid Destroyer" zone, which is an invisible GameObject that contains a rigidbody and box collider. Acts as the boundary for the asteroids that are passing in front of the player.
// Upon entering this zone, the asteroid will be destroyed and removed from the game. The box collider on this GameObject acts as the trigger for collision events.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
