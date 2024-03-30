using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController playerController;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && TryGetComponent(out Canvas pauseMenu))
        {
            if (pauseMenu.enabled == true)
            {
                pauseMenu.enabled = false;
                Time.timeScale = 1;
                playerController.enabled = true;
            }
            else if (pauseMenu.enabled == false)
            {
                pauseMenu.enabled = true;
                Time.timeScale = 0;
                playerController.enabled = false;
            }
        }
    }
}
