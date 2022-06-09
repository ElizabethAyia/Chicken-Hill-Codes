using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    [SerializeField] Animator fadingAnimator;

    [SerializeField] float transitionTimer;

    [SerializeField] private Image fadeScreen;
    [SerializeField] private Sprite endState;

    [SerializeField]private float transitionCoolDown;

    private void Start()
    {
        fadeScreen = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
        if (SceneManager.GetActiveScene().name != "Menu")
            DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (transitionCoolDown > 0)
            transitionCoolDown -= Time.deltaTime;
    }

    public void SceneTransition(string nextSceneName)
    {
       StartCoroutine("NextSceneTransition", nextSceneName);
    }

    IEnumerator NextSceneTransition(string nextSceneName)
    {
        if (transitionCoolDown > 0)
        {
            yield return null;
        }
        else
        {
            transitionCoolDown = 1f;
            fadingAnimator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTimer);
            SceneManager.LoadScene(nextSceneName);
        }
    }

    public void startMenu()
    {
        fadingAnimator.SetTrigger("Skip");
    }
}
