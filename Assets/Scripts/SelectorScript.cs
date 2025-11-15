using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;
using TMPro;
public class SelectorScript : MonoBehaviour
{
    Vector2 moveVector;
    public float moveSpeed;
    float xSpeed;
    float ySpeed;
    TextMeshProUGUI cornerText;
    public TextMeshProUGUI labelText;
    public Transform[] characterIcons;
    bool chosen = false;
    void Start()
    {
        cornerText = GameObject.Find("SceneMenuManager").GetComponent<SceneMenuManager>().getCorner();
        labelText.text = "P" + cornerText.name;
        cornerText.text = "Select a character";
        characterIcons = GameObject.Find("Characters").GetComponentsInChildren<Transform>();
    }

    public void Move(CallbackContext context)
    {
        moveVector = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!chosen)
        {
            ySpeed = moveVector.y * moveSpeed * Time.deltaTime;
            xSpeed = moveVector.x * moveSpeed * Time.deltaTime;
            transform.Translate(xSpeed, ySpeed, 0);

            Transform closest = null;
            foreach (Transform t in characterIcons)
            {
                if (t.gameObject.name == "Characters")
                    continue;
                if (closest == null)
                {
                    closest = t;
                }
                if (Vector3.Distance(transform.position, closest.position) > Vector3.Distance(transform.position, t.position))
                    closest = t;
            }
            cornerText.text = closest.name;
        }
        
    }

    public void Select()
    {
        if (!chosen)
        {
            chosen = true;
            cornerText.text += " (ready!)";
        }
    }


    public void Deselect()
    {
        chosen = false;
        cornerText.text = cornerText.text.Replace(" (ready!)", "");
    }
}
