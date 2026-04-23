using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;

    [SerializeField]
    public GameObject[] playerPrefabs;
    [SerializeField]
    GameObject playerModel;
    [SerializeField]
    CameraBehavior camera;
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();

        for (int i = 0; i < playerConfigs.Length; i++)
        {
            
            switch (playerConfigs[i].animator.name)
            {
                case "anabeth_animator":
                    playerModel = playerPrefabs[0];
                    break;
                case "alicia_animator":
                    playerModel = playerPrefabs[1];
                    break;
                case "nori_animator":
                    playerModel = playerPrefabs[2];
                    break;
                case "jimena_animator":
                    playerModel = playerPrefabs[3];
                    break;
            }
            
            var player = Instantiate(playerModel, playerSpawns[i].position, playerSpawns[i].rotation);
            var animator = Instantiate(playerConfigs[i].animator, player.transform.position + new Vector3(0, 1.15f, -1f), transform.rotation, player.transform.GetChild(0).GetComponent<Transform>());
            switch (playerModel.name)
            {
                case "Alicia":
                case "Anabeth":
                    player.GetComponent<NewPlayerInputHandler>().InitializePlayer(playerConfigs[i]);

                    break;
                case "Nori":
                case "Jimena":
                    player.GetComponent<RangedPlayerInputHandler>().InitializePlayer(playerConfigs[i]);

                    break;

            }
            
            camera.Add(player.transform);

        }
    }

}
