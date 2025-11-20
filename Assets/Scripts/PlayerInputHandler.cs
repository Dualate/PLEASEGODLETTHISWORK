using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private Cube cube;

    private PlayerConfiguration playerConfig;

    [SerializeField]
    private MeshRenderer playerMesh;
    private PlayerControls controls;
    private void Awake()
    {
        controls = new PlayerControls();
    }

    public void InitializePlayer(PlayerConfiguration pc)
    {
        playerConfig = pc;
        playerMesh.material = pc.PlayerMaterial;
        playerConfig.Input.onActionTriggered += Input_onActionTriggered;
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == controls.Gameplay.Move.name)
        {
            OnMove(obj);
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

    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        cube.UpdateMoveVector(moveVector);
    }
}
