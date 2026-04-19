using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadScene(string phrase)
    {
        int sceneIndex = int.Parse(phrase.Substring(0, 1));
        int players = int.Parse(phrase.Substring(1, 1));
        GameObject.Find("SceneReader").GetComponent<SceneReader>().LoadData(sceneIndex, players);
        SceneManager.LoadScene(3);
    }
}
