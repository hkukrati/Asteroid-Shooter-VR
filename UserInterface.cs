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
    
    // Start is called before the first frame update
    public void shotByGun()
    {
        onShotByGun.Invoke();
        Canvas newCanvas = Instantiate(countDownCanvas, new Vector3(2766, 560, 3405), Quaternion.identity);
    }
}
