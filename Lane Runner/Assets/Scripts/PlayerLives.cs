using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    PlayerController playerController;
    public int lives;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.hit)
        {
            lives--;
        }        

        if(lives == 0)
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
