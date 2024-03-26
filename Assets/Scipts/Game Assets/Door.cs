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

    private void Start()
    {
        transform.GetChild(0).localScale = new Vector3 (1.0f, 1 / transform.localScale.y, 1.0f);
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
            //door opening
            if (Mathf.Abs(transform.GetChild(0).localPosition.y) < 1) // Checks if the should still be opening = (it's not already is fully opened)
            {
                BC.size = new Vector2(1, 2); //Sets the doors hitbox to "moving"
                BC.offset = new Vector2(0, openDirection == State.down ? -0.5f : 0.5f); //Sets the doors hitbox to "moving"
                transform.GetChild(0).localPosition += (openDirection == State.down ? 1 : -1) * Time.deltaTime * Vector3.down; // This moves the door up or down depending on the "openDirection"

                if (Mathf.Abs(transform.GetChild(0).localPosition.y) >= 1) // Checks if the door is fully opened
                {
                    transform.GetChild(0).localPosition = new Vector3(0, openDirection == State.down ? -1 : 1, 0); //Corrects the doors position incase it went up a pixel or two to high
                    BC.size = new Vector2(1, 1); //Sets the doors hitbox to "open"
                    BC.offset = new Vector2(0, openDirection == State.down ? -1 : 1); //Sets the doors hitbox to "open"
                }
            }
        }
        else if (Mathf.Abs(transform.GetChild(0).localPosition.y) > 0) // Checks if the door should be closing = (it's not already is fully closed)
        {
            //closing
            BC.size = new Vector2(1, 2); //Sets the doors hitbox to "moving"
            BC.offset = new Vector2(0, openDirection == State.down ? -0.5f : 0.5f); //Sets the doors hitbox to "moving"
            transform.GetChild(0).localPosition += (openDirection == State.down ? -1 : 1) * Time.deltaTime * Vector3.down; // This moves the door up or down depending on the "openDirection"

            if (openDirection == State.down && transform.GetChild(0).localPosition.y >= 0) // Checks if the door is fully closed in the down state
            {
                transform.GetChild(0).localPosition = new Vector3(0, 0, 0); //Corrects the doors position incase it went up a pixel or two to high
                BC.size = new Vector2(1, 1);//Sets the doors hitbox to "closed"
                BC.offset = new Vector2(0, 0); //Sets the doors hitbox to "closed"
            }
            else if (openDirection == State.up && transform.GetChild(0).localPosition.y <= 0) // Checks if the door is fully closed in the up state
            {
                transform.GetChild(0).localPosition = new Vector3(0, 0, 0); //Corrects the doors position incase it went down a pixel or two to low
                BC.size = new Vector2(1, 1); //Sets the doors hitbox to "closed"
                BC.offset = new Vector2(0, 0); //Sets the doors hitbox to "closed"
            }
        }
    }
}
