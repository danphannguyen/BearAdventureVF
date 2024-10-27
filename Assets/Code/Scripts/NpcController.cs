using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NpcController : ScriptableObject
{

    [SerializeField] public string name;
    [SerializeField] public string dialog;


}
