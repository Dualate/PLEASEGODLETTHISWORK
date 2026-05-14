using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PlayerSetupMenuController : MonoBehaviour
{
    public int PlayerIndex;

    [SerializeField]
    private TextMeshProUGUI titleText;
    [SerializeField]
    private GameObject readyPanel;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Button readyButton;

    private float ignoreInputTime = 1.5f;
    private bool inputEnabled;

    AudioSource source;
    [SerializeField]
    AudioClip[] clips;
    private void Start()
    {
        source = GetComponent<AudioSource>();

    }
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
        titleText.SetText("Player " + (PlayerIndex + 1).ToString());
    }
    public void SetColor(GameObject animator)
    {
        int voice = -1;
        //if (!inputEnabled) { return; }
        PlayerConfigurationManager.Instance.SetAnimator(PlayerIndex, animator);
        switch (animator.name) {
            case "alicia_animator":
                voice = 0;
                break;
            case "anabeth_animator":
                voice = 1;
                break;
            case "jimena_animator":
                voice = 2;
                break;
            case "nori_animator":
                voice = 3;
                break;
        
        
        }

        source.PlayOneShot(clips[voice]);
        readyPanel.SetActive(true);
        readyButton.Select();
        menuPanel.SetActive(false);
    }


    public void ReadyPlayer()
    {
        PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
        readyButton.gameObject.SetActive(false);
    }

    public void RemovePlayer()
    {
        PlayerConfigurationManager.Instance.RemovePlayer(PlayerIndex);
        Destroy(this.gameObject);
    }
}

