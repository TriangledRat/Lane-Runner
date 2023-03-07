using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed=10, jumpSpeed = 5, forwardMovement = 10;
    CharacterController controller;
    PlayerLives lives;
    int playerPos;
    Vector3 targetPos, storePos;
    float verticalSpeed, gravity = 9.81f, forwardStore, cooldown;
    bool recovery = false;
    internal bool hit;
    // Start is called before the first frame update
    void Start()
    {
        playerPos = 1;
        targetPos = Vector3.zero;
        controller = GetComponent<CharacterController>();
        lives = GetComponent<PlayerLives>();    
        forwardStore = forwardMovement;
        cooldown = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movingVector = Vector3.zero;
        storePos = transform.position;  
        if (Input.GetKeyDown("left") && controller.isGrounded)
        {
            MoveLane(false);           
        }

        if(Input.GetKeyDown("right") && controller.isGrounded)
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
            if (Input.GetKeyDown("up"))
            {
                Jump(true);
            }
        }
        else
        {
            verticalSpeed -= (gravity * Time.deltaTime);
        }

        if (hit)
        {
            lives.lives--;
            forwardMovement /= 2;
            recovery = true;
            hit = false;
        }

        if (recovery)
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            recovery = false;
            forwardMovement = forwardStore;
        }

        movingVector.x = -forwardMovement;
        movingVector.y = verticalSpeed;
        movingVector.z = (targetPos - transform.position).z * speed;
        controller.Move(movingVector * Time.deltaTime);

    }

    public void MoveLane(bool goingRight)
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

    public void Jump(bool jumping)
    {
        verticalSpeed = jumpSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle" && !recovery)
        {
            hit = true;
            cooldown = 3;
        }
    }

}
