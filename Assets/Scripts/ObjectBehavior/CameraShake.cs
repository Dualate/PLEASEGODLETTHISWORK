using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator cameraAnimation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CamShake()
    {
        Debug.Log("Shake");
        cameraAnimation.SetTrigger("Shake");
    }
}
