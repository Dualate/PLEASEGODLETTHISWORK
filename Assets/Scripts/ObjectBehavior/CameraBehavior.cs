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
    public Vector3 midpoint;
    void Start()
    { 
        
        highest = transform;
        state = STATE.platform;
        
    }
    public void Add(Transform player)
    {
        players.Add(player.GetChild(0).GetComponent<Transform>());
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
        float[] coordinates = new float[2];
        float[] firstMid = new float[2];
        float[] secondMid = new float[2];
        switch (players.Count) {
            case 2:
                coordinates = midPoint(players[0].transform.position, players[1].transform.position);
                break;
            case 3:
                firstMid = midPoint(players[0].transform.position, players[1].transform.position);
                coordinates = midPoint(new Vector3(firstMid[0], firstMid[1], 0), players[2].transform.position);
                break;
            case 4:
                firstMid = midPoint(players[0].transform.position, players[1].transform.position);
                secondMid = midPoint(players[2].transform.position, players[3].transform.position);
                coordinates = midPoint(new Vector3(firstMid[0], firstMid[1], 0), new Vector3(secondMid[0], secondMid[1], 0));
                break;

        }
        midx = coordinates[0];
        midy = coordinates[1];





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

    public float[] midPoint(Vector3 firstPoint, Vector3 second_point)
    {
        float[] coordinates = new float[2];
        coordinates[0] = (firstPoint.x + second_point.x) / 2;
        coordinates[1] = (firstPoint.y + second_point.y) / 2;
        return coordinates;
    }
}
