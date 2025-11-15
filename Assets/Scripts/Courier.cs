using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Courier : MonoBehaviour
{
    public string name1;
    public int[] numbers;
    public List<Transform> players;
    // Start is called before the first frame update
    void Start()
    {
        numbers = new int[2];
        numbers[0] = 1;
        numbers[1] = 2;
        DontDestroyOnLoad(gameObject);
        players = new List<Transform>();
    }


    public void PlayerJoined(Transform player)
    {
        players.Add(player);
        player.parent = transform;
    }

    public List<Transform> ReturnPlayers()
    {
        return players;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
