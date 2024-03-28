using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Substring(gameObject.name.Length - 4, 4) == "Echo") //Checks for if the script belongs to an "Echo" and if so, disables it
        {
            gameObject.TryGetComponent(out Box B); Destroy(B);
        }
        else if (TryGetComponent(out TimeMachine TM))
        {
            TM.willTimetravel = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.TryGetComponent(out TimeMachine TM) && Input.GetKeyDown(Settings.keys[1]) && TM.echo.transform.localScale != Vector3.zero)
        {
            TM.TimeTravel();
        }
    }
}
