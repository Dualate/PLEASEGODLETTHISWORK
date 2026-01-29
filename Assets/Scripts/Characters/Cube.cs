using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;
using TMPro;
public class Cube : MonoBehaviour
{



    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    float xSpeed;
    float ySpeed;
    Vector2 moveVector;

    //jump variables
    float initialJumpVelocity;
    float doubleJumpVelocity;
    float maxJumpHeight = 3.5f;
    float maxJumpTime = 1.5f;
    float jumpGravity;

    //isGrounded variables
    public float distToGround = .5f;

    private GameObject attackBox;
    private float atkTimer = 0f;
    private bool atkTimerActive = false;
    public float knockback; //base knockback taken by character
    public float atkKnockback; //base knockback dealt by attacks
    public float damagePercent;
    private float atkDelayTime = .5f;
    public Vector3[] positions;
    public Vector3 resetPosition;
    public bool secondJump = false;
    public float maxJumpDelay = 1f;
    float jumpDelay = 0;

    bool ready;

    public ParticleSystem landingEffectPrefab;
    public ParticleSystem hitEffectPrefab;
    void Start()
    {
        //GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox
    }

    void Awake()
    {
        setJumpVariables();
    }

    void setJumpVariables()
    {
        float timeToApex = maxJumpTime/2;
        jumpGravity = (-2* maxJumpHeight)/Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2*maxJumpHeight)/timeToApex;
        doubleJumpVelocity = initialJumpVelocity/2;
    }

    public void UpdateMoveVector(Vector2 moveVector)
    {

        this.moveVector = moveVector;

    }


    void Update()
    {
        GroundCheck();

        if (transform.position.y < -15)
        {
            transform.position = resetPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            damagePercent = 0f;
        }
        if (moveVector.x > 0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[0];
            }
            
        }
        else if (moveVector.x < -0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[1];
            }
        }
        xSpeed = moveVector.x * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0, Space.World);

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

    void GroundCheck()
    {
        if(Physics.Raycast(transform.position, Vector3.down, distToGround + .1f))
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            grounded = true;
            jumpDelay = 0;
            secondJump = true;
            resetPosition = transform.position;
        }
        else
        {
            grounded = false;
            if (jumpDelay < maxJumpDelay && secondJump)
            {
                jumpDelay += Time.deltaTime;
            }
        }
    }

    public void Jump()
    {   
        if (grounded)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            gameObject.GetComponent<Rigidbody>().AddForce(initialJumpVelocity * Vector3.up, ForceMode.VelocityChange);
            return;
        }
        else if (secondJump && !grounded)
        {
            // if (jumpDelay > 0)
            // {
            //     jumpDelay--;
            // }
            if (jumpDelay >= maxJumpDelay)
            {
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                gameObject.GetComponent<Rigidbody>().AddForce(initialJumpVelocity * Vector3.up, ForceMode.VelocityChange);
                secondJump = false;
                Debug.Log("double jump");
            }
        }
    }

    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            if (collider.transform.position.y < transform.position.y - 3.5)
            {
                ParticleSystem landingInstance = Instantiate(landingEffectPrefab, collider.contacts[0].point, Quaternion.identity);
                landingInstance.Play();
                Destroy(landingInstance.gameObject, landingEffectPrefab.main.duration);
            }
            // Rigidbody rb = GetComponent<Rigidbody>();
            // rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            // grounded = true;
            // secondJump = true;
            // resetPosition = transform.position;
        }   
    }


    public void Attack()
    {
        if (atkTimerActive)
        {
            return;
        }
        if (Mathf.Abs(moveVector.x) < 0.35f && moveVector.y > 0.5f) //up
        {
            attackBox.transform.localPosition = positions[2];
        }
        else if (Mathf.Abs(moveVector.x) < 0.35f && moveVector.y < -0.5f) //down
        {
            attackBox.transform.localPosition = positions[3];
        }
        else if (moveVector.x > 0.5f && moveVector.y > 0.5f) //top right
        {
            attackBox.transform.localPosition = positions[4];
        }
        else if (moveVector.x < -0.5f && moveVector.y > 0.5f) //top left
        {
            attackBox.transform.localPosition = positions[5];
        }
        else if (moveVector.x < -0.5f && moveVector.y < -0.5f) //bottom left
        {
            attackBox.transform.localPosition = positions[6];
        }
        else if (moveVector.x > 0.5f && moveVector.y < -0.5f) //bottom right
        {
            attackBox.transform.localPosition = positions[7];
        }
        attackBox.SetActive(true);
        atkTimerActive = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("attack"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

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
            gameObject.GetComponent<Rigidbody>().AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
    }
    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        UpdateMoveVector(moveVector);
    }
}
