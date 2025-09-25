using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Cube : MonoBehaviour
{
    PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();
    }
}
