using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    public int score;
    int distanceRun;
    void Update()
    {
        score = (int) transform.position.x;
        distanceRun = score / 3;
    }
}
