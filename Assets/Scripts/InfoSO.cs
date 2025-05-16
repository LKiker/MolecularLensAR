using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Info", menuName = "Info/Add Info")]
public class InfoSO : ScriptableObject
{
    public string tag;  // Used as key in dictionary
    [SerializeField] string PDBName;
    public string[] PDBinformation;
}
