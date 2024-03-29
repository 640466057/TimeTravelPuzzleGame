using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && TryGetComponent(out Canvas pauseMenu))
        {
            if (pauseMenu.enabled == true)
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1;
            }
            else if (pauseMenu.enabled == false)
            {
                pauseMenu.enabled = true;
                Time.timeScale = 0;
            }
        }
    }
}
