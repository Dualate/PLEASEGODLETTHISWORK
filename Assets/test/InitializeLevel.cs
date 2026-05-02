using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InitializeLevel : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawns;

    [SerializeField]
    public GameObject[] playerPrefabs;

    [SerializeField]
    GameObject[] icons;

    [SerializeField]
    Transform[] iconSpots;

    GameObject playerIcon;

    GameObject playerModel;
    [SerializeField]
    CameraBehavior camera;
    void Start()
    {
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();

        for (int i = 0; i < playerConfigs.Length; i++)
        {
            GameObject icon;
            switch (playerConfigs[i].animator.name)
            {
                case "anabeth_animator":
                    playerModel = playerPrefabs[0];
                    playerIcon = icons[0];
                    break;
                case "alicia_animator":
                    playerModel = playerPrefabs[1];
                    playerIcon = icons[1];
                    break;
                case "nori_animator":
                    playerModel = playerPrefabs[2];
                    playerIcon = icons[2];
                    break;
                case "jimena_animator":
                    playerModel = playerPrefabs[3];
                    playerIcon = icons[3];
                    break;
            }
            
            var player = Instantiate(playerModel, playerSpawns[i].position, playerSpawns[i].rotation);
            var animator = Instantiate(playerConfigs[i].animator, player.transform.position + new Vector3(0, 1.15f, -1f), transform.rotation, player.transform.GetChild(0).GetComponent<Transform>());
            switch (playerModel.name)
            {
                case "Alicia":

                case "Anabeth":
                    icon = Instantiate(playerIcon, iconSpots[i]);
                    player.GetComponent<NewPlayerInputHandler>().InitializePlayer(playerConfigs[i], icon);
                    break;

                case "Nori":
                case "Jimena":
                    icon = Instantiate(playerIcon, iconSpots[i]);
                    player.GetComponent<RangedPlayerInputHandler>().InitializePlayer(playerConfigs[i], icon);
                    break;

            }

            camera.Add(player.transform);

        }

    }

}
