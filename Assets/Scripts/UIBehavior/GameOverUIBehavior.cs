using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUIBehavior : MonoBehaviour
{
    private void Start()
    {
        Destroy(GameObject.Find("PlayerConfigurationManager"));
        Destroy(GameObject.Find("SceneReader"));
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(4);
    }

    public void Quit()
    {
        SceneManager.LoadScene(1);
    }
}
