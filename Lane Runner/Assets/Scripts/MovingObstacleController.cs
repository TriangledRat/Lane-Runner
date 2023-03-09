using Codice.Client.BaseCommands.CheckIn.Progress;
using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleController : MonoBehaviour
{
    Vector3 targetpos, storePos;
    public bool lefthit, righthit;
    int currentLane;
    [SerializeField] float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        storePos.x = transform.position.x;
        targetpos = transform.position;
        DetermineCurrentLane();
        DetermineMoveDirection();
        StartCoroutine(LaneSwitching());
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetpos, step);
        if (currentLane == 2)
        {
            righthit = true;
            lefthit = false;
        }
        else if(currentLane == 0)
        {
            righthit = false;
            lefthit = true;
        }
    }

    void LaneChanger(int targetLane)
    {
        switch (targetLane)
        {
            case 0:
                targetpos = new Vector3(storePos.x, 0.5f, -5);
                currentLane = 0;
                break;
            case 1:
                targetpos = new Vector3(storePos.x, 0.5f, 0);
                currentLane = 1;
                break;
            case 2:
                targetpos = new Vector3(storePos.x, 0.5f, 5);
                currentLane = 2;
                break;
            default:
                targetpos = new Vector3(storePos.x, 0.5f, 0);
                currentLane = 1;
                break;
        }
    }

    void DetermineMoveDirection()
    {
        int randomizer = Random.Range(0, 2);
        if(currentLane == 1)
        {
            switch (randomizer)
            {
                case 0:
                    lefthit = true;
                    break;
                case 1:
                    righthit = true;
                    break;             
                default:
                    lefthit = true;
                    break;
            }
        }       
    }

    void DetermineCurrentLane()
    {
        switch (transform.position.z)
        {
            case -5:
                currentLane = 0;
                break;
            case 0:
                currentLane = 1;
                break;
            case 5:
                currentLane = 2;
                break;

        }
    }

    public IEnumerator LaneSwitching()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (lefthit)
            {
                LaneChanger(currentLane + 1);
                yield return new WaitForSeconds(3);
            }
            else if (righthit)
            {
                LaneChanger(currentLane - 1);
                yield return new WaitForSeconds(3);
            }
        }
       
    }

}
