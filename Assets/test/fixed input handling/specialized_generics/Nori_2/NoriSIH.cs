using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.InputSystem.InputAction;
public class NoriSIH : MonoBehaviour
{
    Vector3[] positions;
    bool[] specialSignals;
    [SerializeField]
    private GameObject specialAtkBox;

    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 15f;
    private float specialAttackActiveTimer = 0f;
    public float specialAttackActiveTime = .5f;
    private bool activateSpecial = false;
    public float specialKnockback;
    PlayerConfiguration playerConfig;

    Animator animator;
    Slider specialGauge;

    // Start is called before the first frame update
    void Start()
    {
        playerConfig = GetComponent<RangedPlayerInputHandler>().playerConfig;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        specialSignals = GetComponentInChildren<GenericRanged>().specialSignals;
        //positions = GetComponentInChildren<GenericRanged>().positions;
        //specialAtkBox = GameObject.Find("specialBox");
        specialAtkBox.SetActive(false);
        animator = GetComponentInChildren<Animator>();
        specialGaugeTimer = specialGaugeDelay;
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
        specialGauge.value = specialGaugeTimer / specialGaugeDelay;

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
        animator.SetTrigger("special");
        Debug.Log("Firing special");
        specialAtkBox.SetActive(true);
        activateSpecial = true;
        specialGaugeTimerActive = true;
        GetComponentInChildren<GenericRanged>().iFrameActive = true;
        specialGaugeTimer = 0;
    }
    public void ConnectGauge(Slider gauge)
    {
        specialGauge = gauge;
    }
}