using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Courier : MonoBehaviour
{
    public string name1;
    public int[] numbers;
    // Start is called before the first frame update
    void Start()
    {
        numbers = new int[2];
        numbers[0] = 1;
        numbers[1] = 2;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
