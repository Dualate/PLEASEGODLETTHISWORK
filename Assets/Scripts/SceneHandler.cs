using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    int index = 0;
    public TextMeshProUGUI[] cornerTexts;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
    public TextMeshProUGUI getCorner()
    {
        index += 1;
        return cornerTexts[index - 1];
    }
}
