using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    Vector3[] positions;
    // Start is called before the first frame update
    void Start()
    {
        specialSignals = GetComponentInChildren<GenericRanged>().specialSignals;
        //positions = GetComponentInChildren<GenericRanged>().positions;
        specialAtkBox = GameObject.Find("specialAtkBox");
        specialAtkBox.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (specialSignals[0])
        {
            specialAtkBox.transform.localPosition = positions[0];
            specialSignals[1] = false;
        }
        if (specialSignals[1])
        {
            specialAtkBox.transform.localPosition = positions[1];
            specialSignals[0] = false;
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
        specialAtkBox.SetActive(true);
        activateSpecial = true;
        specialGaugeTimerActive = true;
    }
}
