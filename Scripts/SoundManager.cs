using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] objectsSoundClips;
    public AudioClip[] radioMusics;
    public AudioClip[] otherMusics;
    [SerializeField] AudioClip typingSound;
    private AudioSource[] mainAudioSource = new AudioSource[2];
    // 0 -> Musicas 1 -> sons

    private int mainMusicIndex = 0;
    // 0 = bossa nova 1 = Funk Track 1 2 = Funk Track 2 3 = Tema Dig Hill

    private void Start()
    {
        mainAudioSource = gameObject.GetComponents<AudioSource>();
        changeMainMusic(true);
    }

    public void playASoundClip(int itemIndex)
    {
        if (mainAudioSource[1].isPlaying)
            mainAudioSource[1].Stop();

        mainAudioSource[1].PlayOneShot(objectsSoundClips[itemIndex]);
    }

    public void playTypingSound(bool start)
    {
        mainAudioSource[1].clip = typingSound;

        if (start)
        {
            mainAudioSource[1].Play();
            mainAudioSource[1].loop = true;
        }
        else
        {
            mainAudioSource[1].Stop();
            mainAudioSource[1].loop = false;
        }           
    }
    
    public void changeMainMusic (bool starting)
    {
        if (starting)
        {
            mainAudioSource[0].clip = radioMusics[0];
            mainAudioSource[0].loop = true;
            mainAudioSource[0].Play();
            return;
        }

        mainMusicIndex++;
        if (mainMusicIndex > 3)
            mainMusicIndex = 0;
        mainAudioSource[0].clip = radioMusics[mainMusicIndex];
        mainAudioSource[0].Play();
    }
}
