using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DestroyProFim : MonoBehaviour
{

    void Start()
    {
        Destroy(GameObject.FindGameObjectWithTag("GameManager"));
        Destroy(GameObject.FindGameObjectWithTag("UI Canvas"));
    }

}
