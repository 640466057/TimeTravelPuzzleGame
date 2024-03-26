using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public static List<KeyCode> keys = new List<KeyCode> {
        KeyCode.LeftControl,
        KeyCode.LeftShift,
        KeyCode.R
    };

    public static float volume = 1;
}
