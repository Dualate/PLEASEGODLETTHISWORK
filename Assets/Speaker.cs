using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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


    Dictionary<string, AudioClip[]> test = new Dictionary<string, AudioClip[]>();

    [SerializeField]
    List<Dictionary<string, AudioClip>> voiceLines = new List<Dictionary<string, AudioClip>>();

    string[] characters = new string[4] { "alicia", "anabeth", "jimena", "nori" };
    

    // Start is called before the first frame update
    void Start()
    {
        test.Add(characters[0], aliciaLines);
        test.Add(characters[1], anabethLines);
        test.Add(characters[2], jimenaLines);
        test.Add(characters[3], noriLines);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class characterClip {
        string clipType;
        AudioClip clip;
        public characterClip(string type, AudioClip clip)
        {
            clipType = type;
            this.clip = clip;
        }

    }

}
