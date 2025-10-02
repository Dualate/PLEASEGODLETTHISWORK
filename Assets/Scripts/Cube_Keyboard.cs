using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_Keyboard : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    float xSpeed;
    float ySpeed;
    public float hInput;
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);

    }

    void Update()
    {
        hInput = Input.GetAxis("Horizontal");
        xSpeed = hInput * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0);
        if (Input.GetKeyDown(KeyCode.Space)){
            Jump();
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
}
