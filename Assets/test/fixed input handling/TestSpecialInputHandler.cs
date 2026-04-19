using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class TestSpecialInputHandler : MonoBehaviour
{
    PlayerConfiguration playerConfig;
    // Start is called before the first frame update
    void Start()
    {
        playerConfig = GetComponent<NewPlayerInputHandler>().connectInput();
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }


    public void Input_onActionTriggered(CallbackContext obj){
        if (obj.action.name == "Special"){
            Debug.Log("Special");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
