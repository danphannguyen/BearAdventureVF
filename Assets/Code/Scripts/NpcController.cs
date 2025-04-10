using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NpcController : ScriptableObject
{
    // Créer les différents NPC
    [SerializeField] public string name;
    [SerializeField] public string dialog;


}
