using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarpText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        text.text = $"If you press {Settings.keys[1]}\n you can teleport to\n your Echo";
    }
}
