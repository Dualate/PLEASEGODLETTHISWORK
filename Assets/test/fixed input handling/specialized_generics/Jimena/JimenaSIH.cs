using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class JimenaSIH : MonoBehaviour
{
    private GameObject specialAtkBox;
    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 30f;
    private float specialAttackActiveTimer = 0f;
    public float specialAttackActiveTime = .5f;

    private bool activateSpecial = false;

    bool[] specialSignals;
    public Vector3[] positions;

    //variables for special grab movement
    bool grabbing;
    Vector3 newTargetPos;
    Rigidbody rb; 

    PlayerConfiguration playerConfig;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        playerConfig = GetComponent<RangedPlayerInputHandler>().playerConfig;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        specialSignals = GetComponentInChildren<GenericRanged>().specialSignals;
        //positions = GetComponentInChildren<GenericRanged>().positions;
        specialAtkBox = GameObject.Find("specialAtkBox");
        specialAtkBox.SetActive(false);
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == "Special")
        {
            Debug.Log("Special input");
            SpecialAttack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (specialSignals[0])
        {
            specialAtkBox.transform.rotation = Quaternion.Euler(0,180,0);
            specialAtkBox.transform.localPosition = positions[1];
            //specialSignals[1] = false;
        }
        if (specialSignals[1])
        {
            specialAtkBox.transform.rotation = Quaternion.Euler(0,0,0);
            specialAtkBox.transform.localPosition = positions[0];
            //specialSignals[0] = false;
        }

        if (specialGaugeTimerActive == true)
        {
            specialGaugeTimer += Time.deltaTime;
            if (specialGaugeTimer >= specialGaugeDelay)
            {
                specialGaugeTimerActive = false;
                specialGaugeTimer = 0f;
            }
        }
        if (activateSpecial)
        {
            specialAttackActiveTimer += Time.deltaTime;
            if (specialAttackActiveTimer >= specialAttackActiveTime)
            {
                specialAtkBox.SetActive(false);
                activateSpecial = false;
                specialAttackActiveTimer = 0f;
            }
        }
        if(grabbing)
        {
            float step = GetComponentInChildren<GenericRanged>().moveSpeed * 2 * Time.deltaTime;
            rb.isKinematic = true;
            rb.detectCollisions = false;
            transform.position = Vector3.MoveTowards(transform.position, newTargetPos, step);
        }
    }

    void LateUpdate()
    {
        if(Vector3.Distance(transform.position, newTargetPos) < .001f)
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
            grabbing = false;
        }
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

    public void GetTargetPosition(Vector3 targetPosition)
    {
        if(grabbing)
        {
            return;
        }
        newTargetPos = Vector3.Lerp(transform.position, targetPosition + Vector3.up, .75f);
        //newTargetPos = targetPosition + Vector3.up *2;
        grabbing = true;
    }
}
