using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    public float lifetime;
    private float lifetimeCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity);
        lifetimeCounter += Time.deltaTime;
        if(lifetimeCounter >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
