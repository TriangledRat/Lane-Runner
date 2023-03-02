using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoinInventory : MonoBehaviour
{
    public int coins;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Coin")
        {
            coins++;
            Destroy(other.gameObject);
        }
    }

}
