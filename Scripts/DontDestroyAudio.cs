using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    static bool AudioBegin = false;
    void Awake()
    {
        if (!AudioBegin)
        {
            GetComponent<AudioSource>().Play();
            DontDestroyOnLoad(gameObject);
            AudioBegin = true;
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            GetComponent<AudioSource>().Stop();
            AudioBegin = false;
        }

    }
}
