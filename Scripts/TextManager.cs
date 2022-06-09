using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{
    [SerializeField] private Sprite[] characterPortraits;
    [SerializeField] private Image activePortrait;
    [SerializeField] private TextMeshProUGUI nameText;
    private Animator portraitAnim;

    public DialogScriptableObject[] textDataPortuguese;
    public GameObject textBox;
    private SoundManager soundManagerScript;
    private Text textBoxText;
    private int textIndexer = 0;
    private int textPage = 0;
    public float delayTextTime = 0.08f;
    private bool writingText = false;
    private bool dialogProgressionLocal;

    private void Start()
    {
        soundManagerScript = gameObject.GetComponent<SoundManager>();
        textBox = GameObject.FindGameObjectWithTag("TextBox");
        textBoxText = textBox.transform.GetChild(0).GetComponent<Text>();
        portraitAnim = textBox.transform.GetChild(2).gameObject.GetComponent<Animator>();
        nameText = textBox.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        activePortrait = textBox.transform.GetChild(2).GetComponent<Image>();
        textBox.SetActive(false);
        resetDialogs();

        DontDestroyOnLoad(gameObject);
    }

    private void resetDialogs() // isso serve apenas pra testes enquanto o jogo não ta pronto
    {
        for(int i = 0; i < textDataPortuguese.Length; i++)
        {
            textDataPortuguese[i].dialogActive = 0;
        }
    }
    public void TextScroller(int textIndex, bool dialogProgression)
    {
        if (textIndex != 0) // textIndex = 0 serve pra representar que o player não clicou em um objeto interagivel
        {
            textIndexer = textIndex;
            dialogProgressionLocal = dialogProgression;
        }
        if ((textIndex == 0) && (!textBox.activeInHierarchy))
            return;

        textBox.SetActive(true);
        portraitChanger();
        if (textDataPortuguese[textIndexer].dialogActive == 0)
        {
            if (textPage < textDataPortuguese[textIndexer].DialogAmount.Count)
            {
                if ((writingText == false))
                {
                    writingText = true;
                    soundManagerScript.playTypingSound(true);
                    StartCoroutine("TypeWriterEffect");
                }
                else
                {
                    textBoxText.text = textDataPortuguese[textIndexer].DialogAmount[textPage].DialogBox;
                    writingText = false;
                    soundManagerScript.playTypingSound(false);
                }
            }
            else
            {
                textPage = 0;
                if (dialogProgressionLocal && textDataPortuguese[textIndexer].dialogActive < 3)
                    textDataPortuguese[textIndexer].dialogActive++;
                textBox.SetActive(false);
            }
        }
        else
        {
            if (textDataPortuguese[textIndexer].dialogActive == 1)
            {
                if (textPage < textDataPortuguese[textIndexer].DialogAmount2.Count)
                {
                    if ((writingText == false))
                    {
                        writingText = true;
                        soundManagerScript.playTypingSound(true);
                        StartCoroutine("TypeWriterEffect");
                    }
                    else
                    {
                        textBoxText.text = textDataPortuguese[textIndexer].DialogAmount2[textPage].DialogBox;
                        writingText = false;
                        soundManagerScript.playTypingSound(false);
                    }
                }
                else
                {
                    textPage = 0;
                    if (dialogProgressionLocal && textDataPortuguese[textIndexer].dialogActive < 3)
                        textDataPortuguese[textIndexer].dialogActive++;
                    textBox.SetActive(false);
                }
            }
            else
                 if (textPage < textDataPortuguese[textIndexer].DialogAmount3.Count)
            {
                if ((writingText == false))
                {
                    writingText = true;
                    soundManagerScript.playTypingSound(true);
                    StartCoroutine("TypeWriterEffect");
                }
                else
                {
                    textBoxText.text = textDataPortuguese[textIndexer].DialogAmount3[textPage].DialogBox;
                    writingText = false;
                    soundManagerScript.playTypingSound(false);
                }
            }
            else
            {
                textPage = 0;
                if (dialogProgressionLocal && textDataPortuguese[textIndexer].dialogActive < 3)
                    textDataPortuguese[textIndexer].dialogActive++;
                textBox.SetActive(false);
            }
        }
        
    }
    IEnumerator TypeWriterEffect()
    {
        string eventText;

        switch (textDataPortuguese[textIndexer].dialogActive)
        {
            case 0:
                eventText = textDataPortuguese[textIndexer].DialogAmount[textPage].DialogBox;
                break;
            case 1:
                eventText = textDataPortuguese[textIndexer].DialogAmount2[textPage].DialogBox;
                break;
            default:
                eventText = textDataPortuguese[textIndexer].DialogAmount3[textPage].DialogBox;
                break;

        }

        for (int i = 0; i < eventText.Length; i++)
        {
            if (writingText == false)
            {
                break;
            }

            textBoxText.text = eventText.Substring(0, i + 1);
            yield return new WaitForSeconds(delayTextTime);
        }
        textPage++;
        writingText = false;
        soundManagerScript.playTypingSound(false);
    }

    private void portraitChanger ()
    {
        List<int> portraitIndex = new List<int>();
        int actualIndex;

        if (textDataPortuguese[textIndexer].dialogActive == 0)
            portraitIndex = textDataPortuguese[textIndexer].characterIndex0;
        else
        {
            if (textDataPortuguese[textIndexer].dialogActive == 1)
                portraitIndex = textDataPortuguese[textIndexer].characterIndex1;
            else
                portraitIndex = textDataPortuguese[textIndexer].characterIndex2;
        }

        if (textPage < portraitIndex.Count)
            actualIndex = portraitIndex[textPage];
        else
            actualIndex = 0;

        //activePortrait.sprite = characterPortraits[actualIndex];

        switch (actualIndex)
        {
            case 0:
                nameText.text = "Digueliro";
                portraitAnim.SetTrigger("Dig_Trig");
                break;
            case 1:
                nameText.text = "Bartolomeu";
                portraitAnim.SetTrigger("Bart_Trig");
                break;
            case 2:
                nameText.text = "Rubens";
                portraitAnim.SetTrigger("Rub_Trig");
                break;
            case 5:
                nameText.text = "Gazela";
                portraitAnim.SetTrigger("Gaz_Trig");
                break;
        }
    }
}
