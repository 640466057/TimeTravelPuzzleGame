using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int keysRequired;
    [SerializeField] GameObject player;
    bool open = false;

    void Update()
    {
        if (player.TryGetComponent(out PlayerController PC) && PC.KeysObtained >= keysRequired && !open)
        {
            anim.SetTrigger("Open");
            open = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.TryGetComponent(out PlayerController PC) && PC.KeysObtained >= keysRequired)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
