using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class ProtoConfigManager : MonoBehaviour
{
    public List<ProtoConfig> players;

    [SerializeField]
    private int MaxPlayers = 4;

    public static ProtoConfigManager Instance { get; private set; }

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
            players = new List<ProtoConfig>();
        }
    }

    public void ReadyPlayer(int index)
    {
        players[index].isReady = true;
        if (players.Count == MaxPlayers && players.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {

        if (!players.Any(p=> p.PlayerIndex == pi.playerIndex))
        {
            pi.transform.SetParent(transform);
            players.Add(new ProtoConfig(pi));
        }
    }

    public List<ProtoConfig> GetPlayers()
    {
        return players;
    }
}

public class ProtoConfig {

    public ProtoConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public int PlayerIndex { get; set; }
    public PlayerInput Input { get; set; }
    public bool isReady { get; set; }
}

