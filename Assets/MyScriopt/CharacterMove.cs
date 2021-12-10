using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyScript
{

    public class CharacterMove : MonoBehaviour
    {
        public float speed = 2.0f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;

        private CharacterController controller;

        void Start()
        {
            controller = GetComponent<CharacterController>();
        }

        void Update()
        {
            float x = Input.GetAxis("Horizontal");
            float y = Input.GetAxis("Vertical");
            if (x != 0.0f || y != 0.0f)
            {
                Move(x,y);
	        }
        }

        public void Move(float x,float y)
        {
            Vector3 moveDirection = new Vector3(x, 0.0f, y);
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection = moveDirection * speed;
            if (controller.isGrounded)
            {
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                }
            }

            moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);
        }

        public void MoveCoordinate(Vector3 coordinate)
        {
            Vector3 moveDirection = Vector3.MoveTowards(transform.position, coordinate, Time.deltaTime * speed);
            //moveDirection = transform.InverseTransformPoint(moveDirection);
            transform.position = moveDirection;
        }
    }
}