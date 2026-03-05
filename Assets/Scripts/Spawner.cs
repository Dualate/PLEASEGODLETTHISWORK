using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    enum STATE { DORMANT, ACTIVE }
    STATE state = STATE.DORMANT;
    public float moveSpeed;
    public float xMoveSpeed;

    public GameObject[] platform;
    public GameObject finishLine;
    public GameObject arena;
    int sent = 0;
    int chunk = 0;
    float xScal = 1;
    int yScal = 1;
    bool spawning = true;
    int chunks = 0;
    // Start is called before the first frame update
    private void Start()
    {
        SpawnChunk();
        chunks = Random.Range(10, 15);
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
        if (spawning)
        {
            transform.Translate(new Vector3(0, 1, 0) * Time.deltaTime * moveSpeed);
            transform.Translate(new Vector3(xScal, 0, 0) * Time.deltaTime * xMoveSpeed);
            
            if (sent == chunk)
            {
                CancelInvoke("SpawnPlatform");
                if (chunks == 0)
                {
                    spawning = false;
                    Instantiate(finishLine, transform.position + new Vector3(0, 7, 0), Quaternion.identity);
                    EndSpawn();
                }
                else
                {
                    if (Random.Range(1, 4) == 2)
                    {
                        // these lines spawn the fight pause arena
                        //GameObject temp = Instantiate(arena, transform.position + new Vector3(0, 7, 0), Quaternion.identity) as GameObject;
                        //spawning = false;
                        //GameObject.Find("Main Camera").GetComponent<CameraBehavior>().arenaLanded(temp.GetComponent<Transform>());
                    }
                    else
                    {
                        chunks -= 1;
                        sent = 0;
                        SpawnChunk();
                    }

                }

            }

        }

        
    }

    void SpawnChunk()
    {
        int[] chunk_x = { -2, 0, 2 };
        float repeatRate = 0;
        chunk = Random.Range(1, 6);
        xScal = chunk_x[Random.Range(0, chunk_x.Length)];
        Debug.Log(xScal);
        if (xScal < 0)
        {
            moveSpeed = 15;
            repeatRate = .3f;
        }
        else if (xScal == 0)
        {
            moveSpeed = 45;
            repeatRate = .1f;
            
        }
        else if (xScal > 0)
        {
            moveSpeed = 15;
            repeatRate = .3f;
        }

        InvokeRepeating("SpawnPlatform", .15f, repeatRate);
    }

    void EndSpawn()
    {

    }
}
