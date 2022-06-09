using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObjectsTranslator : MonoBehaviour
{
    TextManager textManagerScript;
    ClickObjects clickObjectsScript;

    private void Awake()
    {
        GameObject gameManager = GameObject.FindGameObjectWithTag("GameManager");

        textManagerScript = gameManager.GetComponent<TextManager>();
        clickObjectsScript = gameManager.GetComponent<ClickObjects>();
    }
    public void click()
    {
        if (textManagerScript.textBox.activeInHierarchy)
        {
            return;
        }


       StartCoroutine("onClickWait");
    }

    IEnumerator onClickWait()
    {
        yield return new WaitForSeconds(0.05f);
        switch (gameObject.name)
        {
            case string a when a.Contains("Green_Cube"):
                clickObjectsScript.ItemSelector("Green_Cube");
                break;
            case string a when a.Contains("Tape_UI"):
                clickObjectsScript.ItemSelector("Tape_UI");
                break;
            case string a when a.Contains("Disquete_UI"):
                clickObjectsScript.ItemSelector("Disquete_UI");
                break;
            case string a when a.Contains("Machado_UI"):
                clickObjectsScript.ItemSelector("Machado_UI");
                break;
            case string a when a.Contains("Chave_01_UI"):
                clickObjectsScript.ItemSelector("Chave_1_UI");
                break;
            //case string a when a.Contains("Cacto_UI"):
               // clickObjectsScript.ItemSelector("Cacto_UI");
                //break;
            case string a when a.Contains("Celula_De_Energia_UI"):
                clickObjectsScript.ItemSelector("Celula_De_Energia_UI");
                break;
            case string a when a.Contains("Chave_02_UI"):
                clickObjectsScript.ItemSelector("Chave_2_UI");
                break;
            case string a when a.Contains("Tonico_UI"):
                clickObjectsScript.ItemSelector("Tonico_UI");
                break;
            case string a when a.Contains("Bananao_UI"):
                clickObjectsScript.ItemSelector("Bananao_UI");
                break;
            case string a when a.Contains("Pasta_De_Banana_UI"):
                clickObjectsScript.ItemSelector("Pasta_De_Banana_UI");
                break;
            case string a when a.Contains("Cacto_Chave_1"):
                clickObjectsScript.ItemSelector("Cacto_Chave_1");
                break;
            case string a when a.Contains("Cacto_Chave_2"):
                clickObjectsScript.ItemSelector("Cacto_Chave_2");
                break;
            case string a when a.Contains("Cacto_Chave_3"):
                clickObjectsScript.ItemSelector("Cacto_Chave_3");
                break;
            case string a when a.Contains("Cacto_Chave_4"):
                clickObjectsScript.ItemSelector("Cacto_Chave_4");
                break;
            default:
                break;
        }

    }
}
