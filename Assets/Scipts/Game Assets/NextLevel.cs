using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] int keysRequired;
    [SerializeField] GameObject player;
    [SerializeField] GameObject fade;
    Animator fadeAnim;
    bool open = false;

    private void Start()
    {
        if (fade != null && fade.transform.childCount != 0 && fade.transform.GetChild(0).TryGetComponent(out Animator fadeAnim))
        {
            this.fadeAnim = fadeAnim;
        }
    }

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
            if (fadeAnim != null)
                fadeAnim.SetTrigger("FadeOut");
            else
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
