using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText, distanceText, livesText, countDownText;
    [SerializeField] GameObject player;
    [SerializeField] GameObject manager;
    int playerscore, distance, playerlives;
    float countdown;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        distanceText.text = "Distance Run: " + 0;
        livesText.text = "Lives: " + 3;
        countDownText.text = "";
        player = GameObject.Find("Player");
        countdown = 3;
       
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeScale == 1f)
        {
            countDownText.text = "";
            distance = player.GetComponent<PlayerScore>().distanceRun;
            playerscore = player.GetComponent<PlayerScore>().score;
            playerlives = player.GetComponent<PlayerLives>().lives;
            scoreText.text = "Score: " + playerscore;
            distanceText.text = "Distance Run: " + distance + " meters";
            livesText.text = "Lives: " + playerlives;
        }
        else
        {
            countDownText.text = "Get Ready!";
        }

    }
}
