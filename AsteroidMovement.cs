// Script attached to each asteroid prefab that controls its movement

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    // Asteroid max speed, min speed are different for asteroids of different colors
    // Blue asteroids are slower than red asteroids, which are slower than green asteroids
    // Movement direction is set to the negative z direction (0,0,-1) relative to the world
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private Vector3 movementDirection;

    private float xAngle, yAngle, zAngle;
    private float asteroidBaseSpeed;

    void Start()
    {
        asteroidBaseSpeed = Random.Range(minSpeed, maxSpeed);

        xAngle = Random.Range(0, 360);
        yAngle = Random.Range(0, 360);
        zAngle = Random.Range(0, 360);

        // Rotate the asteroid at random x, y and z angles
        transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    void Update()
    {
        // Move the asteroid with a randomly selected speed
        transform.Translate(movementDirection * Time.deltaTime * asteroidBaseSpeed, Space.World);

        //Rotate asteroid in all directions as asteroid is moving
        transform.Rotate(Vector3.up * Time.deltaTime * 20, Space.World);
        transform.Rotate(Vector3.right * Time.deltaTime * 20, Space.World);
        transform.Rotate(Vector3.forward * Time.deltaTime * 20, Space.World);
    }
}
