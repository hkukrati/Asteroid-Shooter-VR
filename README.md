# Asteroid-Shooter-VR
Asteroid Shooter game for Meta Quest 2. Designed and built with Unity Engine and C#. Scripts developed for the game are included in this repo.

The player has 30 seconds to shoot as many asteroids as possible to achieve the highest score. Points are calculated based on the distance between the player and the asteroid, as well as the asteroid type. Green asteroids move the fastest and generate the highest number of points, followed by red and then blue.

Gameplay Video: https://www.youtube.com/watch?v=l4p8KbXqyVY

<img width="1023" alt="Screen Shot 2022-10-03 at 9 19 23 PM" src="https://user-images.githubusercontent.com/7616530/193733445-bbe13082-a6eb-4102-b13a-b62344dddce2.png">

GameController.cs manages the entire flow of the game. It holds the current game state and includes methods to start the game, hide/show UI elements, initiate timers etc. 

Core shooting functionality is implemented in LaserGunScript.cs (attached to the gun GameObjects) through a raycast from the end of the gun barrel in the forward direction. Upon a successful hit event with an asteroid, the score is computed (by invoking asteroidCollision() in AsteroidHit.cs) and the asteroid is destroyed. 

Spawner.cs is attached to the SpawnerZone GameObject and controls the rate of asteroid spawning, the position within the zone where it will spawn, as well as the logic behind choosing which type of asteroid will be instantiated (40% chance for blue, 40% for red and 20% for green)

AsteroidMovement.cs and AsteroidHit.cs are attached to each asteroid prefab. AsteroidMovement.cs controls the translation, rotation and speed of an asteroid. These settings are configured differently for each type of asteroid through the inspector (e.g. faster speed for green asteroids). AsteroidHit.cs  defines the behavior that occurs after an asteroid is successfully hit- the score is computed based on the distance between player and asteroid, along with the score multiplier. The explosion animation prefab and score popup prefab is instantiated in place of the asteroid, and the asteroid is destroyed.

PopupControl.cs is tied to the Score Canvas gameobject that gets instantiated in the place of an asteroid upon a successful hit event and displays the score to the user with a bounce/fading animation. The actual score generated is set in AsteroidHit.cs (scorePopup.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "+" + points.ToString();). In this script, the canvas is simply destroyed after 0.8s and is configured to face the user's direction.

AsteroidDestroyer.cs is attached to the "asteroid kill zone", which is an invisible GameObject that has a rigidbody and collider. The collider for this game object is set as the trigger, so that when other asteroids come into contact with it, they are immediately destroyed. 

---------------------------------------------------------------------------------------------------------------------------------------------------------
The flow of a full game is as follows:

- Gamecontroller.cs has currentGameStatus equal to "GameState.Menu". The StartMenu canvas is enabled and the user is able to see the "Start Menu" text object. All other UI menu items (game over text, score, timer etc.) are hidden in this GameState.

- To start the game, user shoots at the "Start Menu". Through the XRGrabInteractable script thats attached to the guns, pulling the trigger on the controller to shoot the gun invokes the Activated (ActivateEventArgs) event, which is tied to The LaserGunFired() method in LaserGunScript.cs (also attached to the the gun gameobject). LaserGunFired() generates a raycast that starts at the end of the gun barrel (rayCastorigin, which is a child of the gun object). The StartMenu text object has a UserInterface.cs script attached to it; the transform attached to the hit is checked to see if contains a "UserInterface" script attached to it. If it does, then it means we have shot the Start Menu text object and we should invoke its "shotByGun()" method:

 else if (hit.transform.GetComponent<UserInterface>()!=null)
            {
                hit.transform.GetComponent<UserInterface>().shotByGun();
            }
            
shotByGun() is a function that's present in the UserInterface.cs script. Within shotByGun() is a UnityEvent called onShotByGun. This UnityEvent is invoked, and performs the following actions:
 
 - The Canvas prefab that holds the "3, 2, 1" animation is instantiated in the same position as the Start Menu canvas
 - Hide the Start Menu canvas
 - Hide the Game Over and Score text (which be referenced later when the game ends and we want to restart the game)
 
 These actions for the UnityEvent are all set in the Inspector.
 
 - The "Countdown" canvas is instantiated in the same location as the start menu. The Countdown canvas holds a TextMeshPro object that has an animation attached to it that transitions from "3, 2, 1" with a growing/shrinking effect. CountDownScript.cs is attached to the CountDown canvas prefab and is set to be destroyed after 2 seconds (time it takes for the 3, 2, 1 transition to occur). It has an onDestroy() method, which starts the game by doing the following:
 
  public void OnDestroy()
    {
        FindObjectOfType<GameController>().startGame();
    }
    
 So when the 3, 2, 1 animation is completed, we find the running instance of GameController and invoke its startGame() method.
 
 - Now that we have called startGame() within the instance of GameController thats currently running, the function starts the game by setting the following: 
 
   currentGameStatus = GameState.Playing;
   timerCanvas.enabled = true;
   resetGame();
   setTimer();
   
resetGame() simply sets the score to 0. setTimer() sets a 30 second timer that runs within GameController.cs within its Update() method.

Now that we are playing the game, the asteroids can be shot at to generate points. Each asteroid prefab has asteroidHit.cs attached to it; if we shoot an asteroid, the raycast logic from LaserGunScript.cs will call asteroidCollision() in asteroidHit.cs. In this method, we instantiate an explosion animation prefab in place of the asteroid, and calculate the score for the asteroid. Points are calculated based on distance between the player and asteroid, and multipled with a score multiplier that is different based on the type of asteroid that was hit (red, green, or blue). See the asteroidHit.cs script for full details. A popup canvas with the score generated is also instantiated in place of the asteroid, and this popup canvas has PopupControl.cs attached to it.

After the score has been computed, gameController.updateScore(int score) is called to pass in the calculated score from shooting the asteroid and update the total score variable within the GameController class. 

The user shoots as many asteroids as they can within the 30 second period.

- After the timer hits zero (within GameController's Update() method), the game is now over and the gameOver() and enableStartMenu() functions are called within GameController. These functions enable the text objects that tell the user that the game is over and shows them their score. The timer text object is also now disabled. The "Start Game" button is enabled so that the user can shoot this button again to restart the game. 



