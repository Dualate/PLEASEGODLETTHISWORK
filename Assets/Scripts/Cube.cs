using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class Cube : MonoBehaviour
{
    PlayerControls controls;


    public void PLEASEGOTLETTHISWORK(CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }
}
