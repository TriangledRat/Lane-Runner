using Codice.Client.Common.GameUI;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed=10, jumpSpeed = 5, forwardMovement = 10;
    CharacterController controller;
    PlayerLives lives;
    public int playerPos;
    Vector3 targetPos, storePos;
    float verticalSpeed, gravity = 9.81f, forwardStore, cooldown;
    public bool recovery = false;
    public bool hit;
    public bool winner;
    public int hits;
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
        if (!recovery)
        {
            this.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        }
        Vector3 movingVector = Vector3.zero;
        storePos = transform.position;  
        if (Input.GetKeyDown("left"))
        {
            MoveLane(false);           
        }

        if(Input.GetKeyDown("right"))
        {
            MoveLane(true);           
        }

        switch (playerPos)
        {
            case 0:
                targetPos = new Vector3(storePos.x, storePos.y, 5);
                break;
            case 1:
                targetPos = new Vector3(storePos.x, storePos.y, 0);
                break;
            case 2:
                targetPos = new Vector3(storePos.x, storePos.y, -5);
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
            forwardMovement /= 2;
            recovery = true;           
            hit = false;
        }

        if (recovery)
        {            
            this.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0)
        {
            recovery = false;
            forwardMovement = forwardStore;
        }

        movingVector.x = forwardMovement;
        movingVector.y = verticalSpeed;
        movingVector.z = (targetPos - transform.position).z * speed;
        controller.Move(movingVector * Time.deltaTime);

    }

    public void MoveLane(bool goingRight)
    {        
        if (goingRight && playerPos != 2 && controller.isGrounded)
        {
            playerPos++;
        }
        else if (!goingRight && playerPos != 0 && controller.isGrounded)
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
        if(other.gameObject.tag == "Obstacle" || other.gameObject.tag == "MovingObstacle")
        {
            if (!recovery)
            {                
                lives.lives--;
                hit = true;
                cooldown = 3;
            }
        }

        if (other.gameObject.tag == "Finish")
        {
            winner = true;
            Debug.Log("Win!");
        }
    }
}
