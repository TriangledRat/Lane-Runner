using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text scoreText, distanceText, livesText;
    [SerializeField] GameObject player;
    int playerscore, distance, playerlives;
    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "Score: " + 0;
        distanceText.text = "Distance Run: " + 0;
        livesText.text = "Lives: " + 3;
        player = GameObject.Find("Player");
       
    }

    // Update is called once per frame
    void Update()
    {
        distance = player.GetComponent<PlayerScore>().distanceRun;
        playerscore = player.GetComponent<PlayerScore>().score;
        playerlives = player.GetComponent<PlayerLives>().lives;
        scoreText.text = "Score: " + playerscore;
        distanceText.text = "Distance Run: " + distance + " meters";
        livesText.text = "Lives: " + playerlives;
    }
}
