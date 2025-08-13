using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public Animator animator;

    public float speed = 1f;
    public float speedRun = 1.5f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;

    public float jumpSpeed = 15f;

    [Header("KeyCodes")]
    public KeyCode jumpKeyCode = KeyCode.Space;
    public KeyCode runKeyCode = KeyCode.LeftShift;

    private float _vsSpeed = 0f;

    private void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;
        var isWalking = inputAxisVertical != 0;

        if (characterController.isGrounded)
        {
            _vsSpeed = 0;
            if (Input.GetKeyDown(jumpKeyCode))
            {
                _vsSpeed = jumpSpeed;
            }
        }

        _vsSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vsSpeed;

        if(isWalking)
        {
            if(Input.GetKey(runKeyCode))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;

            }
            else
            {
                animator.speed = 1f;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("isRunning", inputAxisVertical != 0);

        /*Mesmo que o acima, mas melhor
        if (inputAxisVertical != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        */

    }
}
