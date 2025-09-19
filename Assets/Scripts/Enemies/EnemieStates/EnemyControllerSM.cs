using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class EnemyControllerSM : MonoBehaviour
{
    public Transform enemyTransform;
    public CharacterController enemyController;

    [Header("Moving Config")]
    public Transform[] wayPoints;
    public float speed = 10f;
    public float minDistance = 1f;

    [Header("Jumping Config")]
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;

    [Header("HUD Config")]
    public TextMeshProUGUI showState;

    private int _currentWaypoint = 0;
    private float _verticalSpeed = 0f;

    private bool _isMoving = false;
    private bool _isJumping = false;


    public void Update()
    {
        if (_isJumping)
        {
            HandleJump();
        }else if(_isMoving )
        {
            HandleMove();
        }
        else
        {
            HandleIdle();
        }
    }

    public void HandleIdle()
    {
        //vazio pois acontece nada.
    }

    public void HandleMove()
    {
        if (Vector3.Distance(transform.position, wayPoints[_currentWaypoint].position) < minDistance)
        {
            _currentWaypoint++;
            if (_currentWaypoint >= wayPoints.Length)
                _currentWaypoint = 0;
        }

        Vector3 targetPos = wayPoints[_currentWaypoint].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        transform.LookAt(targetPos);
    }

    public void HandleJump()
    {
        Vector3 move = transform.forward * speed;

        if (_verticalSpeed == 0f)
            _verticalSpeed = jumpSpeed;

        _verticalSpeed -= gravity * Time.deltaTime;
        move.y = _verticalSpeed;

        enemyController.Move(move * Time.deltaTime);

        // Exemplo: quando cair no chão, volta para Idle
        if (enemyController.isGrounded && _verticalSpeed < 0)
        {
            _verticalSpeed = 0;
            EnemieStateMachineManager.Instance.NextState();
        }
    }


    public void Idle()
    {
        showState.text = "State - Idle";
    }

    public void Jumping()
    {
        showState.text = "State - Pulando";
        _isJumping = true;
    }

    public void StopJump()
    {
        _isJumping = false;
    }

    public void Moving()
    {
        showState.text = "State - Movendo";
        _isMoving = true;
    }

    public void StopMoving()
    {
        _isMoving = false;
    }
}
