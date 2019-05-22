using System;
using UnityEngine;
using UnityEngine.InputSystem;
/* 
* Developed by Adam Brodin
* https://github.com/AdamBrodin
*/
public class Player : EntityBase, InputController.IPlayerActions
{
    #region Variables
    public event Action<bool> OnGetShooting;
    public event Action<Vector2> OnGetMovement;

    private InputController playerInput;
    private static Player instance;
    #endregion

    // Singleton
    public static Player Instance
    {
        get
        {
            if (instance == null) { instance = FindObjectOfType<Player>(); }
            return instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();

        // Set this as a receiver for all player inputs
        playerInput = new InputController();
        playerInput.Player.SetCallbacks(this);
    }

    // Whenever any movement input is received
    public void OnMovement(InputAction.CallbackContext context) => OnGetMovement?.Invoke(context.ReadValue<Vector2>());

    // Whenever any shooting input is received
    public void OnShooting(InputAction.CallbackContext context)
    {
        // If the input is currently being pressed down
        if (context.performed)
        {
            OnGetShooting?.Invoke(true);
        }
        // when the input is released 
        else if (context.canceled)
        {
            OnGetShooting?.Invoke(false);
        }
    }

    // Receive input
    private void OnEnable() => playerInput.Enable();

    // Stop receiving input
    private void OnDisable() => playerInput.Disable();
}
