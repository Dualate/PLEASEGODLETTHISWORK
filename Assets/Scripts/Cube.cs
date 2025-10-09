using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;
public class Cube : MonoBehaviour
{
    PlayerControls controls;
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    float xSpeed;
    float ySpeed;
    Vector2 moveVector;

    private GameObject attackBox;
    private float atkTimer = 0f;
    private bool atkTimerActive = false;
    public float knockback;
    public float damagePercent;
    private float atkDelayTime = .5f;

    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox
    }

    public void PLEASEGOTLETTHISWORK(CallbackContext context)
    {

        moveVector = context.ReadValue<Vector2>();

    }

    void Update()
    {
        xSpeed = moveVector.x * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0);

        if (atkTimerActive == true) //This section deactivates the attackbox after a timer
        {
            atkTimer += Time.deltaTime;
            //Debug.Log(atkTimer);
            if (atkTimer >= atkDelayTime)
            {
                attackBox.SetActive(false);
                atkTimerActive = false;
                atkTimer = 0f;
            }
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            grounded = false;
        }

    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }   
    }

    public void Attack()
    {
        if (atkTimerActive)
        {
            return;
        }
        attackBox.SetActive(true);
        atkTimerActive = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("attack"))
        {
            damagePercent += .1f;
            Debug.Log("Hit");
            gameObject.GetComponent<Rigidbody>().AddForce(damagePercent * knockback * Vector3.right, ForceMode.Impulse);
        }
    }
}
