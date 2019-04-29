﻿using System;
using UnityEngine;
using UnityEngine.Experimental.Input;

/* 
 * Developed by Adam Brodin
 * https://github.com/AdamBrodin
 */

public class Player : EntityBase, InputController.IPlayerActions
{
    #region Variables
    public static event Action<bool> OnGetShooting;
    public static event Action<Vector2> OnGetMovement;

    private InputController playerInput;
    #endregion

    private void Awake()
    {
        playerInput = new InputController();
        playerInput.Player.SetCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnGetMovement?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShooting(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnGetShooting?.Invoke(true);
        }
        else if (context.cancelled)
        {
            OnGetShooting?.Invoke(false);
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
