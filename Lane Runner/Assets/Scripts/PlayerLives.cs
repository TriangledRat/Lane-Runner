using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    PlayerController playerController;
    int lives;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(lives);
        if (playerController.hit)
        {
            lives--;
        }        
    }
}
