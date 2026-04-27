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
        DontDestroyOnLoad(this);
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
