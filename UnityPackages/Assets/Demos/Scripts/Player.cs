using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MovementSystem;

public class Player : MonoBehaviour
{
    private PlayerInput input;
    private MSEntity movementEntity;
    private Vector3 moveDirection;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movementEntity = GetComponent<MSEntity>();
    }

    void Update()
    {
        movementEntity.Move(moveDirection);
    }

    #region InputManagement

    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        moveDirection = new Vector3(v.x, 0, v.y);
    }

    private void OnSprint(InputValue value)
    {
        movementEntity.Sprinting = value.Get<float>() != 0.0f ? true : false;
    }

    private void OnJump(InputValue value)
    {
        movementEntity.Jump();
    }

    #endregion
}
