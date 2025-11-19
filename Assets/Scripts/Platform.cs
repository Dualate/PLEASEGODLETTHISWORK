using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed;
    public float floor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= floor)
            Destroy(gameObject);
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

    }
}
