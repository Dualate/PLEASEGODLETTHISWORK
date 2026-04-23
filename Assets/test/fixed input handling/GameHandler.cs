using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public bool active;
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fall()
    {
        active = false;
        this.gameObject.SetActive(false);
    }

    void Return()
    {
        active=true;
        this.gameObject.SetActive(true);
    }
    public bool GetStatus()
    {
        return active;
    }
}
