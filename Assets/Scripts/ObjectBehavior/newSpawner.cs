using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class newSpawner : MonoBehaviour
{
    float xDistance = 0;
    float yDistance = 0;

    float minDistance = 10f;
    public float xMove;
    public float yMove;
    public int chunks;

    Vector3 lastPosition;
    [SerializeField]
    GameObject[] platforms;
    [SerializeField]
    GameObject finishLine;
    [SerializeField]
    GameObject arena;
    float[] xSpeeds = {-3f, 1f, -1, 3f };
    float xSpeed;
    // Start is called before the first frame update
    void Start()
    {
        UpdateSpeed();
        //chunks = Random.Range(1, 10);
        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(xMove * xSpeed, yMove) * Time.deltaTime);
        xDistance += xMove * Time.deltaTime;
        yDistance += yMove * Time.deltaTime;
        if (chunks == 0)
        {
            xMove = 0;
            yMove = 0;
            Instantiate(finishLine, transform.position + new Vector3(0, 7, 0), transform.rotation);
            Destroy(this.gameObject);
        }
        if (minDistance <= Vector3.Distance(transform.position, lastPosition))
        {
            int plat = Random.Range(0, platforms.Length);
            GameObject platform = platforms[plat];
            Instantiate(platform, transform.position, platform.transform.rotation);
            lastPosition = transform.position;
            xDistance = 0;
            yDistance = 0;
            chunks--;
            UpdateSpeed();
        }
        
    }

    void UpdateSpeed()
    {
        xSpeed = xSpeeds[Random.Range(0, xSpeeds.Length)];
        if (Mathf.Abs(xSpeed) == 1)
        {
            yMove = 20f;
        }
        else
            yMove = 15f;
    }
}
