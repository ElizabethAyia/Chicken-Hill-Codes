using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         PublicSavers publicSaverScript = GameObject.FindGameObjectWithTag("Public Saver").GetComponent<PublicSavers>();
         if (publicSaverScript.gameInPortuguese)
             GameObject.Find("Creditos_BR").transform.GetChild(0).gameObject.SetActive(true);
         else
             GameObject.Find("Creditos_EN").transform.GetChild(0).gameObject.SetActive(true);
        
    }
}
