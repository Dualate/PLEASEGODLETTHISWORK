using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    public GameObject testObject;
    public TextMeshProUGUI variable1;
    Rigidbody rb;
    int frameDelay = 150;
    // Start is called before the first frame update
    void Start()
    {
        rb = testObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (frameDelay == 0){
        variable1.text = "Velocity: " + rb.velocity.y;// * rb.mass * Mathf.Pow(rb.velocity.y, 2);             
        frameDelay = 150;
        }
        else{
            frameDelay--;
        }
    }
}
