using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class AnabethSIH : MonoBehaviour
{
    [SerializeField]
    private GameObject specialAtkBox;
    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 15f;
    private float specialAttackActiveTimer = 0f;
    private float specialAttackActiveTime = 0.5f;
    private bool activateSpecial = false;
    private float counterTimer = 0f;
    public float counterTimerTotal = 3f;
    public bool counterActive = false;
    public float damagePercent;
    public float knockback;
    PlayerConfiguration playerConfig;

    Rigidbody rb;

    public ParticleSystem hitEffectPrefab;
    [SerializeField]
    Vector3[] positions;
    bool[] specialSignals;

    Slider specialGauge;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerConfig = GetComponent<NewPlayerInputHandler>().playerConfig;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        //specialAtkBox = GameObject.Find("specialAtkBox");
        specialAtkBox.SetActive(false);
        specialSignals = GetComponentInChildren<GenericMelee>().specialSignals;
        //positions = GetComponentInChildren<GenericMelee>().positions;
        GetComponentInChildren<GenericMelee>().isThisAnabeth = true;
        animator=GetComponentInChildren<Animator>();
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
        UpdateValues();
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
                activateSpecial = false;
                specialAttackActiveTimer = 0f;
            }
        }
        if(counterActive)
        {
            counterTimer += Time.deltaTime;
            if(counterTimer >= counterTimerTotal)
            {
                counterTimer = 0;
                counterActive = false;
            }
        }
    }
    public void SpecialAttack()
    {
        if (specialGaugeTimerActive || counterActive)
        {
            return;
        }
        
        counterActive = true;
        specialGaugeTimerActive = true;
        specialGaugeTimer = 0f;

    }

    public void UpdateValues()
    {
        damagePercent = GetComponentInChildren<GenericMelee>().damagePercent;
        knockback = GetComponentInChildren<GenericMelee>().knockback;
    }

    public void ActivateCounter(int position)
    {
        animator.SetTrigger("special");
        if(position == 0)
        {
            specialAtkBox.transform.localPosition = positions[0];
            Debug.Log("Counter Activate Left");
        }
        else if(position == 1)
        {
            specialAtkBox.transform.localPosition = positions[1];
            Debug.Log("Counter Activate Right");
        }
        specialAtkBox.SetActive(true);
        activateSpecial = true;
        GetComponentInChildren<GenericMelee>().iFrameActive = true;
        counterActive = false;
        counterTimer = 0;
        Debug.Log("Special is active");
    }

    public void CounterHit()
    {
        specialAtkBox.SetActive(false);
        counterActive = false;
        counterTimer = 0;
        activateSpecial = false;
        specialAttackActiveTimer = 0;
    }

    public void ConnectGauge(Slider gauge)
    {
        specialGauge = gauge;
    }
}
