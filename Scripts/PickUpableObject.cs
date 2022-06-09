using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpableObject : MonoBehaviour
{
    [SerializeField] GameObject[] UIObjectsPrefabs;
    // 1 - fita 2 - disquete 3 - machado 4 - Chave cacto 1 5 - Cacto 6 - Celula de energia 7 - Chave 2
    [SerializeField] GameObject inventoryUI;
    public GameObject[] inventorySlots = new GameObject[6]; // isso está publico só por questão de teste, tornar privado e declara-lo no start
    private GameObject[] objectsInInventory = new GameObject[6];
    public bool[] slotsAvailable = new bool[6];
    public string[] objectsInInventoryName = new string[6];

    // lista de Bools responsáveis por organizar os items
    private bool tape_pckd = false;
    [HideInInspector] public bool disk_pckd = false;
    [HideInInspector] public bool machado_pckd = false;
    [HideInInspector] public bool chave_cacto_1_pckd = false;
    [SerializeField]private bool cacto_pckd = false;
    [HideInInspector] public bool celula_pckd = false;
    private bool chave_2_pckd = false;
    [HideInInspector]public bool tonico_pckd = false;
    private bool bananao_pckd = false;
    private bool bananao_pasta_pckd = false;
    [HideInInspector] public bool[] cactos_picked = new bool[4];
    private void Start()
    {
        for(int i = 0; i < 6; i++)
        {
            if (i < 4)
                cactos_picked[i] = false;

            slotsAvailable[i] = true;
            objectsInInventoryName[i] = "none";
        }
    }
    public void InventoryManager(string objectName)
    {
        switch (objectName)
        {
            case "Lixo_Pequeno":
                for(int i = 0; i < inventorySlots.Length; i++)
                {
                    if (tape_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[1], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Tape_UI";
                        tape_pckd = true;
                        break;
                    }
                }
                break;
            case "Disquete":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (disk_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[2], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Disquete_UI";
                        disk_pckd = true;
                        break;
                    }
                }
                break;
            case "Machado":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (machado_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[3], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Machado_UI";
                        machado_pckd = true;
                        break;
                    }
                }
                break;
            case "Chave_1":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (chave_cacto_1_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[4], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Chave_1_UI";
                        chave_cacto_1_pckd = true;
                        break;
                    }
                }
                break;
            case "Cacto_UI":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (cacto_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[5], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Cacto_UI";
                        cacto_pckd = true;
                        break;
                    }
                }
                break;
            case "Celula_De_Energia":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (celula_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[6], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Celula_De_Energia_UI";
                        celula_pckd = true;
                        break;
                    }
                }
                break;
            case "Travesseiro":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (chave_2_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[7], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Chave_2_UI";
                        chave_2_pckd = true;
                        break;
                    }
                }
                break;
            case "Tonico_UI":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (tonico_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[8], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Tonico_UI";
                        tonico_pckd = true;
                        break;
                    }
                }
                break;
            case "Bananao_UI":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (bananao_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[9], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Bananao_UI";
                        bananao_pckd = true;
                        break;
                    }
                }
                break;
            case "Pasta_De_Banana_UI":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (bananao_pasta_pckd)
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[10], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Pasta_de_Banana_UI";
                        bananao_pasta_pckd = true;
                        break;
                    }
                }
                break;
            case "Cacto_Chave_1":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (cactos_picked[0])
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[11], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Cacto_Chave_1";
                        cactos_picked[0] = true;
                        break;
                    }
                }
                break;
            case "Cacto_Chave_2":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (cactos_picked[1])
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[12], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Cacto_Chave_2";
                        cactos_picked[1] = true;
                        break;
                    }
                }
                break;
            case "Cacto_Chave_3":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (cactos_picked[2])
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[13], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Cacto_Chave_3";
                        cactos_picked[2] = true;
                        break;
                    }
                }
                break;
            case "Cacto_Chave_4":
                for (int i = 0; i < inventorySlots.Length; i++)
                {
                    if (cactos_picked[3])
                        break;

                    if (slotsAvailable[i])
                    {
                        objectsInInventory[i] = Instantiate(UIObjectsPrefabs[14], new Vector3(0, 0, 0), Quaternion.identity); // trocar isso no futuro pra algo mais claro
                        objectsInInventory[i].transform.SetParent(inventorySlots[i].transform, false);
                        slotsAvailable[i] = false;
                        objectsInInventoryName[i] = "Cacto_Chave_4";
                        cactos_picked[3] = true;
                        break;
                    }
                }
                break;

            default:
                break;
        }
    }
    public void removeItemsFromInventory(string objectName)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (objectsInInventoryName[i] == objectName)
            {
                Destroy(objectsInInventory[i]);
                slotsAvailable[i] = true;
                objectsInInventoryName[i] = "none";
            }
        }

        //reorganizeInventory();
    }

    private void reorganizeInventory()
    {
        StartCoroutine("organizeInventory");
    }

    IEnumerator organizeInventory()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 2; i++)
        {
            if (slotsAvailable[i] && !slotsAvailable[i + 1])
            {
                objectsInInventory[i + 1].transform.SetParent(inventorySlots[i].transform, false);
                slotsAvailable[i] = false;
                slotsAvailable[i + 1] = true;
                objectsInInventoryName[i] = objectsInInventoryName[i + 1];
                objectsInInventoryName[i + 1] = "none";
            }
        }
    }


    public bool isInventoryFull()
    {

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if (slotsAvailable[i])
            {
                return false;
            }
        }

        return true;
    }

    public bool IsItemInInventory(string itemID)
    {
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (objectsInInventoryName[i] == itemID)
                return true;
        }

        return false;
    }
}
