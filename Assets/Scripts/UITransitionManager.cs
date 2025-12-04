using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class UITransitionManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject[] buttons;      
    public Animator backgroundAnim;     

    [Header("Settings")]
    public float transitionTime = 1.5f; 

    
    public void TriggerTransition(int sceneIndex)
    {
        StartCoroutine(TransitionSequence(sceneIndex));
    }

    
    public void TriggerQuit()
    {
        StartCoroutine(QuitSequence());
    }

    private IEnumerator TransitionSequence(int sceneIndex)
    {
        DisableButtons();

        
        backgroundAnim.SetTrigger("Play");

        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneIndex);
    }

    private IEnumerator QuitSequence()
    {
        DisableButtons();

        backgroundAnim.SetTrigger("Play");

        yield return new WaitForSeconds(transitionTime);

        Application.Quit();
    }

    private void DisableButtons()
    {
        foreach (GameObject btn in buttons)
        {
            btn.SetActive(false);
        }
    }
}
