using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public List<Transform> players;
    enum STATE { platform, fight };
    STATE state;
    public Vector3 offset;
    public Transform highest;
    public float smoothing;
    Transform arena;
    void Start()
    { 
        
        highest = transform;
        state = STATE.platform;
        
    }
    public void Add(Transform player)
    {
        players.Add(player);
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (state == STATE.platform)
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
        if (state == STATE.fight)
        {
            transform.position = arena.position + offset;
        }
    }

    public void fight()
    {
        state = STATE.fight;
    }

    public void arenaLanded(Transform arena)
    {
        this.arena = arena;
    }

    public void climb()
    {
        state = STATE.platform;
    }
}
