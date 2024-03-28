using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    public new GameObject collider;
    public GameObject real;

    private void Start()
    {
        tag = "Echo";
    }

    private void OnTriggerStay2D(Collider2D collider2D)
    {
        this.collider = collider2D.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collider = null;
    }
}
