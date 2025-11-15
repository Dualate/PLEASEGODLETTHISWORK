using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SIHandler : MonoBehaviour
{
    Courier courier;
    public List<Transform> players;
    // Start is called before the first frame update
    void Start()
    {
        courier = GameObject.Find("Courier").GetComponent<Courier>();
        Debug.Log(courier.name1);
        players = new List<Transform>();
        players = courier.ReturnPlayers();
// GameObject.Find("Main Camera")
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
