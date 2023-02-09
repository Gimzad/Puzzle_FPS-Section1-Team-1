using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController controller;

    [SerializeField] int moveSpeed;
    [SerializeField] int jumpMax;
    [SerializeField] float jumpSpeed;
    [SerializeField] int playerGravity;

    [SerializeField] float shootRate;

    int jumpsCurr;
    Vector3 move;
    Vector3 playerVelocity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement();
    }

    void movement()
    {
        if (controller.isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
            jumpsCurr = 0;

        }

        move = (transform.right * Input.GetAxis("Horizontal") +
            (transform.forward * Input.GetAxis("Vertical")));
        controller.Move(move * Time.deltaTime * moveSpeed);

        if (Input.GetButtonDown("Jump") && jumpsCurr <= jumpMax)
        {
            jumpsCurr++;
            playerVelocity.y = jumpSpeed;

        }

        playerVelocity.y -= playerGravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

    }
}
