using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuzzlesHandler : MonoBehaviour
{
    private PickUpableObject pickupScript;
    private CameraMover cameraMoverScript;


    // ================================ PUZZLES DAS LAMPADAS / ALAVANCAS ======================================== //
    [SerializeField] private Light[] lights = new Light[9];
    // linha 1 -> 0 = Vermelha 1 = Azul 2 = Verde
    // linha 2 -> 3 = Verde 4 = Azul 5 = Vermelha
    // linha 3 -> 6 = Vermelha 7 = Verde 8 = Azul
    [SerializeField] private GameObject[] lamps = new GameObject[9];
    [SerializeField] private MeshRenderer[] lampsMesh = new MeshRenderer[9];
    [SerializeField] private Material[] lightsLitMaterial = new Material[3];
    [SerializeField] private Material[] lightsUnlitMaterial = new Material[9];
    [SerializeField] private bool findLights = true;
    [SerializeField] private int puzzleIndex = 0;
    public int puzzleCompletion = 0;
    private bool dontRepeat = false;
    // ================================ ALAVANCAS GARAGEM ===================================================== //
    private bool findLevers = true;
    private Animator[] leverAnimators = new Animator[3]; // 0 = Vermelho, 1 = Verde, 2 = Azul
    private Animator generatorAnimator;
    private float leverTimer = 1f;
    private MeshRenderer[] garagemLuzes = new MeshRenderer[3];

    // ============================================================================================= //
    [SerializeField] Animator[] doorAnimators = new Animator[4];
    [HideInInspector] public bool[] puzzlesDispensaComplete = new bool[4];
    private MeshRenderer[] puzzleLockedLight = new MeshRenderer[4];
    [HideInInspector] public BoxCollider[] cactosColliders = new BoxCollider[4];
    // ===================================== PUZZLE 1 / CHAVONA =============================================== //

    // ======================================================================================================== //
    // ===================================== PUZZLE 2 / CAIXA PI ============================================== //

    [SerializeField] private Material normalGrayMaterial;
    [SerializeField]private MeshRenderer[] keyboardLights = new MeshRenderer[3];
    private string password = "";
    private int keyboardIndex = 0;
    private Animator puzzle2Animator;
    private bool findthings = false;
    private bool animationRunning = false;
    // ======================================================================================================= //
    // =========================================== PUZZLE 3 / LUZES ========================================== //
    [SerializeField] private MeshRenderer[] painel_puzzle_3 = new MeshRenderer[27];
    [SerializeField] private bool[] lightsOn = new bool[27];
    // ======================================================================================================= //
    // =========================================== PUZZLE 4 / CANOS ========================================== //
    private Animator[] canosAnim = new Animator[3];
    private int[] canosPos = new int[3];
    private GameObject[] canos = new GameObject[3];
    private MeshRenderer puzzle4LockedLight;
    // ======================================================================================================= //
    // =========================================== LABORATORIO =============================================== //
    [HideInInspector] public bool[] cactosPlaced = new bool[4];
    public bool celulaPlaced = false;
    public bool celulaEnergized = false;
    public bool labComplete = false;
    private void Start()
    {
        pickupScript = gameObject.GetComponent<PickUpableObject>();
        cameraMoverScript = gameObject.GetComponent<CameraMover>();
        for(int i = 0; i < 3; i++)
        {
            canosPos[i] = 2;
        }
        for(int i = 0; i < 4; i++)
        {
            cactosPlaced[i] = false;
        }
    }

    private void Update()
    {

        if (SceneManager.GetActiveScene().name == "Sala")
        {
            if (findLights)
            {
                findTheLights();
                findLights = false;
            }
        }
        else
            findLights = true;

        if (SceneManager.GetActiveScene().name == "Dispensa")
        {
            if (findthings)
            {
                for (int i = 0; i < 3; i++)
                {
                    canosPos[i] = 2;
                }
                findDispensaThings();
                findthings = false;
            }
        }
        else
            findthings = true;

        if (SceneManager.GetActiveScene().name == "Garagem")
        {

            
            if (findLevers)
            {
                findGarageLevers();
                findLevers = false;
            }

            if (puzzleCompletion == 1)
                garagemLuzes[0].material = lightsLitMaterial[0];

            else if (puzzleCompletion == 2)
            {
                garagemLuzes[0].material = lightsLitMaterial[0];
                garagemLuzes[1].material = lightsLitMaterial[2];
            }
            else if (puzzleCompletion == 3)
            {
                garagemLuzes[0].material = lightsLitMaterial[0];
                garagemLuzes[1].material = lightsLitMaterial[2];
                garagemLuzes[2].material = lightsLitMaterial[1];
            }

        }

        else
            findLevers = true;

        if (leverTimer > 0)
            leverTimer -= Time.deltaTime;

       
}
    public void findTheLights()
    {
        for (int i = 1; i < 10; i++)
        {
            lights[i - 1] = GameObject.Find("Luz_" + i).GetComponent<Light>();
            lamps[i - 1] = GameObject.Find("Lampada_" + i);
            lampsMesh[i - 1] = lamps[i - 1].GetComponent<MeshRenderer>();
            if (i < 4)
                lightsLitMaterial[i - 1] = lampsMesh[i - 1].material;
            lampsMesh[i - 1].material = lightsUnlitMaterial[i - 1];
        }
        allLightsOff();
    }
    public void clickFuzeBox()
    {
        if (!dontRepeat)
            StartCoroutine("lightsBlink");
    }
    IEnumerator lightsBlink()
    {
        // linha 1 -> 0 = Vermelha 1 = Azul 2 = Verde
        // linha 2 -> 3 = Verde 4 = Azul 5 = Vermelha
        // linha 3 -> 6 = Vermelha 7 = Verde 8 = Azul
        // vermelho -> 0,1,2
        // azul     -> 3,4,5
        // verde    -> 6,7,8
        dontRepeat = true;

        lights[1].enabled = true;
        lights[5].enabled = true;
        lights[7].enabled = true;
        lampsMesh[1].material = lightsLitMaterial[1];
        lampsMesh[5].material = lightsLitMaterial[0];
        lampsMesh[7].material = lightsLitMaterial[2];
        yield return new WaitForSeconds(1f);
        allLightsOff();
        lights[2].enabled = true;
        lights[4].enabled = true;
        lights[6].enabled = true;
        lampsMesh[2].material = lightsLitMaterial[2];
        lampsMesh[4].material = lightsLitMaterial[2];
        lampsMesh[6].material = lightsLitMaterial[1];
        yield return new WaitForSeconds(1f);
        allLightsOff();
        lights[0].enabled = true;
        lights[3].enabled = true;
        lights[8].enabled = true;
        lampsMesh[0].material = lightsLitMaterial[0];
        lampsMesh[3].material = lightsLitMaterial[1];
        lampsMesh[8].material = lightsLitMaterial[0];
        yield return new WaitForSeconds(1f);
        allLightsOff();
        dontRepeat = false;
    }

    private void allLightsOff()
    {
        for (int i = 0; i < 9; i++)
        {
            lights[i].enabled = false;
            lampsMesh[i].material = lightsUnlitMaterial[i];
        }
    }

    private void findGarageLevers()
    {
        leverAnimators[0] = GameObject.Find("Alavanca_Vermelha").transform.GetChild(0).GetComponent<Animator>();
        leverAnimators[1] = GameObject.Find("Alavanca_Verde").transform.GetChild(0).GetComponent<Animator>();
        leverAnimators[2] = GameObject.Find("Alavanca_Azul").transform.GetChild(0).GetComponent<Animator>();
        generatorAnimator = GameObject.Find("Gerador").GetComponent<Animator>();
        garagemLuzes[0] = GameObject.Find("Cabo_1").GetComponent<MeshRenderer>();
        garagemLuzes[1] = GameObject.Find("Cabo_2").GetComponent<MeshRenderer>();
        garagemLuzes[2] = GameObject.Find("Cabo_3").GetComponent<MeshRenderer>();

    }

    public void ClickLever(string leverColor)
    {
        // ordem 1 -> azul,vermelho,verde
        // ordem 2 -> verde,verde,azul
        // ordem 3 -> vermelho,azul,vermelho

        if (leverTimer >= 0)
            return;

        switch (leverColor)
        {
            case "Red":
                leverAnimators[0].SetTrigger("Push");

                if (puzzleCompletion == 0 && puzzleIndex == 1)
                    puzzleIndex++;
                else
                {
                    if (puzzleCompletion == 2 && (puzzleIndex == 0 || puzzleIndex == 2))
                    {
                        if (puzzleIndex == 0)
                            puzzleIndex++;
                        if (puzzleIndex == 2)
                        {
                            puzzleIndex = 0;
                            puzzleCompletion = 3;
                            Debug.Log("Puzzle completo");
                        }
                    }
                    else
                        puzzleIndex = 0; // falha no puzzle
                }
                break;
            case "Blue":
                leverAnimators[2].SetTrigger("Push");

                if (puzzleCompletion == 0 && puzzleIndex == 0)
                    puzzleIndex++;
                else
                {
                    if (puzzleCompletion == 1 && puzzleIndex == 2)
                    {
                        puzzleIndex = 0;
                        puzzleCompletion = 2;
                    }
                    else
                    {
                        if (puzzleCompletion == 2 && puzzleIndex == 1)
                            puzzleIndex++;
                        else
                            puzzleIndex = 0; // falha no puzzle
                    }
                }
                break;
            case "Green":
                leverAnimators[1].SetTrigger("Push");

                if (puzzleCompletion == 0 && puzzleIndex == 2)
                {
                    puzzleIndex = 0;
                    puzzleCompletion = 1;
                }
                else
                {
                    if (puzzleCompletion == 1 && (puzzleIndex == 0 || puzzleIndex == 1))
                    {
                        puzzleIndex++;
                    }
                    else
                        puzzleIndex = 0; // falha no puzzle
                }
                break;
                // ordem 1 -> azul,vermelho,verde
                // ordem 2 -> verde,verde,azul
                // ordem 3 -> vermelho,azul,vermelho

        }

        leverTimer = 1f;
            if (puzzleCompletion == 3)
            StartCoroutine("PuzzleGarageEnd");
    }

    IEnumerator PuzzleGarageEnd()
    {
        generatorAnimator.SetTrigger("Free");
        yield return new WaitForSeconds(1.5f);
        puzzleCompletion = 4;
    }


    private void findDispensaThings()
    {
        for(int i = 0; i < 4; i++)
        {
            if (i < 3)
                keyboardLights[i] = GameObject.Find("Luz_" + (i + 1)).GetComponent<MeshRenderer>();
            puzzleLockedLight[i] = GameObject.Find("Luz_Tranca_" + (i + 1)).GetComponent<MeshRenderer>();
            puzzleLockedLight[i].material = lightsLitMaterial[0];
            doorAnimators[i] = GameObject.Find("Door_" + (i + 1)).GetComponent<Animator>();
            if (!pickupScript.cactos_picked[i])
            {
                cactosColliders[i] = GameObject.Find("Cacto_Chave_" + (i + 1)).GetComponent<BoxCollider>();

                if (puzzlesDispensaComplete[i])
                    cactosColliders[i].enabled = true;
                else
                    cactosColliders[i].enabled = false;     
            }
        }
        for (int i = 0; i < 27; i++)
        {
            painel_puzzle_3[i] = GameObject.Find("Luzinha_" + (i+1)).GetComponent<MeshRenderer>();
            painel_puzzle_3[i].material = normalGrayMaterial;
        }
        canosAnim[0] = GameObject.Find("Red Cilinder").GetComponent<Animator>();
        canosAnim[1] = GameObject.Find("Blue Cilinder").GetComponent<Animator>();
        canosAnim[2] = GameObject.Find("Green Cilinder").GetComponent<Animator>();

    }

    public void puzzle1Handler ()
    {
        StartCoroutine("puzzle1Complete");
    }

    IEnumerator puzzle1Complete()
    {
        animationRunning = true;
        doorAnimators[0].SetTrigger("Key");
        yield return new WaitForSeconds(1.5f);
        puzzleLockedLight[0].material = lightsLitMaterial[2];
        yield return new WaitForSeconds(0.3f);
        doorAnimators[0].SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        animationRunning = false;
        puzzlesDispensaComplete[0] = true;
        cactosColliders[0].enabled = true;
    }

    public void puzzle2Handler(int itemIndex)
    {
        if (animationRunning)
            return;

        if (puzzle2Animator == null)
            puzzle2Animator = GameObject.Find("Puzzle_2_a").GetComponent<Animator>();

        password += itemIndex.ToString();
        keyboardIndex++;
        puzzle2Animator.SetTrigger("Button_" + itemIndex);
        keyboardLights[keyboardIndex - 1].material = lightsLitMaterial[2];

        if (keyboardIndex == 3)
        {
            animationRunning = true;
            if (password == "314")
            {
                Debug.Log("CORRECT");
                StartCoroutine("blinkLights",false);
            }
            else
            {
                Debug.Log("INCORRECT");
                StartCoroutine("blinkLights",true);
            }


            keyboardIndex = 0;
            password = "";
        }
    }

    IEnumerator blinkLights (bool isRed)
    {
        yield return new WaitForSeconds(0.5f);
        keyboardLights[0].material = normalGrayMaterial;
        keyboardLights[1].material = normalGrayMaterial;
        keyboardLights[2].material = normalGrayMaterial;
        yield return new WaitForSeconds(0.5f);
        if (isRed)
        {
            keyboardLights[0].material = lightsLitMaterial[0];
            keyboardLights[1].material = lightsLitMaterial[0];
            keyboardLights[2].material = lightsLitMaterial[0];
        }
        else
        {
            keyboardLights[0].material = lightsLitMaterial[2];
            keyboardLights[1].material = lightsLitMaterial[2];
            keyboardLights[2].material = lightsLitMaterial[2];
        }
        yield return new WaitForSeconds(0.5f);
            keyboardLights[0].material = normalGrayMaterial;
            keyboardLights[1].material = normalGrayMaterial;
            keyboardLights[2].material = normalGrayMaterial;
        yield return new WaitForSeconds(0.5f);
        if (isRed)
        {
            keyboardLights[0].material = lightsLitMaterial[0];
            keyboardLights[1].material = lightsLitMaterial[0];
            keyboardLights[2].material = lightsLitMaterial[0];
        }
        else
        {
            keyboardLights[0].material = lightsLitMaterial[2];
            keyboardLights[1].material = lightsLitMaterial[2];
            keyboardLights[2].material = lightsLitMaterial[2];
        }
        yield return new WaitForSeconds(0.5f);

        if (isRed)
        {
            keyboardLights[0].material = normalGrayMaterial;
            keyboardLights[1].material = normalGrayMaterial;
            keyboardLights[2].material = normalGrayMaterial;
        }
        else
        {
            keyboardLights[0].material = lightsLitMaterial[2];
            keyboardLights[1].material = lightsLitMaterial[2];
            keyboardLights[2].material = lightsLitMaterial[2];
            StartCoroutine("OpenPuzzle2");
        }

        animationRunning = false;
    }

    IEnumerator OpenPuzzle2()
    {
        puzzleLockedLight[1].material = lightsLitMaterial[2];
        yield return new WaitForSeconds(0.3f);
        doorAnimators[1].SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        puzzlesDispensaComplete[1] = true;
        cactosColliders[1].enabled = true;
    }

    // ================================= PUZZLE 3 ============================//

    public void turnlightsOnAndOffPuzzle3 (int lightIndex)
    {

        if (lightsOn[lightIndex])
        {
            painel_puzzle_3[lightIndex].material = normalGrayMaterial;
            lightsOn[lightIndex] = false;
        }
        else
        {
            painel_puzzle_3[lightIndex].material = lightsLitMaterial[2];
            lightsOn[lightIndex] = true;
        }

        checkPattern();

    }

    private void checkPattern()
    {
        int puzzleIndex = 0;

        if (lightsOn[0])
            puzzleIndex++;
        if (lightsOn[1])
            puzzleIndex++;
        if (lightsOn[2])
            puzzleIndex++;
        if (!lightsOn[3])
            puzzleIndex++;
        if (lightsOn[4])
            puzzleIndex++;
        if (lightsOn[5])
            puzzleIndex++;
        if (lightsOn[6])
            puzzleIndex++;
        if (!lightsOn[7])
            puzzleIndex++;
        if (lightsOn[8])
            puzzleIndex++;
        if (!lightsOn[9])
            puzzleIndex++;
        if (lightsOn[10])
            puzzleIndex++;
        if (!lightsOn[11])
            puzzleIndex++;
        if (lightsOn[12])
            puzzleIndex++;
        if (lightsOn[13])
            puzzleIndex++;
        if (lightsOn[14])
            puzzleIndex++;
        if (!lightsOn[15])
            puzzleIndex++;
        if (lightsOn[16])
            puzzleIndex++;
        if (!lightsOn[17])
            puzzleIndex++;
        if (lightsOn[18])
            puzzleIndex++;
        if (lightsOn[19])
            puzzleIndex++;
        if (lightsOn[20])
            puzzleIndex++;
        if (!lightsOn[21])
            puzzleIndex++;
        if (!lightsOn[22])
            puzzleIndex++;
        if (!lightsOn[23])
            puzzleIndex++;
        if (lightsOn[24])
            puzzleIndex++;
        if (lightsOn[25])
            puzzleIndex++;
        if (lightsOn[26])
            puzzleIndex++;

        if (puzzleIndex == 27)
            StartCoroutine("puzzle3Completion");
        else
            puzzleIndex = 0;

    }

    public void lightsKeeper()
    {
       for(int i = 0; i < 27; i++)
        {
            if (lightsOn[i])
                painel_puzzle_3[i].material = lightsLitMaterial[2];
            else
                painel_puzzle_3[i].material = normalGrayMaterial;
        }
    }
    IEnumerator puzzle3Completion()
    {
        animationRunning = true;
        puzzleLockedLight[2].material = lightsLitMaterial[2];
        yield return new WaitForSeconds(0.3f);
        doorAnimators[2].SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        puzzlesDispensaComplete[2] = true;
        animationRunning = false;
        cactosColliders[2].enabled = true;
    }

    public void turnValve (int valveIndex)
    {
        if (animationRunning)
            return;

        if (valveIndex == 4)
            StartCoroutine("resetButtom");
        else
            StartCoroutine("valveTurn", valveIndex);
    }

    IEnumerator valveTurn(int valveIndex)
    {
        animationRunning = true;
        doorAnimators[3].SetTrigger("Valve_" + valveIndex);
        
        switch(valveIndex)
        {
            case 0:
                while (canosPos[0] > 0 && canosPos[1] < 3)
                {
                    canosAnim[0].SetTrigger(canosPos[0] + "_" + (canosPos[0]-1));
                    canosAnim[1].SetTrigger(canosPos[1] + "_" + (canosPos[1]+1));
                    canosPos[0]--;
                    canosPos[1]++;
                    yield return new WaitForSeconds(0.4f);
                }
                break;
            case 1:
                while (canosPos[1] > 0 && canosPos[2] < 3)
                {
                    canosAnim[1].SetTrigger(canosPos[1] + "_" + (canosPos[1]-1));
                    canosAnim[2].SetTrigger(canosPos[2] + "_" + (canosPos[2]+1));
                    canosPos[1]--;
                    canosPos[2]++;
                    yield return new WaitForSeconds(0.4f);
                }
                break;
            case 2:
                while (canosPos[2] > 0 && canosPos[0] < 3)
                {
                    canosAnim[2].SetTrigger(canosPos[2] + "_" + (canosPos[2]-1));
                    canosAnim[0].SetTrigger(canosPos[0] + "_" + (canosPos[0]+1));
                    canosPos[2]--;
                    canosPos[0]++;
                    yield return new WaitForSeconds(0.4f);
                }
                break;
        }
  

        if (canosPos[0] == 2 && canosPos[1] == 3 && canosPos[2] == 1)
            StartCoroutine("puzzle4Completion"); 
        animationRunning = false;
        yield return new WaitForSeconds(0.6f);
    }

    IEnumerator resetButtom()
    {
        animationRunning = true;

        while (canosPos[0] < 3 || canosPos[1] < 3 || canosPos[2] < 3)
        {
            if (canosPos[0] < 3)
            {
                canosAnim[0].SetTrigger(canosPos[0] + "_" + (canosPos[0] + 1));
                canosPos[0]++;
            }
            if (canosPos[1] < 3)
            {
                canosAnim[1].SetTrigger(canosPos[1] + "_" + (canosPos[1] + 1));
                canosPos[1]++;
            }
            if (canosPos[2] < 3)
            {
                canosAnim[2].SetTrigger(canosPos[2] + "_" + (canosPos[2] + 1));
                canosPos[2]++;
            }

            yield return new WaitForSeconds(0.6f);
        }

        for(int i = 0; i < 3; i ++)
        {
            canosAnim[i].SetTrigger("3_2");
            canosPos[i] = 2;
        }
        yield return new WaitForSeconds(0.6f);
        animationRunning = false;
    }

    IEnumerator puzzle4Completion()
    {
        animationRunning = true;
        puzzleLockedLight[3].material = lightsLitMaterial[2];
        yield return new WaitForSeconds(0.3f);
        doorAnimators[3].SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        puzzlesDispensaComplete[3] = true;
        animationRunning = false;
        cactosColliders[3].enabled = true;
    } 

    public void resetAllPuzzles()
    {
        for(int i = 0; i < 27; i++)
        {
            if (i < 3)
            {
                canosPos[i] = 2;
            }
            lightsOn[i] = false;
        }
        keyboardIndex = 0;
        password = "";

    }
}

