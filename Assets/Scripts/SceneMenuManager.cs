using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SceneMenuManager : MonoBehaviour
{
    public TextMeshProUGUI[] cornerTexts;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextMeshProUGUI getCorner()
    {
        index += 1;
        return cornerTexts[index -1];
    }
}
