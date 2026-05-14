using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinnerPassport : MonoBehaviour
{
    public int winnerIndex;
    string winnerName;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            GameObject.Find("winnerText").GetComponent<TMPro.TextMeshProUGUI>().text = "Player " + winnerIndex + " wins!";
            switch (winnerName) {
                case "anabeth_animator":
                    break;
                case "alicia_animator":
                    break;
                case "jimena_animator":
                    break;
                case "nori_animator":
                    break;
                
            }

            Destroy(this.gameObject);

        }
    }
}
