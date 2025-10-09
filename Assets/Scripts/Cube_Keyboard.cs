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
    private GameObject attackBox;
    private float atkTimer = 0f;
    private float atkDelayTime = .5f;
    private bool atkTimerActive = false;
    public float knockback;
    public float damagePercent;
    public Vector3[] positions;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<CameraBehavior>().Add(transform);
        attackBox = GameObject.Find("attackBox"); //find attackBox
        attackBox.SetActive(false); //deactivate attackbox
    }

    void Update()
    {
        if (hInput > 0)
        {
            attackBox.transform.localPosition = positions[0];
        }
        else if (hInput < 0)
        {
            attackBox.transform.localPosition = positions[1];
        }
        hInput = Input.GetAxis("Horizontal");
        xSpeed = hInput * moveSpeed * Time.deltaTime;
        transform.Translate(xSpeed, ySpeed, 0);
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.Space) && atkTimerActive == false)
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
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("attack"))
        {
            damagePercent += .1f;
            Debug.Log("Hit");
            gameObject.GetComponent<Rigidbody>().AddForce(damagePercent* knockback * Vector3.right, ForceMode.Impulse);
        }
    }
    public void Attack()
    {
        attackBox.SetActive(true);
        atkTimerActive = true;
    }
}
