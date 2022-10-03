// Script that is attached to the "3, 2, 1" animation countdown canvas. This runs after the canvas is instantiated (after user selects "Start Game")

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownScript : MonoBehaviour
{
    private void Update()
    {
        //Make the canvas/animation face the player and destory the canvas after 2 seconds (time it takes for animation to play) 
        transform.LookAt(Camera.main.transform);
        Destroy(gameObject, 2f);
    }

    // Once the object is destroyed, start the game by invoking startGame within the running instance of GameController 
    public void OnDestroy()
    {
        FindObjectOfType<GameController>().startGame();
    }
}