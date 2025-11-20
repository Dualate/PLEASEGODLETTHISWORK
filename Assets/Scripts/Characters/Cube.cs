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
    int jumpDelay = 3;

    bool ready;

    public ParticleSystem landingEffectPrefab;
    public ParticleSystem hitEffectPrefab;

    void Start()
    {
        //GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox

    }



    public void UpdateMoveVector(Vector2 moveVector)
    {

        this.moveVector = moveVector;

    }


    void Update()
    {
        

        if (transform.position.y < -15)
        {
            transform.position = resetPosition;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            damagePercent = 0f;
        }
        if (moveVector.x > 0)
            {
            attackBox.transform.localPosition = positions[0];
        }
        else if (moveVector.x < 0)
        {
            attackBox.transform.localPosition = positions[1];
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

    public void Jump()
    {
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
                if (rb.velocity.y < 0)
                {
                    rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
                }
                gameObject.GetComponent<Rigidbody>().AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
                secondJump = false;
                jumpDelay = 3;
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
            grounded = true;
            secondJump = true;
            resetPosition = transform.position;
        }   
    }


    public void Attack()
    {
        //to add: the rest of the directional inputs
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
}
