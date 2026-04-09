using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private Player cube;

    private PlayerConfiguration playerConfig;
    public int deviceId;
    [SerializeField]
    private PlayerControls controls;
    private void Awake()
    {
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
        deviceId = playerConfig.deviceName;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        Debug.Log(obj.ToString());
        Debug.Log("Device #" + obj.control.device.deviceId + " told " + deviceId + " to move");
        if (obj.control.device.deviceId == deviceId)
        {
            if (obj.action.name == controls.Gameplay.Move.name)
            {
                cube.OnMove(obj);
            }
            else if (obj.action.name == controls.Gameplay.Jump.name)
            {
                cube.Jump();
            }
            else if (obj.action.name == controls.Gameplay.Attack.name)
            {
                cube.Attack();
            }
        }
        
    }

    public int GetIndex()
    {
        return playerConfig.PlayerIndex;
    }


    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        cube.UpdateMoveVector(moveVector);
    }
}
