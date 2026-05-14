using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floor : MonoBehaviour
{
    // Start is called before the first frame update
    public InitializeLevel SI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SI.Respawn(collider.gameObject.transform);
        }
    }
}
