using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarpModeText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        text.text = $"Pink: Just the blue boxes teleport\nBlue: Both you & the boxes teleport\nSwitch between them with {Settings.keys[0]}";
    }
}
