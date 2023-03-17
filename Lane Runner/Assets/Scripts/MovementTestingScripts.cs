using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTestingScripts : MonoBehaviour
{
    public bool laneTesting = false;
    public bool jumpingTesting = true;
    private bool hit;
    int layerMask = 1 << 8;
    PlayerController controller;
    CharacterController cc;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
      
    }
    private void Update()
    {
        Debug.Log(hit);
        Vector3 fwd = transform.TransformDirection(Vector3.right);
        Vector3 rght = transform.TransformDirection(Vector3.back);
        Vector3 lft = transform.TransformDirection(Vector3.forward);
        lft = Quaternion.Euler(0, -45, 0) * transform.right;
        rght = Quaternion.Euler(0, 45, 0) * transform.right;
        Ray forwardRay = new Ray(transform.position + new Vector3(0,-0.5f, 0), fwd);
        Ray rightRay = new Ray(transform.position + new Vector3(0, -0.5f, 0), rght);
        Ray leftRay = new Ray(transform.position + new Vector3(0, -0.5f, 3), lft);

        Debug.DrawRay(rightRay.origin, rightRay.direction*10);
        if (jumpingTesting)
        {
            if (Physics.Raycast(forwardRay, 5, layerMask))
            {
                if (!controller.recovery & cc.isGrounded)
                {
                    controller.Jump(true);
                }

            }
        }
        if (laneTesting)
        {
            if (Physics.Raycast(forwardRay, 3, layerMask))
            {
                hit = true;
                
            }
            else
            {
                hit = false;
            }

            if (hit)
            {
                if (controller.playerPos == 2)
                {
                    controller.MoveLane(false);
                }
                else if(controller.playerPos == 0)
                {
                    controller.MoveLane(true);
                }
                else if (controller.playerPos == 1 && !Physics.Raycast(leftRay, 10, layerMask))
                {
                    controller.MoveLane(false);
                }
                else if(controller.playerPos == 1 && Physics.Raycast(leftRay, 10, layerMask))
                {
                    controller.MoveLane(true);
                }

            }

        }
        
    }
}
