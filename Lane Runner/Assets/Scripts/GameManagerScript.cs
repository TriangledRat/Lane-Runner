using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        StartCoroutine(CountingDown());
    }

    IEnumerator CountingDown()
    {
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1f;


    }

}
