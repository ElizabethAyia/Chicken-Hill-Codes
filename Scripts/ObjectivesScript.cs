using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesScript : MonoBehaviour
{
    [SerializeField] Text objectiveText;
    [SerializeField] DialogScriptableObject objectiveScriptableObject;
    [SerializeField] DialogScriptableObject objectiveScriptableObjectEnglish;
    [SerializeField] PublicSavers publicSaverscript;

    private void Start()
    {
        publicSaverscript = GameObject.FindGameObjectWithTag("Public Saver").GetComponent<PublicSavers>();
    }
    public void objectiveChanger (int objectiveIndex)
    {
      /*  switch(objectiveIndex)
        {
            case (0):
                if (publicSaverscript.gameInPortuguese)
                    objectiveText.text = objectiveScriptableObject.DialogAmount[0].DialogBox;
                else
                    objectiveText.text = objectiveScriptableObjectEnglish.DialogAmount[0].DialogBox;
                break;
            case (1):
                if (publicSaverscript.gameInPortuguese)
                    objectiveText.text = objectiveScriptableObject.DialogAmount[1].DialogBox;
                else
                    objectiveText.text = objectiveScriptableObjectEnglish.DialogAmount[1].DialogBox;
                break;
        } */
    }

    public void objectiveLanguageChanger()
    {
        if (publicSaverscript.gameInPortuguese)
        {
            for (int i = 0; i < objectiveScriptableObject.DialogAmount.Count; i++)
            {
                if (objectiveText.text == objectiveScriptableObjectEnglish.DialogAmount[i].DialogBox)
                    objectiveText.text = objectiveScriptableObject.DialogAmount[i].DialogBox;
            }
        }
        else
        {
            for (int i = 0; i < objectiveScriptableObject.DialogAmount.Count; i++)
            {

                if (objectiveText.text == objectiveScriptableObject.DialogAmount[i].DialogBox)
                    objectiveText.text = objectiveScriptableObjectEnglish.DialogAmount[i].DialogBox;
            }
        }

    }
}
