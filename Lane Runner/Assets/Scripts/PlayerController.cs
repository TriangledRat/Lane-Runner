using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed=10, jumpSpeed = 10;
    CharacterController controller;
    int playerPos;
    Vector3 targetPos, storePos;
    float verticalSpeed, gravity = 9.81f;

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
        if (Input.GetKeyDown(KeyCode.A) && controller.isGrounded)
        {
            MoveLane(false);           
        }

        if(Input.GetKeyDown(KeyCode.D) && controller.isGrounded)
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

        if (controller.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                verticalSpeed = jumpSpeed;
            }
        }
        else
        {
            verticalSpeed -= (gravity * Time.deltaTime);
        }


        movingVector.y = verticalSpeed;
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
