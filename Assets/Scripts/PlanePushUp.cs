using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePushUp : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        //Rigidbody rb = collision.transform.parent.GetComponentInChildren<Rigidbody>();
        if(collision.transform.position.y < transform.position.y)
        {
            Physics.IgnoreCollision(collision.transform.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
    }
    void OnTriggerExit(Collider collision)
    {
        Physics.IgnoreCollision(collision.transform.GetComponent<Collider>(), GetComponent<Collider>(), false);
    }
}
