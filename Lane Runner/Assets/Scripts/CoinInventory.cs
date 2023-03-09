using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CoinInventory : MonoBehaviour
{
    public int coins;
    [SerializeField] GameObject player;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            player.GetComponent<PlayerScore>().score += 10;
            Destroy(other.gameObject);
        }
    }

}
