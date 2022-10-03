// AsteroidHit script that is attached to every Asteroid GameObject.
// This script defines the behavior that occurs after the asteroid is successfully shot by the player
//
// From the laser gun script:
// hit.transform.GetComponent<AsteroidHit>().asteroidCollision() is invoked upon a successful RayCast hit event with an Asteroid (i.e. an asteroid is shot)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AsteroidHit : MonoBehaviour
{
    // asteroidExplosion gameobject is an explosion animation prefab
    [SerializeField] private GameObject asteroidExplosion;

    // scorePopup is tied to a Canvas prefab which has an animation of text (score) popping up
    [SerializeField] private Canvas scorePopup;

    [SerializeField] private GameController gameController;

    private int points;

    private void Awake()
    {
        // Find the one instance of GameController that is running in the game and set gameController to be a reference to it
        gameController = FindObjectOfType<GameController>();
    }

    public void asteroidCollision()
    {
        // Instantiate the asteroidExplosion animation prefab at the exact place where the asteroid currently is
        Instantiate(asteroidExplosion, transform.position, transform.rotation);

        // If we are currently in a Game Session, we need to calculate score, display it to the user in the popup animation, and add it to the current score
        if (gameController.currentGameStatus == GameController.GameState.Playing) 
        {
            points = calculatePoints(tag);
            gameController.updateScore(points);

            scorePopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + points.ToString();
            Instantiate(scorePopup, transform.position, transform.rotation);
        }

        Destroy(this.gameObject);
    }

    // Calculate points based on asteroidTag. There are three different asteroid prefabs in the game with their own unique tags:
    // "AsteroidBlue", "AsteroidRed" and "AsteroidGreen"
    // Base number of points is the raw distance between the player and the asteroid
    // Score multipler is 1.2 for a red asteroid, 1.5 for a green asteroid 
    private int calculatePoints(string asteroidTag)
    {
        float scoreMultiplier = 0;
        float distanceFromPlayer = Vector3.Distance(transform.position, gameController.transform.position);

        switch(asteroidTag)
        {
            case ("AsteroidBlue"):
                scoreMultiplier = 1;
                break;

            case ("AsteroidRed"):
                scoreMultiplier = 1.2f;
                break;

            case ("AsteroidGreen"):
                scoreMultiplier = 1.5f;
                break;
        }

        return (int)(scoreMultiplier * distanceFromPlayer);
    }
}
