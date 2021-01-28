using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MovementSystem;
using CombatSystem;

public class Player : MonoBehaviour
{
    private PlayerInput input;
    private MSEntity movementEntity;
    private CSWeapon weapon;
    private Vector3 moveDirection;

    void Awake()
    {
        input = GetComponent<PlayerInput>();
        movementEntity = GetComponent<MSEntity>();
        weapon = GetComponentInChildren<CSWeapon>();
    }

    void Update()
    {
        movementEntity.Move(moveDirection, false);
    }

    #region Input Management

    private void OnLightAttack(InputValue value)
    {
        Debug.Log("Initiating light attack.");
        weapon.Combo(0);
    }

    private void OnHeavyAttack(InputValue value)
    {
        Debug.Log("Heavy Attack");
        weapon.Combo(1);
    }

    private void OnMove(InputValue value)
    {
        Vector2 v = value.Get<Vector2>();
        moveDirection = new Vector3(v.x, 0, v.y);
    }

    private void OnSprint(InputValue value)
    {
        movementEntity.Sprinting = value.Get<float>() != 0.0f ? true : false;
    }

    private void OnDash(InputValue value)
    {
        movementEntity.Dash(moveDirection, false);
    }

    private void OnJump(InputValue value)
    {
        movementEntity.Jump();
    }

    #endregion
}
