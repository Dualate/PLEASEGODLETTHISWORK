using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float moveSpeed;
    public GameObject platform;
    int sent = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnPlatform", 0, .1f);
    }

    // Update is called once per frame
    void SpawnPlatform()
    {
        float xVariation = 3f * Random.Range(-3, 4);
        Instantiate(platform, transform.position + new Vector3(xVariation, 0, 0), transform.rotation);
        sent++;
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        if (sent == 50)
        {
            CancelInvoke("SpawnPlatform");
            moveSpeed = 0;
        }
    }
}
