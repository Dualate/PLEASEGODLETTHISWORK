using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
public class AnabethSIH : MonoBehaviour
{
    private GameObject specialAtkBox;
    private float specialGaugeTimer = 0f;
    private bool specialGaugeTimerActive = false;
    public float specialGaugeDelay = 15f;
    private float specialAttackActiveTimer = 0f;
    private float specialAttackActiveTime;
    private bool activateSpecial = false;
    private float counterTimer = 0f;
    public float counterTimerTotal = 3f;
    private bool counterActive = false;
    public float damagePercent;
    public float knockback;
    PlayerConfiguration playerConfig;

    Rigidbody rb;

    public ParticleSystem hitEffectPrefab;
    Vector3[] positions;
    bool[] specialSignals;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerConfig = GetComponent<NewPlayerInputHandler>().playerConfig;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        specialAtkBox = GameObject.Find("specialAtkBox");
        specialAtkBox.SetActive(false);
        specialSignals = GetComponentInChildren<GenericMelee>().specialSignals;
        positions = GetComponentInChildren<GenericMelee>().positions;
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
        UpdateValues();
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
        if (specialGaugeTimerActive || counterActive)
        {
            return;
        }
        counterActive = true;
        specialGaugeTimerActive = true;
    }

    public void UpdateValues()
    {
        damagePercent = GetComponentInChildren<GenericMelee>().damagePercent;
        knockback = GetComponentInChildren<GenericMelee>().knockback;
    }


    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("attack") && !counterActive)
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
        else if (collider.gameObject.CompareTag("LightProjectile") && !counterActive)
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
        else if (collider.gameObject.CompareTag("HeavyProjectile") && !counterActive)
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
        else if (collider.gameObject.CompareTag("HRanged") && !counterActive)
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
        else if (collider.gameObject.CompareTag("HMelee") && !counterActive)
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
        else if (collider.gameObject.CompareTag("HMSpecial") && !counterActive)
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
        else if (collider.gameObject.CompareTag("LMSpecial") && !counterActive)
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
        else if (collider.gameObject.CompareTag("HRSpecial") && !counterActive)
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
        else if (collider.gameObject.CompareTag("LRSpecial") && !counterActive)
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
        else if (collider.gameObject.CompareTag("attack") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("LightProjectile") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("HeavyProjectile") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("HRanged") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("HMelee") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("HMSpecial") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("HRSpecial") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("LMSpecial") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
        else if (collider.gameObject.CompareTag("LRSpecial") && counterActive)
        {
            if (collider.transform.position.x < transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[1];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
            if (collider.transform.position.x > transform.position.x)
            {
                specialAtkBox.transform.localPosition = positions[0];
                specialAtkBox.SetActive(true);
                activateSpecial = true;
            }
        }
    }
}
