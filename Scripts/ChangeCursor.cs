using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCursor : MonoBehaviour
{

    public Texture2D Dentro;
    public Texture2D Fora;
    CursorMode cursorMode = CursorMode.Auto;
    public bool DentroDoCoisa;
    private CameraMover cameraMoverScript;

    private void Start()
    {
        cameraMoverScript = gameObject.GetComponent<CameraMover>();
    }
    void Update()
    {
        if (cameraMoverScript.mainCameraActive)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Cursor.SetCursor(Dentro, Vector2.zero, cursorMode);
            }
            else
            {
                Cursor.SetCursor(Fora, Vector2.zero, cursorMode);
            }

        }
        else
        {
            RaycastHit hit;
            Ray ray;
            switch (cameraMoverScript.cameraIndex)
            {
                case 1:
                    ray = cameraMoverScript.altCameras[0].ScreenPointToRay(Input.mousePosition);
                    break;
                case 2:
                    ray = cameraMoverScript.altCameras[1].ScreenPointToRay(Input.mousePosition);
                    break;
                case 3:
                    ray = cameraMoverScript.altCameras[2].ScreenPointToRay(Input.mousePosition);
                    break;
                case 4:
                    ray = cameraMoverScript.altCameras[3].ScreenPointToRay(Input.mousePosition);
                    break;
                default:
                    Debug.Log("CAMERA SWITCH ERROR, NO CAMERA SELECTED");
                    return;
            }

            
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                Cursor.SetCursor(Dentro, Vector2.zero, cursorMode);
            }
            else
            {
                Cursor.SetCursor(Fora, Vector2.zero, cursorMode);
            }

        }

    }
}

