using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Animations;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GenericMelee : MonoBehaviour
{




    public int playerIndex;
    public float moveSpeed;
    public float jumpForce;
    public bool grounded;
    public float xSpeed;
    public float ySpeed;
    public Vector2 moveVector;

    //jump variables
    public float initialJumpVelocity;
    public float doubleJumpVelocity;
    public float maxJumpHeight = 2f;
    public float maxJumpTime = 1.5f;
    public float jumpGravity;

    //isGrounded variables
    public float distToGround = .5f;

    public GameObject attackBox;
    public float atkTimer = 0f;
    public bool atkTimerActive = false;


    public float knockback; //base knockback taken by character
    public float atkKnockback; //base knockback dealt by attacks
    public float damagePercent;
    public float atkDelayTime = .5f;
    public Vector3[] positions;
    public Vector3 resetPosition;
    public bool secondJump = false;
    public float maxJumpDelay = .25f;
    public float jumpDelay = 0;

    public bool ready;

    public ParticleSystem landingEffectPrefab;
    public ParticleSystem hitEffectPrefab;

    public bool[] specialSignals;

    [SerializeField]
    Rigidbody rb;

    Animator animator;
    SpriteRenderer sprite;
    //Variables for Jimena's special
    bool grabbed = false;
    Vector3 targetPosition;
    public void Awake()
    {
        SetJumpVariables();
        specialSignals = new bool[10];

    }
    void Start()
    {
        try
        {
            animator = GetComponentInChildren<Animator>();
            sprite = GetComponentInChildren<SpriteRenderer>();
        }
        catch (NullReferenceException exception)
        {
            Debug.Log("Failed");
        }
        //GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox
        rb = GetComponent<Rigidbody>();
    }

    public void SetIndex(int index)
    {
        playerIndex = index;
    }

    public void UpdateMoveVector(Vector2 moveVector)
    {
        this.moveVector = moveVector;
    }

    void SetJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        jumpGravity = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
        doubleJumpVelocity = initialJumpVelocity * 2f;
    }

    void Update()
    {
        GroundCheck();
        FootstoolCheck();

        
        if (moveVector.x > 0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            sprite.flipX = false;
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[0];
                specialSignals[1] = true;
                specialSignals[0] = false;
            }

        }
        else if (moveVector.x < -0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            sprite.flipX = true;

            if (atkTimerActive == false)
            {
                specialSignals[0] = true;
                specialSignals[1] = false;
                attackBox.transform.localPosition = positions[1];
            }
        }
        //xSpeed += moveVector.x * moveSpeed * Time.deltaTime;
        xSpeed = moveVector.x * moveSpeed;
        //transform.Translate(xSpeed, ySpeed, 0, Space.World);
        rb.velocity = new Vector3(xSpeed, rb.velocity.y, 0);
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
        if(grabbed)
        {
            float step = moveSpeed * Time.deltaTime;
            rb.isKinematic = true;
            rb.detectCollisions = false;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if(Vector3.Distance(transform.position, targetPosition) < .001f)
            {
                rb.isKinematic = false;
                rb.detectCollisions = true;
                grabbed = false;
            }
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
            animator.SetBool("posY", true);
            attackBox.transform.localPosition = positions[2];
        }
        else if (Mathf.Abs(moveVector.x) < 0.35f && moveVector.y < -0.5f) //down
        {
            animator.SetBool("posY", false);

            attackBox.transform.localPosition = positions[3];
        }
        else if (moveVector.x > 0.5f && moveVector.y > 0.5f) //top right
        {
            animator.SetBool("posY", true);

            attackBox.transform.localPosition = positions[4];
        }
        else if (moveVector.x < -0.5f && moveVector.y > 0.5f) //top left
        {
            animator.SetBool("posY", true);

            attackBox.transform.localPosition = positions[5];
        }
        else if (moveVector.x < -0.5f && moveVector.y < -0.5f) //bottom left
        {
            animator.SetBool("posY", false);

            attackBox.transform.localPosition = positions[6];
        }
        else if (moveVector.x > 0.5f && moveVector.y < -0.5f) //bottom right
        {
            animator.SetBool("posY", false);

            attackBox.transform.localPosition = positions[7];
        }
        animator.SetTrigger("attack");

        attackBox.SetActive(true);
        atkTimerActive = true;
    }


    public void Jump()
    {
        if (grounded)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(initialJumpVelocity * Vector3.up, ForceMode.Impulse);
            return;
        }
        else if (secondJump && !grounded)
        {
            if (jumpDelay >= maxJumpDelay)
            {
                // if (rb.velocity.y < 0)
                // {
                //     rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                // }
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(doubleJumpVelocity * Vector3.up, ForceMode.Impulse);
                secondJump = false;
            }
        }
    }
    private void LateUpdate()
    {
        GroundCheck();

        if (transform.position.y < resetPosition.y - 10)
        {
            transform.position = resetPosition;
            rb.velocity = Vector3.zero;
            damagePercent = 0f;
        }
        if (moveVector.x > 0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[1];
            }

        }
        else if (moveVector.x < -0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[0];
            }
        }
        xSpeed = moveVector.x * moveSpeed;
        //transform.Translate(xSpeed, ySpeed, 0, Space.World);
        rb.velocity = new Vector3(xSpeed, rb.velocity.y, 0);

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + .1f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                grounded = true;
                jumpDelay = 0;
                secondJump = true;
                resetPosition = transform.position;
                animator.SetBool("grounded", true);
            }
            else if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(initialJumpVelocity / 6 * Vector3.up, ForceMode.Impulse);
            }
            if (hit.collider.CompareTag("BouncePlatform"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                grounded = true;
                jumpDelay = 0;
                secondJump = true;
                rb.AddForce(initialJumpVelocity / 3 * Vector3.up, ForceMode.Impulse);
            }

        }
        else
        {
            grounded = false;
            animator.SetBool("grounded", false);

            if (jumpDelay < maxJumpDelay && secondJump)
            {
                jumpDelay += Time.deltaTime;
            }
        }
    }

    public void FootstoolCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + .1f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(initialJumpVelocity / 3 * Vector3.up, ForceMode.VelocityChange);
            }

        }
        if (Physics.Raycast(transform.position, Vector3.up, out hit, distToGround + .1f))
        {
            if (hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(initialJumpVelocity / 6 * Vector3.down, ForceMode.VelocityChange);
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
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("attack"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("LightProjectile"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HeavyProjectile"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HRanged"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HMelee"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HMSpecial"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("LMSpecial"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HRSpecial"))
        {
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);

            Vector3 scalar = Vector3.zero;
            if (collider.transform.parent.gameObject.transform.position.x < transform.position.x)
            {
                scalar = Vector3.right;
            }
            else if (collider.transform.parent.gameObject.transform.position.x > transform.position.x)
            {
                scalar = Vector3.left;
            }
            damagePercent += .1f;
            Debug.Log("Hit");
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("LRSpecial"))
        {
            if(grabbed)
            {
                return;
            }
            ParticleSystem hitInstance = Instantiate(hitEffectPrefab, collider.transform.position, Quaternion.identity);
            hitInstance.Play();
            Destroy(hitInstance.gameObject, hitEffectPrefab.main.duration);
            damagePercent += .1f;
            targetPosition = Vector3.Lerp(transform.position, collider.transform.parent.gameObject.transform.position, .75f);
            //collider.GetComponentInParent<JimenaSIH>().GetTargetPosition(transform.position);
            collider.GetComponentInParent<GenericRanged>().JimenaGrab(transform.position);
            grabbed = true;
        }
    }

    public int GetIndex()
    {
        return playerIndex;
    }
}
