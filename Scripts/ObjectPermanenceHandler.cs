using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPermanenceHandler : MonoBehaviour
{
    private PickUpableObject pickObjectsScript;
    private PuzzlesHandler puzzleScript;
    private ClickObjects clickScript;
    void Start()
    {
        pickObjectsScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PickUpableObject>();
        puzzleScript = pickObjectsScript.gameObject.GetComponent<PuzzlesHandler>();
        clickScript = pickObjectsScript.gameObject.GetComponent<ClickObjects>();
        objectOfPermanence();
    }

    private void objectOfPermanence()
    {
        if (SceneManager.GetActiveScene().name == "Rua")
        {
            if (pickObjectsScript.machado_pckd)
                Destroy(GameObject.Find("Machado"));
        }

        switch(SceneManager.GetActiveScene().name)
        {
            case "Dispensa":
                //puzzleScript.lightsKeeper();
                if (pickObjectsScript.disk_pckd)
                    Destroy(GameObject.Find("Disquete"));
                for(int i = 0; i < 4; i++)
                {
                    if (puzzleScript.puzzlesDispensaComplete[i])
                    {
                        Animator doorAnim = GameObject.Find("Door_" + (i + 1)).GetComponent<Animator>();
                        doorAnim.SetTrigger("Maintain");
                        if (pickObjectsScript.cactos_picked[i])
                            Destroy(GameObject.Find("Cacto_Chave_" + (i + 1)));
                    }
                }
                puzzleScript.resetAllPuzzles();

                break;
            case "Rua":
                if (pickObjectsScript.machado_pckd)
                    Destroy(GameObject.Find("Machado"));
                break;
            case "Garagem":
                if (puzzleScript.puzzleCompletion == 4)
                {
                    Destroy(GameObject.Find("Celula_De_Energia"));
                    Animator celulaAnimator = GameObject.Find("Gerador").GetComponent<Animator>();
                    celulaAnimator.SetTrigger("END");
                }
                if (pickObjectsScript.tonico_pckd)
                {
                    Animator boxAnim = GameObject.Find("Caixa_Garagem").GetComponent<Animator>();
                    boxAnim.SetTrigger("Maintain");
                }
                break;
            case "Quarto":
                if (pickObjectsScript.chave_cacto_1_pckd)
                {
                    //Destroy(GameObject.Find("Tampa"));
                    Destroy(GameObject.Find("Chave"));
                    Animator caixaAnim = GameObject.Find("Caixa_de_madeira").GetComponent<Animator>();
                    caixaAnim.SetTrigger("END");
                }
                if (clickScript.radioTaped)
                {
                    GameObject gazela = GameObject.FindGameObjectWithTag("Gazela");
                    GameObject[] propsGazela = GameObject.FindGameObjectsWithTag("tuboluz");
                    for(int i = 0; i < propsGazela.Length; i++)
                    {
                        Destroy(propsGazela[i]);
                    }
                    Destroy(gazela);
                }
                break;
            case "Laboratorio":
                Debug.Log("ok");
                for(int i = 0; i < 4; i ++)
                {
                    if (puzzleScript.cactosPlaced[i])
                    {
                        GameObject cacto = GameObject.Find("Espaço_Cacto_" + (i + 1)).transform.GetChild(0).gameObject;
                        cacto.SetActive(true);
                    }
                }
                if (puzzleScript.celulaPlaced)
                {
                    GameObject celula = GameObject.Find("Gerador_Lab").transform.GetChild(10).gameObject;
                    celula.SetActive(true);
                    // adicionar efeito especial pra mostrar que a celula ta cheia
                }

                break;

        }
    }


}
