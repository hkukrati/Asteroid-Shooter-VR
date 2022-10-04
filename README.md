# Asteroid-Shooter-VR
Asteroid Shooter game for Meta Quest 2. Designed and built with Unity Engine and C#. Scripts developed for the game are included in this repo.

The player has 30 seconds to shoot as many asteroids as possible to achieve the highest score. Points are calculated based on the distance between the player and the asteroid, as well as the asteroid type. Green asteroids move the fastest and generate the highest number of points, followed by red and then blue.

Gameplay Video: https://www.youtube.com/watch?v=l4p8KbXqyVY


<img width="1023" alt="Screen Shot 2022-10-03 at 9 19 23 PM" src="https://user-images.githubusercontent.com/7616530/193733445-bbe13082-a6eb-4102-b13a-b62344dddce2.png">

GameController.cs manages the entire flow of the game. It holds the current game state and includes methods to start and the game, hide/show UI elements, start timers etc. 

Core shooting functionality is implemented in LaserGunScript.cs (attached to the gun GameObjects) through a raycast from the end of the gun barrel in the forward direction. Upon a successful hit event with an asteroid, the score is computed (by invoking asteroidCollision() in AsteroidHit.cs) and the asteroid is destroyed. 

Spawner.cs is attached to the SpawnerZone GameObject and controls the rate of asteroid spawning, the position within the zone where it will spawn, as well as the logic behind choosing which type of asteroid will be instantiated (40% chance for blue, 40% for red and 20% for green)

AsteroidMovement.cs and AsteroidHit.cs are attached to each asteroid prefab. AsteroidMovement.cs controls the speed, translation, and rotation of an asteroid. These settings are configured differently for each type of asteroid through the inspector (e.g. faster speed for green asteroids). AsteroidHit.cs  defines the behavior that occurs after an asteroid is successfully hit- the score is computed based on the distance between player and asteroid, along with the score multiplier. The explosion animation prefab and score popup prefab is instantiated in place of the asteroid, and the asteroid is destroyed.

AsteroidDestroyer.cs is attached to the "asteroid kill zone", which is an invisible GameObject that has a rigidbody and collider. The collider for this game object is set as the trigger, so that when other asteroids come into contact with it, they are immediately destroyed. 

