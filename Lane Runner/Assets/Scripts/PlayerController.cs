using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed=10;
    CharacterController controller;
    int playerPos;
    Vector3 targetPos, storePos;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = 1;
        targetPos = Vector3.zero;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movingVector = Vector3.zero;
        storePos = transform.position;  
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLane(false);           
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            MoveLane(true);           
        }

        switch (playerPos)
        {
            case 0:
                targetPos = new Vector3(storePos.x, storePos.y, -5);
                break;
            case 1:
                targetPos = new Vector3(storePos.x, storePos.y, 0);
                break;
            case 2:
                targetPos = new Vector3(storePos.x, storePos.y, 5);
                break;
            default:
                targetPos = new Vector3(storePos.x, storePos.y, 0);
                break;
        }  
        movingVector.z = (targetPos - transform.position).z * speed;
        controller.Move(movingVector * Time.deltaTime);

    }

    private void MoveLane(bool goingRight)
    {        
        if (goingRight && playerPos != 2)
        {
            playerPos++;
        }
        else if (!goingRight && playerPos != 0)
        {
            playerPos--;
        }
    }

}
