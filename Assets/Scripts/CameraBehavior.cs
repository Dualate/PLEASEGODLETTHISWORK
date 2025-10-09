using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public List<Transform> players;

    public Vector3 offset;
    Transform highest;
    public float smoothing;
    void Awake()
    {
        players = new List<Transform>();
        highest = transform;
    }

    public void Add(Transform player)
    {
        players.Add(player);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (players.Count > 0) 
        {
            foreach (Transform t in players)
            {
                if (t.position.y > highest.position.y)
                {
                    highest = t;
                }
            }
            transform.position = Vector3.Lerp(transform.position, highest.position + offset, smoothing * Time.deltaTime);
        }
        
    }
}
