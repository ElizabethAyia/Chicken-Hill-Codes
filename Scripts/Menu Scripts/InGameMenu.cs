using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class InGameMenu : MonoBehaviour
{

    private PublicSavers publicSavers;
    [SerializeField] ObjectivesScript objectiveScript;

    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject quittingMenu;
    [SerializeField] AudioMixer masterMixer;

    //Static text for language purposes//
    [SerializeField] TextMeshProUGUI[] staticTexts = new TextMeshProUGUI[12];
    // 0 - volume 1 - resolução 2 - tela cheia 3 - qualidade 4 - idioma
    // 5 - Continuar 6 - Opções 7 - Sair 8 - voltar 9 - Are you sure
    // 10 - yes 11 - no

    Resolution[] resolutions;
    int currentResolutionIndex = 0;

    [SerializeField] private TextMeshProUGUI resolutionText;
    [SerializeField] private TextMeshProUGUI qualityText;
    [SerializeField] private TextMeshProUGUI languageText;
    [SerializeField] private TextMeshProUGUI volumeText;
    [SerializeField] private TextMeshProUGUI fullScreenText;

    private int volumeIndex;
    private int volumeSet;

    private void Start()
    {
        publicSavers = GameObject.FindGameObjectWithTag("Public Saver").GetComponent<PublicSavers>();
        resolutionText = GameObject.Find("Resolution Changer").GetComponent<TextMeshProUGUI>();
        qualityText = GameObject.Find("Quality Changer").GetComponent<TextMeshProUGUI>();
        languageText = GameObject.Find("Language Changer").GetComponent<TextMeshProUGUI>();
        volumeText = GameObject.Find("Volume Changer").GetComponent<TextMeshProUGUI>();
        fullScreenText = GameObject.Find("Fullscreen Changer").GetComponent<TextMeshProUGUI>();

        startResolutionSettings();
        loadMenuSettings();
        quittingMenu.SetActive(false);
    }

    private void loadMenuSettings()
    {
        volumeSet = publicSavers.volumeSet;
        volumeIndex = volumeSet;
        VolumeChanger(true);
        VolumeChanger(false);
        qualityText.text = publicSavers.qualitySet;
        languageChanged();

    }
    private void startResolutionSettings()
    {
        resolutions = Screen.resolutions;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionText.text = resolutions[currentResolutionIndex].width.ToString() + "x" + resolutions[currentResolutionIndex].height.ToString();

        if (publicSavers.gameInPortuguese)
            languageText.text = "PT";
        else
            languageText.text = "EN";
        languageChanged();
    }

    //começar o jogo
    public void ClickStartButtom()
    {
        inGameMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }

    // entrar no menu de opções
    public void ClickOptionsButtom()
    {
        inGameMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    // voltar do menu de opções
    public void ClickBackOptionsMenu()
    {

        inGameMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    // sair do jogo
    public void ClickExitButtom()
    {
        quittingMenu.SetActive(true);
    }

    public void ClickTrueExitButtom (bool isQuitting)
    {
        if (isQuitting)
            Application.Quit();
        else
            quittingMenu.SetActive(false);
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
                    volumeIndex = -80;
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
                case (-80):
                    volumeIndex = -70;
                    volumeText.text = "10%";
                    break;
            }
        }

        masterMixer.SetFloat("MasterVolume", volumeIndex);
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
    }

    //qualidade gráfica
    public void graphicQualityChanger(bool lowering)
    {
        if (lowering)
        {
            switch (qualityText.text)
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

    public void toggleFullScreen()
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
    }

    //linguagem
    public void languageChanged()
    {
        // 0 - volume 1 - resolução 2 - tela cheia 3 - qualidade 4 - idioma
        // 5 - Começar 6 - Opções 7 - Sair 8 - Voltar
        if (languageText.text == "EN")
        {
            publicSavers.gameInPortuguese = true;
            languageText.text = "PT";
            staticTexts[0].text = "Volume";
            staticTexts[1].text = "Resolução";
            staticTexts[2].text = "Tela Cheia";
            staticTexts[3].text = "Qualidade";
            staticTexts[4].text = "Idioma";
            staticTexts[5].text = "Continuar";
            staticTexts[6].text = "Opções";
            staticTexts[7].text = "Sair do jogo";
            staticTexts[8].text = "Voltar";
            staticTexts[9].text = "Você tem certeza?";
            staticTexts[10].text = "Sim";
            staticTexts[11].text = "Não";

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
            staticTexts[5].text = "Resume";
            staticTexts[6].text = "Options";
            staticTexts[7].text = "Quit game";
            staticTexts[8].text = "Back";
            staticTexts[9].text = "Are you sure?";
            staticTexts[10].text = "Yes";
            staticTexts[11].text = "No";

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

        //objectiveScript.objectiveLanguageChanger();
    }
}
