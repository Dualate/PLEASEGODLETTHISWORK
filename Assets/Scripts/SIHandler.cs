using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SIHandler : MonoBehaviour
{
    Courier courier;
    // Start is called before the first frame update
    void Start()
    {
        courier = GameObject.Find("Courier").GetComponent<Courier>();
        Debug.Log("Courier's name is " + courier.name1);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (int number in courier.numbers)
        {
            Debug.Log(number);
        }
    }
}
