using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHoverOutline : MonoBehaviour
{
    //Outline outlineScript;

    private MeshRenderer objMeshRenderer;
    private Material standartMaterial;
    public Material glowingMaterial;

    private void Awake()
    {
        objMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        standartMaterial = gameObject.GetComponent<MeshRenderer>().material;
        //outlineScript = gameObject.GetComponent<Outline>();
        //outlineScript.enabled = false;
    }
    void OnMouseEnter()
    {
        //outlineScript.enabled = true;
        objMeshRenderer.material = glowingMaterial;
        
    }
    void OnMouseExit()
    {
        //outlineScript.enabled = false;
        objMeshRenderer.material = standartMaterial;
    }
}
