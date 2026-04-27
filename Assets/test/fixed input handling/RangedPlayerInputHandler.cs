using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class RangedPlayerInputHandler : MonoBehaviour
{


    public PlayerConfiguration playerConfig;

    [SerializeField]
    public PlayerControls controls;


    Vector2 moveVector;

    [SerializeField]
    GenericRanged mover;
    public void Awake()
    {
        controls = new PlayerControls();

    }

    private void Start()
    {
        mover.SetIndex(playerConfig.PlayerIndex);
    }
    public int GetIndex()
    {
        return playerConfig.PlayerIndex;
    }

    public void InitializePlayer(PlayerConfiguration pc, GameObject icon)
    {
        playerConfig = pc;
        mover.SetIndex(pc.PlayerIndex);
        mover.SetIcon(icon);
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    public void Input_onActionTriggered(CallbackContext obj)
    {
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

    public PlayerConfiguration connectInput()
    {
        return playerConfig;
    }


    public void OnMove(CallbackContext context)
    {
        mover.UpdateMoveVector(context.ReadValue<Vector2>());

    }






}
