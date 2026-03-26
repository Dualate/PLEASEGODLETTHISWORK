using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FinishLine : MonoBehaviour
{
    public bool finishLineCrossed = false;
    public bool SingleController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (SingleController)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EndGame(1);
        }
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().EndGame(collider.gameObject.GetComponent<PlayerInputHandler>().GetIndex() + 1);
        }
    }
}
