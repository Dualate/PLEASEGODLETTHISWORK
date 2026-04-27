using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public float returnTimer;
    float timer = 0;
    // Start is called before the first frame update
    bool active;
    GameObject playerIcon;
    void Start()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!active)
        {
            timer += Time.deltaTime;
        }
        if (timer >= returnTimer)
        {
            timer = 0;
            Return();
        }
    }

    public void Fall()
    {
        active = false;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
    }

    void Return()
    {
        active=true;
        GameObject.Find("Main Camera").GetComponent<CameraBehavior>().ReturnPlayer(GetComponentInChildren<Transform>());
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

    }
    public bool GetStatus()
    {
        return active;
    }

    public void InitializeUI(GameObject icon)
    {

    }
}
