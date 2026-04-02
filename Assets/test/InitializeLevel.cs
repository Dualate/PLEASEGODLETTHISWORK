using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    CameraBehavior camera;
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation);
            var animator = Instantiate(playerConfigs[i].animator, player.transform.position + new Vector3(0, 1.15f, -1f), transform.rotation, player.transform.Find("LightMelee").GetComponent<Transform>());             
            player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
            camera.Add(player.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
