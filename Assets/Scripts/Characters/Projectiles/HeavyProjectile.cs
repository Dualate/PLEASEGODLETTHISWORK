using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyProjectile : MonoBehaviour
{
    public float lifetime = 3f;

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if(lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
