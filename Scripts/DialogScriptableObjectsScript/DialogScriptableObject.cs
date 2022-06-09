using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialogs")]
public class DialogScriptableObject : ScriptableObject
{
    public int dialogActive = 0;
    [Tooltip("0 - digueliro 1 - bartolomeu 2 - outro?")]
    public List<int> characterIndex0 = new List<int>();
    public List<int> characterIndex1 = new List<int>();
    public List<int> characterIndex2 = new List<int>();
    public List<DialogData> DialogAmount = new List<DialogData>();
    public List<DialogData> DialogAmount2 = new List<DialogData>();
    public List<DialogData> DialogAmount3 = new List<DialogData>();






}
