using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationScript : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float maxRotationLeft = 80f;
    [SerializeField] private float maxRotationRight = 270f;
    [SerializeField] GameObject inGameMenu;
    private bool dragging = false;

    private void Start()
    {
       inGameMenu = GameObject.FindGameObjectWithTag("In Game Menu");
    }
    private void Update()
    {
        if (inGameMenu.transform.GetChild(0).gameObject.activeInHierarchy || inGameMenu.transform.GetChild(1).gameObject.activeInHierarchy)
           return;

        if (Input.GetMouseButtonDown(1))
            dragging = true;
        if (Input.GetMouseButtonUp(1))
            dragging = false;

        if (dragging)
        {
           // float mouseInputY = Input.GetAxis("Mouse Y");
            float mouseInputX = Input.GetAxis("Mouse X");
            transform.Rotate(0, mouseInputX * (rotationSpeed * 1920 / Screen.width * Time.deltaTime), 0); // no modo de build essa linha pode dar problema, caso de substitua pela debaixo
            // transform.Rotate(0, mouseInputX * (rotationSpeed * Time.deltaTime), 0);
            cameraRotationLimits();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                transform.rotation = Quaternion.identity;
                //StartCoroutine("RotateBack");
            }
        }
    }

    private void cameraRotationLimits()
    {
        if ((transform.rotation.eulerAngles.y >= maxRotationLeft) && (transform.rotation.eulerAngles.y < 170f))
            transform.rotation = Quaternion.Euler(0, maxRotationLeft, 0);
        if ((transform.rotation.eulerAngles.y <= maxRotationRight) && (transform.rotation.eulerAngles.y >= 170f))
            transform.rotation = Quaternion.Euler(0, maxRotationRight, 0); 
    }
    IEnumerator RotateBack() // isso serve pra rotacionar o cenário devolta (ñão está em uso atualmente)
    {
        do
        {
            if (dragging)
                break;
            if (transform.rotation.eulerAngles.y < (180))
                transform.Rotate(0, -1 * rotationSpeed * 2 * Time.deltaTime, 0);
            else
                transform.Rotate(0, 1 * rotationSpeed * 2 * Time.deltaTime, 0);

            yield return null;

            if ((transform.rotation.eulerAngles.y <= 30 && transform.rotation.eulerAngles.y >= 0) || (transform.rotation.eulerAngles.y >= 330 && transform.rotation.eulerAngles.y < 366))
            {
                transform.rotation = Quaternion.identity;
                break;
            }

        }
        while (transform.rotation.eulerAngles.y != 0);

        yield return new WaitForSeconds(0);
    }

}
