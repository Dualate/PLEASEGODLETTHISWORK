using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
    [SerializeField]
    GameObject joinText;
    [SerializeField]
    PassportScript passport;
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers = 1;

    int sceneIndex;
    public static PlayerConfigurationManager Instance { get; private set; }

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
            this.playerConfigs = new List<PlayerConfiguration>();
        }
        MaxPlayers = GameObject.Find("SceneReader").GetComponent<SceneReader>().GetMaxPlayers();
        sceneIndex = GameObject.Find("SceneReader").GetComponent<SceneReader>().GetSceneIndex();
    }

    public void SetPlayerColor(int index, GameObject animator)
    {
        playerConfigs[index].animator = animator;
        Debug.Log(passport);
    }


    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if (/*playerConfigs.Count == MaxPlayers &&*/playerConfigs.Count != 0 && playerConfigs.All(p => p.IsReady == true))
        {
            if (passport.Ready())
            {
                SceneManager.LoadScene(sceneIndex);                
            }
           
        }
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        if (joinText.activeSelf == true)
            joinText.SetActive(false);
        
        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
            pi.transform.SetParent(transform);
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

    public PlayerInput Input { get; set; }
    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }
    public Material PlayerMaterial { get; set; }

    public GameObject animator;

}
