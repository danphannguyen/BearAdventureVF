using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Woodplank : MonoBehaviour
{
    [SerializeField] private string secondaryDialog; // Texte à afficher

    private int woodplankCount = 0;
    private const int requiredWoodplanks = 3;

    // Ajoute des planches
    public void AddWoodplank()
    {
        woodplankCount++;
    }

    // Vérifie si les planches requis ont été collectés
    public bool RequirementMet()
    {
        return woodplankCount >= requiredWoodplanks;
    }

    // Retourne le texte secondaire pour le dialogue
    public string GetSecondaryDialog()
    {
        return secondaryDialog;
    }
}
