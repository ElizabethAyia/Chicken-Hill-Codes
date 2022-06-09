using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMover : MonoBehaviour
{
    public int cameraIndex = 0;

    [SerializeField]private GameObject mainCamera;
    [SerializeField]private GameObject cameraArm;

    private bool doOnce = false;
    private bool doOnce2 = false;
    public bool mainCameraActive = true;

    public Camera[] altCameras = new Camera[4];
    private AudioListener[] cameraListeners = new AudioListener[4];

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Dispensa")
        {
            if (!doOnce)
            {
                findPuzzlesPositions();
                doOnce = true;
            }
        }
        else
            doOnce = false;
    }

    private void findPuzzlesPositions()
    {
        cameraArm = GameObject.FindGameObjectWithTag("Camera Arm");
        mainCamera = cameraArm.transform.GetChild(0).gameObject;

        for (int i = 1; i < 5; i++)
      {
           altCameras[i - 1] = GameObject.Find("PuzzlePosition" + i).GetComponent<Camera>();
           cameraListeners[i - 1] = altCameras[i - 1].gameObject.GetComponent<AudioListener>();
           cameraListeners[i - 1].enabled = false;
       }
    }

    public void switchCamera(string positionName)
    {
        if (positionName == "Parede_Out")
        {
            cameraIndex = 0;
            mainCamera.SetActive(true);
            mainCameraActive = true;
            for(int i = 0; i < 4; i++)
            {
                altCameras[i].enabled = false;
                cameraListeners[i].enabled = false;
                camerasHitBoxes(true);
            }
        }
        else
        {
            for (int i = 1; i < 5; i++)
            {
                if (positionName == "Puzzle_" + i)
                {
                    cameraIndex = i;
                    mainCamera.SetActive(false);
                    altCameras[i - 1].enabled = true;
                    cameraListeners[i - 1].enabled = true;
                    mainCameraActive = false;
                    camerasHitBoxes(false);
                }
            }
        }

    }

    private void camerasHitBoxes (bool turnOn)
    {
        for(int i = 0; i < 4; i++)
        {
            GameObject.Find("Puzzle_" + (i + 1)).GetComponent<BoxCollider>().enabled = turnOn;
        }
    }

}
