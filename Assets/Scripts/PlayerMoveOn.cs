using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMoveOn : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public Animator anim;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button7) || Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("GoToNextScene", 3f);
            
            audioSource.clip = clip;
            audioSource.Play();

            anim.SetTrigger("PlayerPress");
        }
    }

    void GoToNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
