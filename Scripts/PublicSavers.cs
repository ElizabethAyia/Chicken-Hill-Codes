using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublicSavers : MonoBehaviour
{
    public bool gameInPortuguese = true;
    public string qualitySet;
    public int volumeSet;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

}
