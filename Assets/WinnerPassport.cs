using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerPassport : MonoBehaviour
{
    public int winnerIndex;


    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            GameObject.Find("winnerText").GetComponent<TMPro.TextMeshProUGUI>().text = "Player " + winnerIndex;
            Destroy(this.gameObject);

        }
    }
}
