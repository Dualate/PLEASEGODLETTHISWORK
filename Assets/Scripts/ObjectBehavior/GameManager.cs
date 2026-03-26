using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject winScreen;
    [SerializeField]
    TextMeshProUGUI winnerText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void Update()
    {
        
        
    }

    public void EndGame(int index)
    {

        winScreen.SetActive(true);

        winnerText.text = "Player " + index + " wins!";
    }

    public void PlayAgain()
    {
        Debug.Log("Feature not implemented");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
