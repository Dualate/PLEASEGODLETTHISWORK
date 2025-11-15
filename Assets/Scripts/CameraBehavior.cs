using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public List<Transform> players;

    public Vector3 offset;
    public Transform highest;
    public float smoothing;
    void Start()
    { 
        
        highest = transform;
        foreach(Transform cube in players)
        {
            cube.gameObject.GetComponent<Cube>().Activate();
        }
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
                if (t.position.y > highest.position.y && t.position.x >= -20 && t.position.x <= 30) //camera follows highest position player unless they are too far to the left or right
                {
                    highest = t;
                }
            }
            transform.position = Vector3.Lerp(transform.position, highest.position + offset, smoothing * Time.deltaTime);
        }
        
    }
}
