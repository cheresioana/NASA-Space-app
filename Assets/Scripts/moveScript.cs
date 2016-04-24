﻿using UnityEngine;
using System.Collections;

public class moveScript : MonoBehaviour {
    public float speed = 8.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 200F;
    public bool in_rover = false;
    Vector3 rotation;
    private Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        if (!in_rover)
        {
            CharacterController controller = GetComponent<CharacterController>();
            if (controller.isGrounded)
            {
                float dir = 0;
                if (Input.GetKey("q"))
                    dir = -1;
                if (Input.GetKey("e"))
                    dir = 1;
                moveDirection = new Vector3(dir, 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                if (Input.GetKey("a") || Input.GetKey("d"))
                    rotation = new Vector3(0, Input.GetAxis("Horizontal") * 5, 0);
                else
                    rotation = new Vector3(0, 0, 0);

                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;

            }
            controller.transform.Rotate(rotation);
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
        }
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Pickup")
        {
            // destroy the capsule at the root (the Package)
            //Destroy(hit.transform.root.gameObject);
            Destroy(hit.transform.parent.gameObject);
            SpawnMgr.RemoveDropPod(hit.gameObject.transform);

            Resource.ResourceType resourceType = SpawnMgr.GetRandomResource();
            int bonus = SpawnMgr.GetRandomAmount();

            // get bonus points
            ResourceManager.AddToResource(resourceType, bonus);
        }
    }
}
