using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    enum STATE { DORMANT, ACTIVE }
    STATE state = STATE.DORMANT;
    public float moveSpeed;
    public GameObject[] platform;
    int sent = 0;
    // Start is called before the first frame update
    private void Start()
    {
        InvokeRepeating("SpawnPlatform", .15f, .1f);

    }

    // Update is called once per frame
    void SpawnPlatform()
    {
        int index = Random.Range(0, platform.Length);
        float xVariation = 3f * Random.Range(-3, 4);
        Instantiate(platform[index], transform.position + new Vector3(xVariation, 0, 0), platform[index].transform.rotation);
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
