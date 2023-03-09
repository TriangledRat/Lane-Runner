using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score, distanceRun;
    public bool coinHit;
    private int coinTotal;
    GameObject player;

    private void Start()
    {
        player = this.gameObject;
    }
    void Update()
    {
        coinTotal = player.GetComponent<CoinInventory>().coins;
        score = (int) transform.position.x + (coinTotal * 10);
        distanceRun = (int) transform.position.x / 3;


    }
}
