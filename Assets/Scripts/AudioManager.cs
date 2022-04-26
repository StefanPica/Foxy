using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] soundEffects;

    public AudioSource bgm, levelEndMusic;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySFX(int SoundToPlay)
    {
        soundEffects[SoundToPlay].Stop();

        soundEffects[SoundToPlay].pitch = Random.Range(.9f, 1.1f);

        soundEffects[SoundToPlay].Play();
    }
}
