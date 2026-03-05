using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBehavior : MonoBehaviour
{
    List<GameObject> players = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            players.Add(collision.gameObject);
        }

    }

    private void Update()
    {
        if (players.Count == GameObject.Find("Main Camera").GetComponent<CameraBehavior>().players.Count)
        {
            GameObject.Find("Main Camera").GetComponent<CameraBehavior>().fight();
        }
    }

}
