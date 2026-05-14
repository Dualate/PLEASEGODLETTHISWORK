using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Users;


public class PlayerConfigurationManager : MonoBehaviour
{


    [SerializeField]
    GameObject joinText;
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers = 1;

    int players = 0;
    int sceneIndex = 2;
    int[] activeScenes = { 2, 4, 5, 6 };
    public static PlayerConfigurationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            this.playerConfigs = new List<PlayerConfiguration>();
        }
        MaxPlayers = GameObject.Find("SceneReader").GetComponent<SceneReader>().GetMaxPlayers();
        sceneIndex = GameObject.Find("SceneReader").GetComponent<SceneReader>().GetSceneIndex();
    }

    private void Update()
    {
        if (!activeScenes.Contains(SceneManager.GetActiveScene().buildIndex))
        {
            Destroy(GameObject.Find("SceneReader"));
            Destroy(this.gameObject);
            

        }
    }
    public void SetAnimator(int index, GameObject animator)
    {
        playerConfigs[index].animator = animator;
    }


    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if (playerConfigs.Count != 0 && playerConfigs.All(p => p.IsReady == true))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

    public void RemovePlayer(int playerIndex)
    {
        playerConfigs.RemoveAt(playerIndex);
        players--;

        if (playerConfigs.Count == 0)
        {
            SceneManager.LoadScene(3);
        }
        else
        {
            foreach (var player in playerConfigs)
            {
                if (player.PlayerIndex > playerIndex)
                    player.PlayerIndex -= 1;
            }
            PlayerSetupMenuController[] temp = GameObject.Find("MainLayout").GetComponentsInChildren<PlayerSetupMenuController>();
            foreach (var player in temp)
            {
                if (player.PlayerIndex > playerIndex)
                    player.PlayerIndex -= 1;
            }
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (!(SceneManager.GetActiveScene().name == "inputPermanenceTest"))
            return;
        if (joinText.activeSelf == true)
            joinText.SetActive(false);
        
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
            pi.transform.SetParent(transform);
            playerConfigs[players].deviceName = pi.devices[0].deviceId;
            players += 1;
            if (playerConfigs.Count == MaxPlayers)
            {
                GetComponent<PlayerInputManager>().DisableJoining();
            }
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }
}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public int deviceName { get; set; }
    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public GameObject animator;

}
