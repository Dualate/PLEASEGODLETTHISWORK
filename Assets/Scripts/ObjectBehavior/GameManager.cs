using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

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

        Debug.Log("Player " + index + " wins!");
    }

    public void PlayAgain()
    {
        Debug.Log("Feature not implemented");
    }

    public void Quit()
    {
        SceneManager.LoadScene(4);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
    }

    public void Resume()
    {
        winScreen.SetActive(false);
        Time.timeScale = 1f;
    }
}
