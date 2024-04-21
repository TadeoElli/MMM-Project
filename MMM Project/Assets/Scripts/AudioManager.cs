using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set;}
    // Start is called before the first frame update
    [SerializeField] private AudioSource musicSource, effectsSource;
    [SerializeField] private AudioClip menuMusic;
    private void Awake() {
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayMusic(menuMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayMusic( AudioClip music){
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySoundEffect(AudioClip effect){
        effectsSource.PlayOneShot(effect);
    }

    public void SetMusicVolume(float amount){
        musicSource.volume = amount;
    }
    public void SetEffectsVolume(float amount){
        effectsSource.volume = amount;
    }
}
