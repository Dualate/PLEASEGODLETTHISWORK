using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PassportScript : MonoBehaviour
{
    List<GameObject> players;
    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){
            SceneManager.LoadScene("SampleScene");
        }   
    }

    public void Board(GameObject player)
    {
        players.Add(player);
        player.transform.SetParent(player.transform, false);
    }
}
