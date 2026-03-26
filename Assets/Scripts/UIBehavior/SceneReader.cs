using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReader : MonoBehaviour
{
    int sceneIndex;
    int players;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);    
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "inputPermanenceTest")
        {
            Debug.Log(players);
        }
    }

    public void LoadData(int index, int players)
    {
        sceneIndex = index;
        this.players = players;
    }

}
