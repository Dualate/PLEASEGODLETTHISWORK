using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class Cube : MonoBehaviour
{
    PlayerControls controls;
    public float moveSpeed;
    float xSpeed;
    float ySpeed;
    Vector2 moveVector; 
    public void PLEASEGOTLETTHISWORK(CallbackContext context)
    {

        moveVector = context.ReadValue<Vector2>();
        
    }

    void Update(){
        xSpeed = moveVector.x * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0);
    }
}
