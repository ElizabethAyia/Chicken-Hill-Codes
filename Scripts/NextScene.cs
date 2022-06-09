using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    public Animator Fades;
    public float TempoDeTela = 5f;

    private void Start()
    {
        StartCoroutine("TempoDeTelaCou");
    }
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name == "Laboratorio")
        {
                       
        }
       
        if (Input.GetMouseButtonDown(0) && SceneManager.GetActiveScene().name != "Sequencia_Final")
        {
            Fades.Play("Fade Out");
        }

        if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().name == "Sequencia_Final")
        {
            Fades.Play("Fade Out");
        }

    }

    private IEnumerator TempoDeTelaCou()
    {
        yield return new WaitForSeconds(TempoDeTela);
        Fades.Play("Fade Out");
    }
    public void NextSceneChanger()
    {
        if (SceneManager.GetActiveScene().name == "Sequencia_Ini_EN")
        {
            SceneManager.LoadScene("Tutorial");
        }
        else if (SceneManager.GetActiveScene().name == "Laboratorio")
        {
            SceneManager.LoadScene("Sequencia_Final");
        }

        else if (SceneManager.GetActiveScene().name == "Sequencia_Ini_BR")
        {
            SceneManager.LoadScene("Tutorial_BR");
        }

        else if (SceneManager.GetActiveScene().name == "Tutorial" || SceneManager.GetActiveScene().name == "Tutorial_BR")
        {
            SceneManager.LoadScene("porao porao");
        }


        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

            if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
