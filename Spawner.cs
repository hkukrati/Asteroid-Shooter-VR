// Script that is attached to the AsteroidSpawner zone GameObject .
// Its size is currently set to 50 x 150 x 50 in the inspector. 0.8 seconds for the spawn rate.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Spawner : MonoBehaviour
{
    public Vector3 spawnerSize;
    public float spawnRate;

    [SerializeField] private GameObject asteroidModelBlue;
    [SerializeField] private GameObject asteroidModelRed;
    [SerializeField] private GameObject asteroidModelGreen;

    private float spawnTimer = 0f;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnRate)
        {
            SpawnAsteroid();
            spawnTimer = 0;
        }
    }

    private void SpawnAsteroid()
    {
        // Spawn point will be at a random location, witin the AsteroidSpawner zone
        Vector3 spawnPoint = transform.position + new Vector3(UnityEngine.Random.Range(-spawnerSize.x / 2, spawnerSize.x / 2),
                                                              UnityEngine.Random.Range(-spawnerSize.y / 2, spawnerSize.y / 2),
                                                              UnityEngine.Random.Range(-spawnerSize.z / 2, spawnerSize.z / 2));

        // 40% chance of spawning a blue (slow speed) asteroid.
        // 40% chance of spawning a red (medium speed) asteroid
        // 20% chance of spawning a green (fast) asteroid

        Random rnd = new System.Random();
        int randomNumber = rnd.Next(1, 11);

        if(randomNumber <= 4)
        {
            GameObject asteroid = Instantiate(asteroidModelBlue, spawnPoint, transform.rotation);
            asteroid.transform.localScale = new Vector3(7, 7, 7);
        }
        else if (5 <= randomNumber && randomNumber <= 8)
        {
            GameObject asteroid = Instantiate(asteroidModelRed, spawnPoint, transform.rotation);
            asteroid.transform.localScale = new Vector3(7, 7, 7);
        }

        else
        {
            GameObject asteroid = Instantiate(asteroidModelGreen, spawnPoint, transform.rotation);
            asteroid.transform.localScale = new Vector3(7, 7, 7);
        }
    }

    // Make the spawn zone visible in debug with a Gizmo that represents the size of the zone
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(transform.position, spawnerSize);
    }
}
