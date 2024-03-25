using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] List<GameObject> checks;
    [SerializeField] int filledCriteria;

    public State openDirection;
    public enum State
    {
        up,
        down
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        filledCriteria = 0;
        foreach (GameObject c in checks)
        {
            if (c.activeSelf)
                filledCriteria++;
        }

        TryGetComponent(out BoxCollider2D BC);
        if (filledCriteria == checks.Count)
        {
            if (Mathf.Abs(transform.GetChild(0).transform.localPosition.y) < 1)
            {
                transform.GetChild(0).transform.localPosition += Vector3.down * Time.deltaTime * (openDirection == State.down ? 1 : -1);
                if (Mathf.Abs(transform.GetChild(0).transform.localPosition.y) > 1)
                {
                    transform.GetChild(0).transform.localPosition = new Vector3(0, (openDirection == State.down ? -1 : 1), 0);
                    BC.enabled = false;
                }
            }
        }
        else
        {
            BC.enabled = true;
            if (Mathf.Abs(transform.GetChild(0).transform.localPosition.y) > 0)
            {
                transform.GetChild(0).transform.localPosition += Vector3.down * Time.deltaTime * (openDirection == State.down ? -1 : 1);
                if (openDirection == State.down)
                {
                    if (transform.GetChild(0).transform.localPosition.y >= 0)
                        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
                }
                else
                {
                    if (transform.GetChild(0).transform.localPosition.y <= 0)
                        transform.GetChild(0).transform.localPosition = new Vector3(0, 0, 0);
                }
            }
        }
    }
}
