using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDuplicate : MonoBehaviour
{
    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1 && gameObject.tag == "GameManager")
            Destroy(gameObject);
        if (GameObject.FindGameObjectsWithTag("UI Canvas").Length > 1 && gameObject.tag == "UI Canvas")
            Destroy(gameObject);
        if (GameObject.FindGameObjectsWithTag("Transition").Length > 1 && gameObject.tag == "Transition")
            Destroy(gameObject);
        if (GameObject.FindGameObjectsWithTag("Public Saver").Length > 1 && gameObject.tag == "Public Saver")
            Destroy(gameObject);
    }
}
