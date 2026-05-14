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

    bool startingFlag = false;
    public Dictionary<string, AudioClip[]> test = new Dictionary<string, AudioClip[]>();


    string[] characters = new string[4] { "alicia", "anabeth", "jimena", "nori" };

    Dictionary<string, AudioClip> playlist = new Dictionary<string, AudioClip>();
    // Start is called before the first frame update
    void Start()
    {
        playlist.Add("SampleScene", songClips[0]);
        playlist.Add("StartScreen", songClips[1]);
        playlist.Add("ModeSelector", songClips[1]);
        playlist.Add("PlayQuit", songClips[1]);
        playlist.Add("inputPermanenceTest", songClips[1]);
        playlist.Add("GameOver", songClips[1]);
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
        if (startingFlag)
        {
            if (SceneManager.GetActiveScene().name == "StartScreen")
                Destroy(this.gameObject);
        }
        if (SceneManager.GetActiveScene().name != "StartScreen")
            startingFlag = true;
        if (playlist[SceneManager.GetActiveScene().name] != source.clip){
            source.Stop();
            source.clip = playlist[SceneManager.GetActiveScene().name];
            source.Play();
        }
    }


}
