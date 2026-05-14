using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Speaker : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    AudioClip[] songClips;

    [SerializeField]
    AudioClip[] anabethLines;

    [SerializeField]
    AudioClip[] aliciaLines;

    [SerializeField]
    AudioClip[] jimenaLines;

    [SerializeField]
    AudioClip[] noriLines;

    AudioClip currentClip;
    public Dictionary<string, AudioClip[]> test = new Dictionary<string, AudioClip[]>();


    string[] characters = new string[4] { "alicia", "anabeth", "jimena", "nori" };
    

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        test.Add(characters[0], aliciaLines);
        test.Add(characters[1], anabethLines);
        test.Add(characters[2], jimenaLines);
        test.Add(characters[3], noriLines);
        currentClip = songClips[1];
        source.clip = currentClip;
        source.Play();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            //source.Pla
        }
    }


}
