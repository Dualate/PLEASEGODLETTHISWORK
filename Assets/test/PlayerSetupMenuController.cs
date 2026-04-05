using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerSetupMenuController : MonoBehaviour
{
    private int PlayerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    public GameObject player;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
        titleText.SetText("Player " + (pi+1).ToString());
        ignoreInputTime = Time.time + ignoreInputTime;
    }
    void Update()
    {
        if (Time.time > ignoreInputTime)
        {
            inputEnabled = true;
        }
    }
    public void SetColor(GameObject prefab)
    {
        if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.SetPlayerColor(PlayerIndex, prefab);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
        player = Instantiate(prefab, transform.position, transform.rotation, transform);
        GameObject.Find("Passport").GetComponent<PassportScript>().Board(this.gameObject);
    }


    public void ReadyPlayer()
    {
        PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }
}
