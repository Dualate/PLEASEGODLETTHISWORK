using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;
using TMPro;
public class HeavyRanged : MonoBehaviour
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
    public float maxJumpHeight = 2f;
    public float maxJumpTime = 1.5f;
    float jumpGravity;

    //isGrounded variables
    public float distToGround = .5f;

    private GameObject attackBox;
    private GameObject specialAtkBox;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    public Vector3 projectileOffset = new Vector3(2,0,0);
    private Vector3 setProjectileOffset;
    private float atkTimer = 0f;
    private bool atkTimerActive = false;
    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 30f;
    private float specialAttackActiveTimer = 0f;
    public float specialAttackActiveTime = .5f;
    private bool activateSpecial = false;
    public float knockback; //base knockback taken by character
    public float atkKnockback; //base knockback dealt by attacks
    public float specialKnockback;
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

    private Rigidbody rb;
    void Start()
    {
        //GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox
        specialAtkBox = GameObject.Find("specialAtkBox");
        specialAtkBox.SetActive(false);
        rb = GetComponent<Rigidbody>();
    }

    void Awake()
    {
        SetJumpVariables();
    }

    void SetJumpVariables()
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
            rb.velocity = Vector3.zero;
            damagePercent = 0f;
        }
        if (moveVector.x > 0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[0];
                specialAtkBox.transform.localPosition = positions[0];
                setProjectileOffset = projectileOffset;
            }
            
        }
        else if (moveVector.x < -0.5f && Mathf.Abs(moveVector.y) < 0.5f)
        {
            if (atkTimerActive == false)
            {
                attackBox.transform.localPosition = positions[1];
                specialAtkBox.transform.localPosition = positions[1];
                setProjectileOffset = -projectileOffset;
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
        if(specialGaugeTimerActive == true)
        {
            specialGaugeTimer +=  Time.deltaTime;
            if (specialGaugeTimer >= specialGaugeDelay)
            {
                specialGaugeTimerActive = false;
                specialGaugeTimer = 0f;
            }
        }
        if(activateSpecial)
        {
            specialAttackActiveTimer += Time.deltaTime;
            if(specialAttackActiveTimer >= specialAttackActiveTime)
            {
                specialAtkBox.SetActive(false);
                activateSpecial = false;
                specialAttackActiveTimer = 0f;
            }
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, Vector3.down, out hit, distToGround + .1f))
        {
            if(hit.collider.CompareTag("Ground"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                grounded = true;
                jumpDelay = 0;
                secondJump = true;
                resetPosition = transform.position;
            }
            else if(hit.collider.CompareTag("Player"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                rb.AddForce(initialJumpVelocity/6 * Vector3.up, ForceMode.VelocityChange);
            }
            if(hit.collider.CompareTag("BouncePlatform"))
            {
                rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                grounded = true;
                jumpDelay = 0;
                secondJump = true;
                rb.AddForce(initialJumpVelocity/6 * Vector3.up, ForceMode.VelocityChange);
            }
            
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
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
            rb.AddForce(initialJumpVelocity * Vector3.up, ForceMode.VelocityChange);
            return;
        }
        else if (secondJump && !grounded)
        {
            if (jumpDelay >= maxJumpDelay)
            {
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                rb.AddForce(initialJumpVelocity * Vector3.up, ForceMode.VelocityChange);
                secondJump = false;
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
        FireProjectile();
        attackBox.SetActive(true);
        atkTimerActive = true;
    }
    public void SpecialAttack()
    {
        if (specialGaugeTimerActive)
        {
            return;
        }
        specialAtkBox.SetActive(true);
        activateSpecial = true;
        specialGaugeTimerActive = true;
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("LightProjectile"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HeavyProjectile"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HRanged"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HMelee"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HMSpecial"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("LMSpecial"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("HRSpecial"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
        else if (collider.gameObject.CompareTag("LRSpecial"))
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
            rb.AddForce(damagePercent * knockback * scalar, ForceMode.Impulse);
        }
    }
    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        UpdateMoveVector(moveVector);
    }
    void FireProjectile()
    {
        if (attackBox.transform.localPosition == positions[0])
        {
            setProjectileOffset = projectileOffset;
        }
        else if(attackBox.transform.localPosition == positions[1])
        {
            setProjectileOffset = -projectileOffset;
        }
        GameObject projectile = Instantiate(projectilePrefab, attackBox.transform.position + setProjectileOffset, attackBox.transform.rotation);
        Rigidbody projectileRB = projectile.GetComponent<Rigidbody>();
        if (projectileRB != null)
        {
            if (attackBox.transform.localPosition == positions[0])
            {
                projectileRB.velocity = Vector3.right * projectileSpeed;
            }
            else if(attackBox.transform.localPosition == positions[1])
            {
                projectileRB.velocity = Vector3.left * projectileSpeed;
            }
        }
    }
}
