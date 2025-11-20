using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCameraScript : MonoBehaviour
{
    Transform[] players;
    bool running = false;
    Transform highest;
    public float smoothing;
    public Vector3 offset;

    public void Setup(Transform[] players)
    {
        highest = transform;
        this.players = players;
        running = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (running)
        {
            if (players.Length > 0)
            {
                foreach (Transform t in players)
                {
                    if (t.position.y > highest.position.y && t.position.x >= -20 && t.position.x <= 30) //camera follows highest position player unless they are too far to the left or right
                    {
                        highest = t;
                    }
                }
                transform.position = Vector3.Lerp(transform.position, highest.position + offset, smoothing * Time.deltaTime);
            }
        }
    }
}
