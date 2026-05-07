using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public float returnTimer;
    public float colliderTimer;
    public float colliderTime;
    float timer = 0;
    // Start is called before the first frame update
    bool active = true;
    bool colliderActive = true;
    GameObject playerIcon;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!colliderActive)
        {
            colliderTime += Time.deltaTime;
        }
        if (colliderTime > colliderTimer)
        {
            colliderActive = true;
            colliderTime = 0;
            transform.GetChild(0).transform.position += new Vector3(0, 0, -1);
        }
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
        colliderActive = false;


    }
    public bool GetStatus()
    {
        return active;
    }

    public void InitializeUI(GameObject icon)
    {

    }
}
