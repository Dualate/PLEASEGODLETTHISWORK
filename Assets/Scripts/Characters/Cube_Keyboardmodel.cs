using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Cube_Keyboard_Model : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    float xSpeed;
    float ySpeed;
    public float hInput;
    public float yInput;
    private GameObject attackBox;
    private float atkTimer = 0f;
    private float atkDelayTime = .5f;
    private bool atkTimerActive = false;
    public float knockback; //Knockback level of character
    public float atkKnockback; //Knockback level of attacks
    public float damagePercent;
    public Vector3[] positions;
    public Vector3 resetPosition;
    public bool secondJump = false;
    int jumpDelay = 3;
    bool jumping = false;
    // Start is called before the first frame update
    void Start()
    {
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox
    }

    void Update()
    {
        if (hInput > 0 && yInput == 0) //moves attack right
        {
            attackBox.transform.localPosition = positions[0];
        }
        else if (hInput < 0 && yInput == 0) //moves attack left
        {
            attackBox.transform.localPosition = positions[1];
        }
        if (jumping)
        {
            jumpDelay--;
        }
        
        if (transform.position.y < -15)
        {
            transform.position = resetPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            damagePercent = 0f;
        }
        hInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");
        xSpeed = hInput * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0, Space.World);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            jumping = true;
        }
        if (Input.GetMouseButtonDown(0) && atkTimerActive == false)
        {
            Attack();
        }
        if (atkTimerActive == true) //This section deactivates the attackbox after a timer
        {
            atkTimer += Time.deltaTime;
            //Debug.Log(atkTimer);
            if (atkTimer >= atkDelayTime)
            {
                attackBox.SetActive(false);
                atkTimerActive = false;
                atkTimer = 0f;
                attackBox.transform.localPosition = positions[0]; //reset position of attacks
            }
        }
    }

    public void Jump()
    {
        Debug.Log("Jump");
        if (grounded)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            grounded = false;
            return;
        }
        else if (secondJump && !grounded)
        {
            if (jumpDelay > 0)
            {
                jumpDelay--;
            }
            if (jumpDelay <= 0)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                float cancelForce;
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                    //cancelForce = -0.5f * rb.mass * rb.velocity.y;
                }
                else
                {
                    cancelForce = 0;
                }
                gameObject.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
                secondJump = false;
            }

        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
        {
            grounded = false;
            secondJump = true;
        }
    }
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("attack"))
        {
            Vector3 scalar = Vector3.zero;
            if (collider.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            gameObject.GetComponent<Rigidbody>().AddForce(damagePercent* knockback * scalar, ForceMode.Impulse);
        }
    }
    public void Attack()
    {
        if (hInput == 0 && yInput > 0) //up
        {
            attackBox.transform.localPosition = positions[2];
        }
        else if (hInput == 0 && yInput < 0) //down
        {
            attackBox.transform.localPosition = positions[3];
        }
        else if (hInput > 0 && yInput > 0) //top right
        {
            attackBox.transform.localPosition = positions[4];
        }
        else if (hInput < 0 && yInput > 0) //top left
        {
            attackBox.transform.localPosition = positions[5];
        }
        else if (hInput < 0 && yInput < 0) //bottom left
        {
            attackBox.transform.localPosition = positions[6];
        }
        else if (hInput > 0 && yInput < 0) //bottom right
        {
            attackBox.transform.localPosition = positions[7];
        }
        attackBox.SetActive(true);
        atkTimerActive = true;
    }
}
