using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    List<Material> colors;
    [SerializeField]
    List<Transform> starters;
    [SerializeField]
    List<TextMeshProUGUI> texts;

    List<Transform> players = new List<Transform>();

    public bool running = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public TextMeshProUGUI CheckIn(GameObject player)
    {
        players.Add(player.GetComponent<Transform>());
         int number = Random.Range(0, starters.Count);
        Transform startingPos = starters[number];
        player.transform.position = startingPos.position;
        starters.Remove(startingPos);

        Material color = colors[number];
        player.GetComponent<MeshRenderer>().material = color;
        colors.Remove(color);
        TextMeshProUGUI readyText = texts[number];
        texts.Remove(readyText);

        return readyText;
    }

    void Update()
    {
        if (players.Count == 0)
            return;
        foreach (var player in players)
        {
            if (player.gameObject.GetComponent<Cube_Keyboard>().state == Cube_Keyboard.STATE.DORMANT)
            {
                return;
            }
        }
        Transform[] transforms = new Transform[players.Count];
        for (int i = 0; i < players.Count; i++)
        {
            transforms[i] = players[i].transform;
        }
        if (!running)
        {
            GameObject.Find("Main Camera").GetComponent<SelectCameraScript>().Setup(transforms);
            running = true;
            GameObject.Find("Spawner").GetComponent<Spawner>().Activate();
        }
    }

}
