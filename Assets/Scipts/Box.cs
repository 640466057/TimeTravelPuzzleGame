using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField] bool AffectedByTimeTravel;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Substring(gameObject.name.Length - 4, 4) == "Echo") //Checks for if the script belongs to an "Echo" and if so, disables it
        {
            //gameObject.TryGetComponent(out Box B);
            enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.TryGetComponent(out PlayerController PC) && gameObject.TryGetComponent(out TimeMachine TM) && Input.GetKeyDown(StaticControls.keys[1]) && AffectedByTimeTravel)
        {
            TM.TimeTravel();
        }
    }
}
