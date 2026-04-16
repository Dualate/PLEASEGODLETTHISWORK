using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AliciaSIH : MonoBehaviour
{
    private GameObject specialAtkBox;
    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 30f;
    private float specialAttackActiveTimer = 0f;
    private bool activateSpecial = false;

    bool[] specialSignals = GetComponent<GenericMelee>();
    // Start is called before the first frame update
    void Start()
    {
        specialAtkBox = GameObject.Find("specialAtkBox");
        specialAtkBox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (specialSignals[0])
            Debug.Log("Firing");//specialAtkBox.transform.localPosition = positions[0];

    }
}
