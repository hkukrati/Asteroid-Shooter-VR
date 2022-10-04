// Script that's tied to the Start Game Button within the Start Menu canvas

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UserInterface : MonoBehaviour
{
    public UnityEvent onShotByGun;

    // countDownCanvas holds the "3, 2, 1" animation countdown
    [SerializeField] private Canvas countDownCanvas;
    
/* 
 * shotByGun() is called in the Laser Gun script upon a successful Raycast hit event with the "Start Game" button
 * A UnityEvent (onShotByGun) is tied to this function. From configuration in the Inspector, invoking this UnityEvent performs the following actions:
 * - Hides the Start Menu canvas
 * - Hides the Game Over and Score text 
 * - Calls startGame() from the GameController to begin the game loop 
 * The Canvas prefab that holds the "3, 2, 1" animation is also instantiated in the same position as the Start Menu canvas 
 */ 
    public void shotByGun()
    {
        onShotByGun.Invoke();
        Canvas newCanvas = Instantiate(countDownCanvas, new Vector3(2766, 560, 3405), Quaternion.identity);
    }
}
