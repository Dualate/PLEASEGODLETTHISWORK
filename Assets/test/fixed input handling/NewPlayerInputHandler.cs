using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class NewPlayerInputHandler : MonoBehaviour
{


    public PlayerConfiguration playerConfig;

    [SerializeField]
    public PlayerControls controls;

    
    Vector2 moveVector;

    [SerializeField]
    GenericMover mover;
    public void Awake()
    {
        controls = new PlayerControls();

    }



    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    public void Input_onActionTriggered(CallbackContext obj)
    {
        Debug.Log(obj);
        if (obj.action.name == controls.Gameplay.Move.name)
        {
            OnMove(obj);
        }
        else if (obj.action.name == controls.Gameplay.Jump.name)
        {
            mover.Jump();
        }
        else if (obj.action.name == controls.Gameplay.Attack.name)
        {
            mover.Attack();
        }
    }
    public void UpdateMoveVector(Vector2 moveVector)
    {

        this.moveVector = moveVector;

    }


    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        mover.UpdateMoveVector(moveVector);
    }






}
