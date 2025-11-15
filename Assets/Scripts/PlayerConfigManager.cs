using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;
public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers = 4;

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Trying to create another instance");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }

        
    }


    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        if (playerConfigs.Count == MaxPlayers && playerConfigs.All(p=> p.isReady == true))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input {  get; set; }
    public int PlayerIndex { get; set; }
    public bool isReady { get; set; }
}
