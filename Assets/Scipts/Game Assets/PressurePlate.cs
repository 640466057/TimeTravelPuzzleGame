using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool active = false;
    [SerializeField] Animator anim;

    private void Start()
    {
        if (transform.GetChild(0).TryGetComponent(out Animator animator))
            anim = animator;
        else
            Debug.LogError("Missing Animator");
    }

    private void Update()
    {
        transform.GetChild(1).gameObject.SetActive(active);
        anim.SetBool("down", active);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "box")
        {
            active = true;
            //transform.GetChild(0).transform.localPosition = new Vector3(0, -0.5f, 0);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "box")
        {
            active = false;
            //transform.GetChild(0).transform.localPosition = new Vector3(0, -0.45f, 0);
        }
    }
}
