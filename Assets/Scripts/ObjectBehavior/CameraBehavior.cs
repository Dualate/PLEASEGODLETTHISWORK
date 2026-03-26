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
    Vector3 midpoint;
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
                if (players.Count == 2)
                {
                    transform.position = Vector3.Lerp(transform.position, midpoint + offset, smoothing * Time.deltaTime);
                }
                else
                    transform.position = Vector3.Lerp(transform.position, highest.position + offset, smoothing * Time.deltaTime);
            }
        }
        if (state == STATE.fight)
        {
            transform.position = arena.position + offset;
        }
    }


    void Update()
    {
        float midx = 0;
        float midy = 0;
        if (players.Count == 2)
        {
            midx = (players[0].transform.position.x + players[1].transform.position.x) / 2;
            midy = (players[0].transform.position.y + players[1].transform.position.y) / 2;
        }
        midpoint = new Vector3(midx, midy, 0);
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
