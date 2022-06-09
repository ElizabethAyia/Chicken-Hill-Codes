using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using System.Linq;
using System;

public class MainMenuScript : MonoBehaviour
{
    PublicSavers publicSavers;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] Transitions transitionScript;
    [SerializeField] AudioMixer masterMixer;

    //Static text for language purposes//
    //[SerializeField] Text[] staticTexts = new Text[9];
    [SerializeField] TextMeshProUGUI[] staticTexts = new TextMeshProUGUI[9];
    // 0 - volume 1 - resolução 2 - tela cheia 3 - qualidade 4 - idioma
    // 5 - Começar 6 - Opções 7 - Sair 8 - voltar

    Resolution[] resolutions;
    int currentResolutionIndex = 0;
    int volumeIndex = 20;

    private TextMeshProUGUI resolutionText;
    private TextMeshProUGUI qualityText;
    private TextMeshProUGUI languageText;
    private TextMeshProUGUI volumeText;
    private TextMeshProUGUI fullScreenText;
    //private bool isFullScreen = false;

    private int volumeSet;
    private void Start()
    {
        publicSavers = GameObject.FindGameObjectWithTag("Public Saver").GetComponent<PublicSavers>();
        resolutionText = GameObject.Find("Resolution Changer").GetComponent<TextMeshProUGUI>();
        qualityText = GameObject.Find("Quality Changer").GetComponent<TextMeshProUGUI>();
        languageText = GameObject.Find("Language Changer").GetComponent<TextMeshProUGUI>();
        volumeText = GameObject.Find("Volume Changer").GetComponent<TextMeshProUGUI>();
        fullScreenText = GameObject.Find("Fullscreen Changer").GetComponent<TextMeshProUGUI>();
        transitionScript.startMenu();

        startResolutionSettings();
        optionsMenu.SetActive(false);
    }

    private void startResolutionSettings()
    {
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == 60).ToArray();

        //List<string> resolutionOptions = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionText.text = resolutions[currentResolutionIndex].width.ToString() + "x" + resolutions[currentResolutionIndex].height.ToString();
    }

    //começar o jogo
    public void ClickStartButtom()
    {
        publicSavers.qualitySet = qualityText.text;
        publicSavers.volumeSet = volumeSet;
        if (publicSavers.gameInPortuguese)
            transitionScript.SceneTransition("Sequencia_Ini_BR"); // trocar isso no futuro por algo mais elaborado, como uma cena de introdução
        else
            transitionScript.SceneTransition("Sequencia_Ini_EN");
    }

    // entrar no menu de opções
    public void ClickOptionsButtom()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // voltar sem salvar do menu de opções
    public void ClickBackOptionsMenu()
    {

        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // sair do jogo
    public void ClickExitButtom()
    {
        Application.Quit();
    }

    // som
    public void VolumeChanger(bool lowering)
    {
        if (lowering)
        {
            switch (volumeIndex)
            {
                case (20):
                    volumeIndex = 10;
                    volumeText.text = "90%";
                    break;
                case (10):
                    volumeIndex = 0;
                    volumeText.text = "80%";
                    break;
                case (0):
                    volumeIndex = -10;
                    volumeText.text = "70%";
                    break;
                case (-10):
                    volumeIndex = -20;
                    volumeText.text = "60%";
                    break;
                case (-20):
                    volumeIndex = -30;
                    volumeText.text = "50%";
                    break;
                case (-30):
                    volumeIndex = -40;
                    volumeText.text = "40%";
                    break;
                case (-40):
                    volumeIndex = -50;
                    volumeText.text = "30%";
                    break;
                case (-50):
                    volumeIndex = -60;
                    volumeText.text = "20%";
                    break;
                case (-60):
                    volumeIndex = -70;
                    volumeText.text = "10%";
                    break;
                case (-70):
                    volumeIndex = -160;
                    volumeText.text = "0%";
                    break;
            }
        }
        else
        {
            switch (volumeIndex)
            {
                case (10):
                    volumeIndex = 20;
                    volumeText.text = "100%";
                    break;
                case (0):
                    volumeIndex = 10;
                    volumeText.text = "90%";
                    break;
                case (-10):
                    volumeIndex = 0;
                    volumeText.text = "80%";
                    break;
                case (-20):
                    volumeIndex = -10;
                    volumeText.text = "70%";
                    break;
                case (-30):
                    volumeIndex = -20;
                    volumeText.text = "60%";
                    break;
                case (-40):
                    volumeIndex = -30;
                    volumeText.text = "50%";
                    break;
                case (-50):
                    volumeIndex = -40;
                    volumeText.text = "40%";
                    break;
                case (-60):
                    volumeIndex = -50;
                    volumeText.text = "30%";
                    break;
                case (-70):
                    volumeIndex = -60;
                    volumeText.text = "20%";
                    break;
                case (-160):
                    volumeIndex = -70;
                    volumeText.text = "10%";
                    break;
            }
        }

        masterMixer.SetFloat("MasterVolume", (volumeIndex-20f)/3.3f);
        volumeSet = volumeIndex;
    }

    //resolução de tela
    public void screenResolutionChanger(bool lowering)
    {
        if (lowering)
        {
            if (currentResolutionIndex > 0)
                currentResolutionIndex--;
        }
        else
        {
            if (currentResolutionIndex < resolutions.Length - 1)
                currentResolutionIndex++;
        }

        resolutionText.text = resolutions[currentResolutionIndex].width.ToString() + "x" + resolutions[currentResolutionIndex].height.ToString();
        Screen.SetResolution(resolutions[currentResolutionIndex].width, resolutions[currentResolutionIndex].height, Screen.fullScreen);
    }

    //qualidade gráfica
    public void graphicQualityChanger(bool lowering)
    {
        if (lowering)
        {
            switch(qualityText.text)
            {
                case "Baixo":
                    break;
                case "Médio":
                    qualityText.text = "Baixo";
                    QualitySettings.SetQualityLevel(2);
                    break;
                case "Alto":
                    qualityText.text = "Médio";
                    QualitySettings.SetQualityLevel(1);
                    break;
                case "Low":
                    break;
                case "Medium":
                    qualityText.text = "Low";
                    QualitySettings.SetQualityLevel(2);
                    break;
                case "High":
                    qualityText.text = "Medium";
                    QualitySettings.SetQualityLevel(1);
                    break;
            }
        }
        else
        {
            switch (qualityText.text)
            {
                case "Baixo":
                    qualityText.text = "Médio";
                    QualitySettings.SetQualityLevel(1);
                    break;
                case "Médio":
                    qualityText.text = "Alto";
                    QualitySettings.SetQualityLevel(0);
                    break;
                case "Alto":
                    break;
                case "Low":
                    qualityText.text = "Medium";
                    QualitySettings.SetQualityLevel(1);
                    break;
                case "Medium":
                    qualityText.text = "High";
                    QualitySettings.SetQualityLevel(0);
                    break;
                case "High":
                    break;
            }
        }
    }

    public void toggleFullScreen ()
    {
        if (Screen.fullScreen == true)
            Screen.fullScreen = false;
        else
            Screen.fullScreen = true;

        if (publicSavers.gameInPortuguese)
        {
            if (Screen.fullScreen)
                fullScreenText.text = "NÃO";
            else
                fullScreenText.text = "SIM";
        }
        else
        {
            if (Screen.fullScreen)
                fullScreenText.text = "NO";
            else
                fullScreenText.text = "YES";
        }

        Debug.Log(Screen.fullScreen.ToString());  
    }

    //linguagem
    public void languageChanged ()
    {
        // 0 - volume 1 - resolução 2 - tela cheia 3 - qualidade 4 - idioma
        // 5 - Começar 6 - Opções 7 - Sair 8 - Voltar
        if (languageText.text == "EN")
        {
            publicSavers.gameInPortuguese = true;
            languageText.text = "BR";
            staticTexts[0].text = "Volume";
            staticTexts[1].text = "Resolução";
            staticTexts[2].text = "Tela Cheia";
            staticTexts[3].text = "Qualidade";
            staticTexts[4].text = "Idioma"; 
            staticTexts[5].text = "Começar";
            staticTexts[6].text = "Opções";
            staticTexts[7].text = "Sair";
            staticTexts[8].text = "Voltar";

            if (fullScreenText.text == "NO")
                fullScreenText.text = "NÂO";
            else
                fullScreenText.text = "SIM";

            switch (qualityText.text)
            {
                case "Low":
                    qualityText.text = "Baixo";
                    break;
                case "Medium":
                    qualityText.text = "Médio";
                    break;
                case "High":
                    qualityText.text = "Alto";
                    break;
            }
        }
        else
        {
            publicSavers.gameInPortuguese = false;
            languageText.text = "EN";
            staticTexts[0].text = "Volume";
            staticTexts[1].text = "Resolution";
            staticTexts[2].text = "Full Screen";
            staticTexts[3].text = "Quality";
            staticTexts[4].text = "Language"; 
            staticTexts[5].text = "Start";
            staticTexts[6].text = "Options";
            staticTexts[7].text = "Exit"; 
            staticTexts[8].text = "Back";

            if (fullScreenText.text == "NÃO")
                fullScreenText.text = "NO";
            else
                fullScreenText.text = "YES";

            switch (qualityText.text)
            {
                case "Baixo":
                    qualityText.text = "Low";
                    break;
                case "Médio":
                    qualityText.text = "Medium";
                    break;
                case "Alto":
                    qualityText.text = "High";
                    break;
            }
        }
    }

}
