using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;

public class AliciaSIH : MonoBehaviour
{
    [SerializeField]
    private GameObject specialAtkBox;
    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 30f;
    private float specialAttackActiveTimer = 0f;
    public float specialAttackActiveTime = .5f;
    private bool activateSpecial = false;
    PlayerConfiguration playerConfig;
    bool[] specialSignals;
    [SerializeField]
    Vector3[] positions;
    Animator animator;
    Slider specialGauge;

    // Start is called before the first frame update
    void Start()
    {
        playerConfig = GetComponent<NewPlayerInputHandler>().playerConfig;
        
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        specialSignals = GetComponentInChildren<GenericMelee>().specialSignals;
        //positions = GetComponentInChildren<GenericMelee>().positions;
        //specialAtkBox = GameObject.Find("specialBox");
        specialAtkBox.transform.localPosition = positions[1];
        specialAtkBox.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        specialGaugeTimer = specialGaugeDelay;

    }

    private void Input_onActionTriggered(CallbackContext context)
    {
        if (context.action.name == "Special")
        {
            SpecialAttack();
        }
    }

    // Update is called once per frame
    void Update()
    {
        specialGauge.value = specialGaugeTimer / specialGaugeDelay;
        if (specialSignals[0])
        {
            specialAtkBox.transform.localPosition = positions[0];
        }
        if (specialSignals[1])
        {
            specialAtkBox.transform.localPosition = positions[1];
        }

    }



    private void LateUpdate()
    {
        if (specialGaugeTimerActive == true)
        {
            specialGaugeTimer += Time.deltaTime;
            if (specialGaugeTimer >= specialGaugeDelay)
            {
                specialGaugeTimerActive = false;

            }
        }

        if (activateSpecial)
        {
            specialAttackActiveTimer += Time.deltaTime;
            if (specialAttackActiveTimer >= specialAttackActiveTime)
            {
                specialAtkBox.SetActive(false);
                specialAttackActiveTimer = 0f;
                activateSpecial = false;
                //specialGaugeTimerActive = false;
            }
        }
    }

    public void SpecialAttack()
    {
        if (specialGaugeTimerActive)
        {
            return;
        }
        GetComponent<GameHandler>().PlayVoiceLine(1);

        animator.SetTrigger("special");
        specialAtkBox.SetActive(true);
        activateSpecial = true;
        specialGaugeTimerActive = true;
        specialGaugeTimer = 0;
    }

    public void ConnectGauge(Slider gauge)
    {
        specialGauge = gauge;
    }
}
