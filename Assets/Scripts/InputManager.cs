using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : MonoBehaviour
{
    private static InputManager instance;

    public static InputManager Instance
    {
        get { return instance; }
    }

    private PlayerInputs playerInputs;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        playerInputs = new PlayerInputs();
    }

    private void OnEnable()
    {
        playerInputs.Enable();
    }
    private void OnDisable()
    {
        playerInputs.Disable();
    }
    
    public Vector2 GetPlayerMovement()
    {
        return playerInputs.Player.Movement.ReadValue<Vector2>();
    }
    public Vector2 GetMousDelta()
    {
        return playerInputs.Player.Look.ReadValue<Vector2>();
    }
    public bool IsPlayerJumped()
    {
        return playerInputs.Player.Jump.triggered;
    }
    public bool IsPlayerFired()
    {
        return playerInputs.Player.Fire.triggered;
    }



}
