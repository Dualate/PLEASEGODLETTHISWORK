using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReader : MonoBehaviour
{
    int sceneIndex;
    int players;
    // Start is called before the first frame update

    public static SceneReader Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Trying to create another instance of a singleton");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
    }

    private void Update()
    {
        
    }

    public void LoadData(int index, int players)
    {
        sceneIndex = index;
        this.players = players;
    }

    public int GetMaxPlayers()
    {
        return players;
    }

    public int GetSceneIndex()
    {
        return sceneIndex;
    }

}
