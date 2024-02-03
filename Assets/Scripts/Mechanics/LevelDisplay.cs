using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelDisplay : MonoBehaviour
{
    public TMP_Text levelText;
    public Experience playerExperience;

    void Update()
    {
        // Update the displayed level whenever the player's experience changes.
        levelText.text = $"Level: {playerExperience.GetCurrentLevel()}";
    }
}