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
    public static bool pink = false;
    public static SystemLanguage language; //Future language changing system?
}
