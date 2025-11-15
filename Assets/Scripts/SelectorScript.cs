using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class SelectorScript : MonoBehaviour
{
    Vector2 moveVector;
    public float moveSpeed;
    float xSpeed;
    float ySpeed;
    void Start()
    {
        
    }

    public void Move(CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        ySpeed = moveVector.y * moveSpeed * Time.deltaTime;
        xSpeed = moveVector.x * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0);
        
    }
}
