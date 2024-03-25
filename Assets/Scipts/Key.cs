using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController PC))
        {
            PC.KeysObtained++;
            GameObject.Destroy(gameObject);
        }
    }
}
