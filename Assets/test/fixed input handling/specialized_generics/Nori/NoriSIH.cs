using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public class NoriSIH : MonoBehaviour
{
    Vector3[] positions;
    bool[] specialSignals;
    private GameObject specialAtkBox;

    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 15f;
    private float specialAttackActiveTimer = 0f;
    public float specialAttackActiveTime = .5f;
    private bool activateSpecial = false;
    public float specialKnockback;
    PlayerConfiguration playerConfig;

    // Start is called before the first frame update
    void Start()
    {
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
            specialSignals[1] = false;
            //specialAtkBox.transform.localPosition = positions[0];
        }
        if (specialSignals[1])
        {
            specialSignals[0] = false;
            //specialAtkBox.transform.localPosition = positions[1];
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
    }

    public void SpecialAttack()
    {
        if (specialGaugeTimerActive)
        {
            return;
        }
        Debug.Log("Firing special");
        specialAtkBox.SetActive(true);
        activateSpecial = true;
        specialGaugeTimerActive = true;
        GetComponentInChildren<GenericRanged>().iFrameActive = true;
    }

}
