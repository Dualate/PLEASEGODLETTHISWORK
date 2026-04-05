using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Rework_PlayerInputHandler : MonoBehaviour
{
    [SerializeField]
    private Player playerPrefab;

    private PlayerControls playerControls;

    // Start is called before the first frame update
    void Start()
    {
        playerControls = new PlayerControls();
        transform.SetParent(GameObject.Find("Passport").GetComponent<Transform>());
        GameObject.Find("Passport").GetComponent<PassportScript>().Board(this.gameObject);
    }

    private void Input_onActionTriggered(CallbackContext obj)
    {
        if (obj.action.name == playerControls.Gameplay.Move.name)
        {
            OnMove(obj);
        }
        else if (obj.action.name == playerControls.Gameplay.Jump.name)
        {
            playerPrefab.Jump();
        }
        else if (obj.action.name == playerControls.Gameplay.Attack.name)
        {
            playerPrefab.Attack();
        }
    }



    public void OnMove(CallbackContext context)
    {
        Vector2 moveVector = context.ReadValue<Vector2>();
        playerPrefab.UpdateMoveVector(moveVector);
    }
}

