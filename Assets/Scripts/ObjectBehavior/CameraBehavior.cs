using Palmmedia.ReportGenerator.Core.CodeAnalysis;
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
    public float avgDistance = 0;
    int activePlayers;
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

            if (activePlayers > 0)
            {
                foreach (Transform t in players)
                {
                    if (t.position.y > highest.position.y && t.position.x >= -20 && t.position.x <= 30) //camera follows highest position player unless they are too far to the left or right
                    {
                        highest = t;
                    }
                }
                if (activePlayers == 2)
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
        activePlayers = 0;
        foreach (var player in players)
        {
            if (player.parent.gameObject.GetComponent<GameHandler>().GetStatus())
            {
                activePlayers++;
            }
        }
        Transform[] active = new Transform[activePlayers];
        for (int i = 0; i < activePlayers; i++)
        {
            active[i] = players[i];
        }
        float midx = 0;
        float midy = 0;
        float[] coordinates = new float[2];
        float[] firstMid = new float[2];
        float[] secondMid = new float[2];
        switch (active.Length) {
            case 2:
                coordinates = midPoint(active[0].transform.position, active[1].transform.position);
                break;
            case 3:
                firstMid = midPoint(active[0].transform.position, active[1].transform.position);
                coordinates = midPoint(new Vector3(firstMid[0], firstMid[1], 0), active[2].transform.position);
                break;
            case 4:
                firstMid = midPoint(active[0].transform.position, active[1].transform.position);
                secondMid = midPoint(active[2].transform.position, active[3].transform.position);
                coordinates = midPoint(new Vector3(firstMid[0], firstMid[1], 0), new Vector3(secondMid[0], secondMid[1], 0));
                break;

        }
        midx = coordinates[0];
        midy = coordinates[1];




        float zoomDist = 0.308684f * avgDistance + 0.00000666667f;
        midpoint = new Vector3(midx, midy, -zoomDist);

        foreach (var player in players)
        {
            if (Vector3.Distance(player.position, highest.position) > 25f)
            {
                player.parent.gameObject.GetComponent<GameHandler>().Fall();
            }
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

    public float[] midPoint(Vector3 firstPoint, Vector3 second_point)
    {
        avgDistance = Vector3.Distance(firstPoint, second_point);
        float[] coordinates = new float[2];
        coordinates[0] = (firstPoint.x + second_point.x) / 2;
        coordinates[1] = (firstPoint.y + second_point.y) / 2;
        return coordinates;
    }
}
