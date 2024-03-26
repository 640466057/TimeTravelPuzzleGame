using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    float cycle;

    private void Update()
    {
        cycle = (cycle + 2 * Time.deltaTime) % (2 * Mathf.PI);
        transform.GetChild(0).localPosition = new Vector2(0, 0.1f * Mathf.Sin(cycle));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerController PC))
        {
            PC.KeysObtained++;
            GameObject.Destroy(gameObject);
        }
    }
}
