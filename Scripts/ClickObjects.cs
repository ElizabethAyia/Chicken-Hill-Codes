using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickObjects : MonoBehaviour
{
    [HideInInspector] public TextManager textManagerScript;
    [HideInInspector] public TextManagerEnglish textManagerEnglishScript;
    [HideInInspector] public PickUpableObject pickupScript;
    [HideInInspector] public GameObject textBox;
    private SoundManager soundManagerScript;
    private PublicSavers publicSaversScript;
    private PuzzlesHandler puzzleScript;
    private CameraMover cameraMoverScript;

    [SerializeField] Transitions transitionScript;
    private ObjectivesScript objectivesChangerScript;
    private GameObject inGameMenu;

    private bool animRolling = false;

    private bool unlockedDoor1 = false;
    public bool radioTaped = false;
    private bool computerDisqueted = false;
    private bool caixa_axed = false;
    private bool caixa_cacted = false;
    private bool big_bananed = false;
    private bool caixa_open = false;

    private void Start()
    {
        puzzleScript = gameObject.GetComponent<PuzzlesHandler>();
        soundManagerScript = gameObject.GetComponent<SoundManager>();
        publicSaversScript = GameObject.FindGameObjectWithTag("Public Saver").GetComponent<PublicSavers>();
        inGameMenu = GameObject.FindGameObjectWithTag("In Game Menu");
        objectivesChangerScript = gameObject.GetComponent<ObjectivesScript>(); 
        textManagerScript = gameObject.GetComponent<TextManager>();
        textManagerEnglishScript = gameObject.GetComponent<TextManagerEnglish>();
        pickupScript = gameObject.GetComponent<PickUpableObject>();
        cameraMoverScript = gameObject.GetComponent<CameraMover>();
        textBox = textManagerScript.textBox;

        objectivesChangerScript.objectiveChanger(0);

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(GameObject.FindGameObjectWithTag("UI Canvas"));

        inGameMenu.gameObject.transform.GetChild(1).gameObject.SetActive(false); // desativa o menu de opções in Game
        inGameMenu.gameObject.transform.GetChild(0).gameObject.SetActive(false); // desativa o menu incial in Game
        
    }

    void Update()
    {
        if (animRolling)
            return;

        if (Input.GetKeyDown(KeyCode.Escape) && !textBox.activeInHierarchy)
            OpenInGameMenu();

        if (!inGameMenu.transform.GetChild(0).gameObject.activeInHierarchy && !inGameMenu.transform.GetChild(1).gameObject.activeInHierarchy)
            clickEffect();


    }

    private void OpenInGameMenu()
    {
        if (!inGameMenu.transform.GetChild(0).gameObject.activeInHierarchy && !inGameMenu.transform.GetChild(1).gameObject.activeInHierarchy)
            inGameMenu.transform.GetChild(0).gameObject.SetActive(true);
        else
        {
            inGameMenu.gameObject.transform.GetChild(1).gameObject.SetActive(false); // desativa o menu de opções in Game
            inGameMenu.gameObject.transform.GetChild(0).gameObject.SetActive(false); // desativa o menu incial in Game
        }
    }
    public void clickEffect()
    {

        if (Input.GetMouseButtonUp(0))
        {
            
            if ((textBox.activeInHierarchy == true)) // caso o chat esteja ativo, rolar o testo com o click
            {
                if (publicSaversScript.gameInPortuguese)
                    textManagerScript.TextScroller(0, false);
                else
                    textManagerEnglishScript.TextScroller(0, false);
            }
            else // caso não esteja, testar se o click está atingindo algum objeto chave
            {
                if(cameraMoverScript.mainCameraActive)
                {
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit, 100.0f))
                    {
                        string objectName = hit.transform.gameObject.name.ToString();
                        //Debug.Log(objectName);

                        ItemSelector(objectName);
                        
                        if ((hit.transform.gameObject.tag == "PickUpObject") || (hit.transform.gameObject.tag == "PickUpAndDestroyObject"))
                        {
                            if (!pickupScript.isInventoryFull())
                            {
                                pickupScript.InventoryManager(objectName);

                                if (hit.transform.gameObject.tag == "PickUpAndDestroyObject")
                                    Destroy(hit.transform.gameObject);
                            }
                        }
                    }
                 
                }
                else
                {
                    RaycastHit hit;
                    Ray ray;
                    switch(cameraMoverScript.cameraIndex)
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
                        string objectName = hit.transform.gameObject.name.ToString();
                        Debug.Log(objectName);

                        ItemSelector(objectName);

                        if ((hit.transform.gameObject.tag == "PickUpObject") || (hit.transform.gameObject.tag == "PickUpAndDestroyObject"))
                        {
                            if (!pickupScript.isInventoryFull())
                            {
                                pickupScript.InventoryManager(objectName);

                                if (hit.transform.gameObject.tag == "PickUpAndDestroyObject")
                                    Destroy(hit.transform.gameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    public void ItemSelector(string ObjectName)
    {
        // é muito importante que a ordem desse Switch seja respeitada quando for adicionados textos dentro do textData
        // pois caso contrário todos os textos ficarão fora de ordem
        int textIndex = -1;
        bool dialogProgress = false;

        switch (ObjectName)
        {
            /*case "Gaveta_1": // dentro de cada case deve ser guardada a inteligencia do objeto
                textIndex = 1;
                break;
            case "Gaveta_2":
                textIndex = 2;
                break;
            case "Gaveta_3":
                textIndex = 3;
                break;
            case "Gaveta_4":
                textIndex = 4;
                break; */
            case "Pia_Porta_1":
                textIndex = 5;
                break;
            case "Pia_Porta_2":
                textIndex = 6;
                break;
            case "Pia_Porta_Dupla":
                textIndex = 7;
                break;
            /*case "Pia_Porta_4":
                textIndex = 8;
                break; */
            case "Pia_Balcão":
                textIndex = 9;
                break;
            case "Armario_Aereo":
                textIndex = 10;
                break;
           /* case "Armario_Aereo_Porta_2":
                textIndex = 11;
                break; */
            case "Lava_Louça":
                textIndex = 12;
                break;
            case "Lixo_Pequeno":
                if (pickupScript.isInventoryFull())
                {
                    textIndex = 20;
                }
                else
                {
                    textIndex = 13;
                    if (textManagerScript.textDataPortuguese[textIndex].dialogActive < 3 && publicSaversScript.gameInPortuguese)
                    {
                        dialogProgress = true;
                        textManagerEnglishScript.textDataEnglish[textIndex].dialogActive++;
                    }

                    if (textManagerEnglishScript.textDataEnglish[textIndex].dialogActive < 3 && !publicSaversScript.gameInPortuguese)
                    {
                        textManagerScript.textDataPortuguese[textIndex].dialogActive++;
                        dialogProgress = true;
                    }
                    //objectivesChangerScript.objectiveChanger(1);
                }
                break;
            case "Armario_Longo":
                textIndex = 14;
                break;
            /*case "Armario_Longo_Porta_Inferior":
                textIndex = 15;
                break; */
            case "Fogao":
                textIndex = 16;
                break;
            case "Fogao_Boca":
                textIndex = 17;
                break;
            /*case "Porta_1":
                textIndex = 18;
                break;
            case string a when a.Contains("Green_Cube"):
                textIndex = 19;
                break;
            case "Porta_2":
                transitionScript.SceneTransition("Cozinha");
                textIndex = 0;
                break; */
            case "Full Inventory":
                textIndex = 20;
                break;
            case "Luminaria":
                textIndex = 21;
                break;
            case "Porta_Porao": // isso é o laboratório na vdd
                textIndex = 22;

                if (radioTaped)
                {
                    transitionScript.SceneTransition("Laboratorio");
                    textIndex = 0;
                }
                else
                    soundManagerScript.playASoundClip(1);
                break;
            case "Caixa_de_madeira":
                textIndex = 23;

                if (pickupScript.IsItemInInventory("Machado_UI"))
                {
                    StartCoroutine("caixa_Sala");
                    pickupScript.removeItemsFromInventory("Machado_UI");
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    textIndex = 0;
                }
                else if (caixa_axed)
                {
                    dialogProgress = true;
                    caixa_axed = false;
                }
                break;

            case "Cama":
                textIndex = 24;
                break;
            case "Comoda":
                textIndex = 25;
                break;
            case "Janela_Quarto":
                textIndex = 26;
                break;
            case "Porta_Quarto":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Sala");
                textIndex = 0;
                break;
            case "Caixa_de_energia":
                soundManagerScript.playASoundClip(10);
                textIndex = 27;
                if (textManagerScript.textDataPortuguese[textIndex].dialogActive == 1)
                {
                    puzzleScript.clickFuzeBox();
                    textIndex = 0;
                }
                else
                {
                    StartCoroutine("triggersAfterDialog", ObjectName);
                    dialogProgress = true;
                }             
                break;
            case "Radio1":
                textIndex = 28;

                if (pickupScript.IsItemInInventory("Tape_UI"))
                {
                    pickupScript.removeItemsFromInventory("Tape_UI");
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    radioTaped = true;
                    GameObject.Find("Som da gazela").GetComponent<AudioSource>().Play();
                }
                else
                {
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 0;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 0;
                    //soundManagerScript.changeMainMusic(false);
                    StartCoroutine("triggersAfterDialog", ObjectName);
                    soundManagerScript.playASoundClip(9);
                }
                break;
            case "Relogio":
                textIndex = 29;
                break;
            //case "TV":
                //textIndex = 30;
                //break;
            case "Doorway_Sala_Quarto":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Quarto");
                textIndex = 0;
                break;
            case "Porta_Sala_Cozinha":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Cozinha");
                textIndex = 0;
                break;
            case "Porta_Sala_Garagem":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Garagem");
                textIndex = 0;
                break;
            default: // isso serve pra representar que o player não clicou em nenhum objeto interagivel
                textIndex = 0;
                break;
            case "Doorway_Cozinha_Sala":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Sala");
                textIndex = 0;
                break;
            case "Porta_Cozinha_Dispensa":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Dispensa");
                textIndex = 0;
                break;
            case "Doorway_Dispensa_Cozinha":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Cozinha");
                textIndex = 0;
                break;
            case "Porta_Dispensa_Porao":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Porao porao");
                textIndex = 0;
                break;
            case "Escada_Porao":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Dispensa");
                textIndex = 0;
                break;
            case "Doorway_Garagem_Sala":
                soundManagerScript.playASoundClip(7);
                if (puzzleScript.puzzleCompletion >= 3 && (pickupScript.celula_pckd == false))
                    textIndex = 15;
                else
                {
                    transitionScript.SceneTransition("Sala");
                    textIndex = 0;
                }   
                break;
            case "Doorway_Garagem_Rua":
                soundManagerScript.playASoundClip(7);
                if (puzzleScript.puzzleCompletion >= 3 && (pickupScript.celula_pckd == false))
                    textIndex = 15;
                else
                {
                    transitionScript.SceneTransition("Rua");
                    textIndex = 0;
                }
                break;
            case "Porta_Rua_Garagem":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Garagem");
                textIndex = 0;
                break;
            case "Escada_Laboratorio_Quarto":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Quarto");
                textIndex = 0;
                break;
            case "Escada_Quarto_Laboratorio":
                soundManagerScript.playASoundClip(7);
                transitionScript.SceneTransition("Laboratório");
                textIndex = 0;
                break;
            case string a when a.Contains("Tape_UI"):
                textIndex = 31;
                break;
            case "Disquete":
                soundManagerScript.playASoundClip(5);
                if (pickupScript.isInventoryFull())
                    textIndex = 20;
                else
                    textIndex = 32;
                break;
            case "Disquete_UI":
                textIndex = 33;
                break;
            case "Machado":
                if (pickupScript.isInventoryFull())
                    textIndex = 20;
                else
                    textIndex = 35;
                break;
            case "Machado_UI":
                textIndex = 36;
                break;
            case "Chave_1_UI":
                textIndex = 37;
                break;
            case "Caixa_Do_Cacto":
                textIndex = 38;
                if (pickupScript.IsItemInInventory("Chave_1_UI"))
                {
                    pickupScript.removeItemsFromInventory("Chave_1_UI");
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    dialogProgress = true;
                    pickupScript.InventoryManager("Cacto_UI");
                }
                break;
            case "Cacto_UI":
                textIndex = 39;
                break;
            // ======================= PUZZLE DA LUZ NA SALA ==================================================== //
            case "Alavanca_Vermelha":
                soundManagerScript.playASoundClip(0);
                puzzleScript.ClickLever("Red");
                textIndex = 0;
                break;
            case "Alavanca_Verde":
                soundManagerScript.playASoundClip(0);
                puzzleScript.ClickLever("Green");
                textIndex = 0;
                break;
            case "Alavanca_Azul":
                soundManagerScript.playASoundClip(0);
                puzzleScript.ClickLever("Blue");
                textIndex = 0;
                break;
            case "Celula_De_Energia":
                if (pickupScript.isInventoryFull())
                    textIndex = 20;
                else
                {
                    textIndex = 40;
                    if (puzzleScript.puzzleCompletion == 4 && textManagerEnglishScript.textDataEnglish[textIndex].dialogActive == 0)
                    {
                        pickupScript.InventoryManager("Celula_De_Energia");
                        textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                        textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                        Destroy(GameObject.Find("Celula_De_Energia"));
                    }
                    else
                    {
                        if (textManagerEnglishScript.textDataEnglish[textIndex].dialogActive == 1)
                            textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 2;
                    }
                }
                break;
            case "Celula_De_Energia_UI":
                textIndex = 41;
                break;
            case "Travesseiro":
                if (pickupScript.isInventoryFull())
                    textIndex = 20;
                else
                {
                    textIndex = 42;
                    if (textManagerEnglishScript.textDataEnglish[textIndex].dialogActive == 0)
                        dialogProgress = true;
                }

                break;
            case "Chave_2_UI":
                textIndex = 43;
                break;
            case "Caixa_Garagem":
                textIndex = 44;
                if (pickupScript.IsItemInInventory("Chave_2_UI"))
                {
                    StartCoroutine("caixa_Garagem");
                    pickupScript.removeItemsFromInventory("Chave_2_UI");
                    textIndex = 0;
                }
                else if (caixa_open)
                {
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    dialogProgress = true;
                    caixa_open = false;
                }
                break;
            case "Tonico_UI":
                textIndex = 45;
                break;
            case "Coqueiro":
                textIndex = 46;
                if (pickupScript.IsItemInInventory("Tonico_UI"))
                {
                    StartCoroutine("bigBananao");
                    pickupScript.removeItemsFromInventory("Tonico_UI");
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    textIndex = 0;
                    soundManagerScript.playASoundClip(2);
                }
                else if (big_bananed)
                {
                    dialogProgress = true;
                    big_bananed = false;
                }
                break;
            case "Bananao_UI":
                textIndex = 47;
                break;
            case "Liquidificador":
                textIndex = 48;
                if ((pickupScript.IsItemInInventory("Bananao_UI")) && textManagerEnglishScript.textDataEnglish[textIndex].dialogActive == 0)
                {
                    pickupScript.removeItemsFromInventory("Bananao_UI");
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    dialogProgress = true;
                    pickupScript.InventoryManager("Pasta_De_Banana_UI");
                }
                break;
            case "Pasta_De_Banana_UI":
                textIndex = 49;
                break;

            // =========================================== PUZZLES DA DESPENSA ================================================================ //
            case "Puzzle_1":
                soundManagerScript.playASoundClip(4);
                cameraMoverScript.switchCamera(ObjectName);
                textIndex = 0;
                break;
            case "Puzzle_2":
                soundManagerScript.playASoundClip(4);
                cameraMoverScript.switchCamera(ObjectName);
                textIndex = 0;
                break;
            case "Puzzle_3":
                soundManagerScript.playASoundClip(4);
                cameraMoverScript.switchCamera(ObjectName);
                textIndex = 0;
                break;
            case "Puzzle_4":
                soundManagerScript.playASoundClip(4);
                cameraMoverScript.switchCamera(ObjectName);
                textIndex = 0;
                break;
            case "Parede_Out":
                cameraMoverScript.switchCamera(ObjectName);
                textIndex = 0;
                break;
            case "Butao_0":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(0);
                textIndex = 0;
                break;
            case "Butao_1":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(1);
                textIndex = 0;
                break;
            case "Butao_2":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(2);
                textIndex = 0;
                break;
            case "Butao_3":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(3);
                textIndex = 0;
                break;
            case "Butao_4":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(4);
                textIndex = 0;
                break;
            case "Butao_5":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(5);
                textIndex = 0;
                break;
            case "Butao_6":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(6);
                textIndex = 0;
                break;
            case "Butao_7":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(7);
                textIndex = 0;
                break;
            case "Butao_8":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(8);
                textIndex = 0;
                break;
            case "Butao_9":
                soundManagerScript.playASoundClip(5);
                puzzleScript.puzzle2Handler(9);
                textIndex = 0;
                break;
            case string a when a.Contains("Luzinha_"):
                textIndex = 0;
                soundManagerScript.playASoundClip(10);
                for (int i = 1; i < 28; i++)
                {
                    if (a == "Luzinha_" + i)
                        puzzleScript.turnlightsOnAndOffPuzzle3(i-1);
                }
                break;
            // =============================================================================================== //
            // ==================================props garagem 21/05========================================== //
            
            case "Lata_Tinta":
                textIndex = 50;
                break;
            case "Seta_Azul":
                textIndex = 51;
                break;
            case "Seta_Vermelha":
                textIndex = 52;
                break;
            case "Seta_Verde":
                textIndex = 53;
                break;
            case "Globo":
                textIndex = 54;
                break;
            case "Abajur_Garagem":
                textIndex = 55;
                break;
            case "Caixa_Ferramentas_Mesa":
                textIndex = 56;
                break;
            case "Caixa_Ferramentas_Estante":
                textIndex = 57;
                break;
            case "Canos1":
                textIndex = 58;
                break;
            case "Chaves_Parede_Garagem":
                textIndex = 59;
                break;
            // =============================================================================================== //
            // ===============================props dispensa 21/05 =========================================== //
            case "Porta_Ferro":
                //transitionScript.SceneTransition("porao porao");
                //textIndex = 0;
                textIndex = 60;
                break;
            case "Barriu":
                textIndex = 61;
                break;
            case "Mesinha_Dispensa":
                textIndex = 62;
                break;
            case "Pote_Dispensa_1":
                textIndex = 63;
                break;
            case "Pote_Dispensa_2":
                textIndex = 64;
                break;
            case "Pote_Dispensa_3":
                textIndex = 65;
                break;
            case "Potinho_1":
                textIndex = 66;
                break;
            case "Potinho_2":
                textIndex = 67;
                break;
            case "Potinho_3":
                textIndex = 68;
                break;
            case "Caixa_Mesa_Dispensa":
                textIndex = 69;
                break;
            // =============================================================================================== //
            // ==================================props rua 21/05============================================== //
            case "Cerca":
                textIndex = 70;
                break;
            case "Cerca_Murinho":
                textIndex = 71;
                break;
            case "Porta_Rua":
                textIndex = 72;
                break;
            case "Cano_Calha_1":
                textIndex = 73;
                break;
            case "Janela_Rua":
                textIndex = 74;
                break;
            // =============================================================================================== //
            // ==================================props porao porao 21/05====================================== //
            case "Lareira":
                textIndex = 75;
                break;
            case "Jaulas":
                textIndex = 76;
                break;
            case "Caixa_Luz":
                soundManagerScript.playASoundClip(10);
                textIndex = 77;
                break;
            case "Canos":
                textIndex = 78;
                break;
            // =============================================================================================== //
            // ==================================props Quarto 21/05=========================================== //
            case "Cobertor":
                soundManagerScript.playASoundClip(3);
                textIndex = 79;
                break;
            case "Tapete":
                textIndex = 80;
                break;
            // =============================================================================================== //
            // ==================================props Sala 21/05=========================================== //
            case "Comoda1":
                textIndex = 81;
                break;
            case "Comoda2":
                textIndex = 82;
                break;
            case "Estante_De_Livros":
                textIndex = 83;
                break;
            case "Sofa":
                textIndex = 84;
                break;
            case "Abajur_Sala":
                textIndex = 85;
                break;
            case "Sustentação_Sala":
                textIndex = 86;
                break;
            case "Chao_Sala":
                textIndex = 87;
                break;
            // =============================================================================================== //
            case "Tranca_Puzzle_1":
                textIndex = 0;
                if (pickupScript.IsItemInInventory("Chave_1_UI"))
                {
                    soundManagerScript.playASoundClip(14);
                    puzzleScript.puzzle1Handler();
                    pickupScript.removeItemsFromInventory("Chave_1_UI");
                }
                break;
            case string a when a.Contains("Alavanca_redonda"):
                if (ObjectName == "Alavanca_redonda1")
                    puzzleScript.turnValve(0);
                else if (ObjectName == "Alavanca_redonda2")
                    puzzleScript.turnValve(1);
                else
                    puzzleScript.turnValve(2);
                textIndex = 0;
                break;
            case "Reset_Button":
                puzzleScript.turnValve(4);
                textIndex = 0;
                break; 
            // =============================================================================================== //
            // ========================================= LABORATORIO ========================================= //
            case "Escada_Laboratorio":
                textIndex = 0;
                transitionScript.SceneTransition("Quarto");
                break;

            case string a when a.Contains("Cacto_Chave_"):
                if (pickupScript.isInventoryFull())
                    textIndex = 20;
                else
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        if (ObjectName == "Cacto_Chave_" + i)
                        {
                            pickupScript.InventoryManager(ObjectName);
                            textIndex = 87 + i;
                            Destroy(GameObject.Find(ObjectName));
                        }

                    }
                }
                break;
            case string a when a.Contains("Espaço_Cacto_"):
                textIndex = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (ObjectName == "Espaço_Cacto_" + (i+1))
                    {
                        if (pickupScript.IsItemInInventory("Cacto_Chave_" + (i + 1)))
                        {
                            //textIndex = 91 + i;
                            pickupScript.removeItemsFromInventory("Cacto_Chave_" + (i + 1));
                            GameObject cacto = GameObject.Find("Espaço_Cacto_" + (i + 1)).transform.GetChild(0).gameObject;
                            cacto.SetActive(true);
                            puzzleScript.cactosPlaced[i] = true;
                        }
                        else
                            textIndex = 92;
                    }
                }

                break;
            case "Gerador_Lab":
                textIndex = 93;
                if (puzzleScript.celulaEnergized && puzzleScript.celulaPlaced)
                {
                    textIndex = 0;
                    break;
                }
                if (pickupScript.IsItemInInventory("Celula_De_Energia_UI"))
                {
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    pickupScript.removeItemsFromInventory("Celula_De_Energia_UI");
                    GameObject celula = GameObject.Find("Gerador_Lab").transform.GetChild(10).gameObject;
                    celula.SetActive(true);
                    puzzleScript.celulaPlaced = true;
                }
                else
                {
                    if (puzzleScript.celulaPlaced && pickupScript.IsItemInInventory("Pasta_de_Banana_UI"))
                    {
                        textManagerScript.textDataPortuguese[textIndex].dialogActive = 2;
                        textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 2;
                        pickupScript.removeItemsFromInventory("Pasta_de_Banana_UI");
                        puzzleScript.celulaEnergized = true;
                        soundManagerScript.playASoundClip(11);
                    }
                }
                break;
            case "Computador":
                textIndex = 95;
                if (pickupScript.IsItemInInventory("Disquete_UI"))
                {
                    
                    pickupScript.removeItemsFromInventory("Disquete_UI");
                    computerDisqueted = true;
                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                }
                else
                {
                    if (computerDisqueted)
                    {
                        textIndex = 95;
                        if (puzzleScript.celulaPlaced)
                        {
                            if (puzzleScript.celulaEnergized)
                            {
                                if (puzzleScript.cactosPlaced[0] && puzzleScript.cactosPlaced[1] && puzzleScript.cactosPlaced[2] && puzzleScript.cactosPlaced[3])
                                {
                                    textManagerScript.textDataPortuguese[textIndex].dialogActive = 2;
                                    textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 2;
                                    StartCoroutine("Lab_Complete");
                                }
                            }
                        }
                    }
                }
            break;
            case "Geladeira_portal":
                textIndex = 94;
                
                if (puzzleScript.labComplete)
                {
                    //soundManagerScript.playASoundClip(12);
                    //textManagerScript.textDataPortuguese[textIndex].dialogActive = 1;
                    //textManagerEnglishScript.textDataEnglish[textIndex].dialogActive = 1;
                    //GameObject.Find("Final").SetActive(true);
                     textIndex = 0;
                     NextScene nextSceneScript = GameObject.Find("Fade").GetComponent<NextScene>();
                     nextSceneScript.NextSceneChanger();

                    
                }
                break;
        }
        if (publicSaversScript.gameInPortuguese)
            textManagerScript.TextScroller(textIndex, dialogProgress);
        else
            textManagerEnglishScript.TextScroller(textIndex, dialogProgress);

        dialogProgress = false;
    }


    IEnumerator triggersAfterDialog(string itemIndex)
    {
        yield return new WaitForSeconds(.3f);

        while(textBox.activeInHierarchy)
        {
            yield return null;
        }

        switch(itemIndex)
        {
            case "Radio1":
                soundManagerScript.changeMainMusic(false);
                break;
            case "Caixa_de_energia":
                puzzleScript.clickFuzeBox();
                break;
        }
    }

    IEnumerator bigBananao()
    {
        ParticleSystem[] bananaParticles = new ParticleSystem[3];
        animRolling = true;
        Animator bananaAnimator = GameObject.Find("Bananao").GetComponent<Animator>();
        bananaAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(1.3f);
        for (int i = 0; i < 3; i++)
        {
            bananaParticles[i] = GameObject.Find("Particulas banana " + (i + 1)).GetComponent<ParticleSystem>();
            bananaParticles[i].Play();
        }
        big_bananed = true;
        ItemSelector("Coqueiro");
        animRolling = false;
        yield return new WaitForSeconds(0.5f);
        while(textBox.activeInHierarchy)
        {
            yield return null;
        }
        bananaParticles[0].Stop();
        bananaParticles[1].Stop();
        bananaParticles[2].Stop();
        Destroy(GameObject.Find("Bananao"));
        pickupScript.InventoryManager("Bananao_UI");

    }

    IEnumerator caixa_Garagem()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("Camera Arm").transform.GetChild(0).gameObject;
        GameObject secondaryCam = GameObject.Find("CameraPos2");
        Animator boxAnim = GameObject.Find("Caixa_Garagem").GetComponent<Animator>();
        animRolling = true;
        caixa_open = true;
        mainCamera.SetActive(false);
        secondaryCam.SetActive(true);
        secondaryCam.GetComponent<Camera>().enabled = true;
        secondaryCam.GetComponent<AudioListener>().enabled = true;
        boxAnim.SetTrigger("Open");
        yield return new WaitForSeconds(1f);
        ItemSelector("Caixa_Garagem");
        animRolling = false;
        yield return new WaitForSeconds(0.5f);
        while (textBox.activeInHierarchy)
        {
            yield return null;
        }

        Destroy(GameObject.Find("Tonico"));
        pickupScript.InventoryManager("Tonico_UI");
        mainCamera.SetActive(true);
        secondaryCam.SetActive(false);

        yield return null;
    }

    IEnumerator caixa_Sala()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("Camera Arm").transform.GetChild(0).gameObject;
        GameObject secondaryCam = GameObject.Find("CameraPos2");
        Animator boxAnim = GameObject.Find("Caixa_de_madeira").GetComponent<Animator>();
        animRolling = true;
        caixa_axed = true;
        mainCamera.SetActive(false);
        secondaryCam.SetActive(true);
        secondaryCam.GetComponent<Camera>().enabled = true;
        secondaryCam.GetComponent<AudioListener>().enabled = true;
        boxAnim.SetTrigger("Break");
        yield return new WaitForSeconds(0.6f);
        soundManagerScript.playASoundClip(6);
        yield return new WaitForSeconds(0.6f);
        ItemSelector("Caixa_de_madeira");
        animRolling = false;
        yield return new WaitForSeconds(0.5f);

        while (textBox.activeInHierarchy)
        {
            yield return null;
        }

        Destroy(GameObject.Find("Chave"));
        pickupScript.InventoryManager("Chave_1");
        mainCamera.SetActive(true);
        secondaryCam.SetActive(false);
    }

    IEnumerator Lab_Complete()
    {
        Animator geladeiraAnim = GameObject.Find("Geladeira_portal_agregador").GetComponent<Animator>();
        geladeiraAnim.SetTrigger("Open");
        //GameObject.Find("Geladeira_portal 1").SetActive(false);
        soundManagerScript.playASoundClip(12);
        yield return new WaitForSeconds(1f);
        puzzleScript.labComplete = true;
    }
}
