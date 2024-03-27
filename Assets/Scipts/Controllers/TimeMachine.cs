using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMachine : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Time Travel")]
    public GameObject echo;
    [SerializeField] Vector3 previusPos;
    [SerializeField] Quaternion previusRot;
    [SerializeField] Vector3 previusScale;
    public Vector3 previusVelocity;
    public float previusHorizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Substring(gameObject.name.Length - 4, 4) != "Echo")
        {
            echo = Instantiate(gameObject);
            echo.name = echo.name.Substring(0, echo.name.Length - 7) + " Echo";
            echo.AddComponent<Echo>();

            if (echo.TryGetComponent(out TimeMachine TM))
                TM.enabled = false;
            if (echo.TryGetComponent(out Rigidbody2D RB))
                RB.constraints = RigidbodyConstraints2D.FreezeAll;
            if (echo.TryGetComponent(out BoxCollider2D BC))
            {
                BC.isTrigger = true;
                BC.size -= new Vector2(0.05f, 0.05f);
            }
            if (echo.TryGetComponent(out SpriteRenderer SR))
                SR.color = new Vector4(0.0f, 1.0f, 0.2f, 0.5f);
            if (echo.TryGetComponent(out AudioSource AS))
                AS.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Time Travel
        echo.transform.SetPositionAndRotation(previusPos, previusRot);
        echo.transform.localScale = previusScale;
        StartCoroutine(LoadTimeTravel(10));
    }

    IEnumerator LoadTimeTravel(int time)
    {
        Vector3 pos = transform.position;
        Quaternion rot = transform.rotation;
        Vector3 scale = transform.localScale;
        Vector2 velocity = rb.velocity;
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        yield return new WaitForSeconds(time);

        previusPos = pos;
        previusRot = rot;
        previusScale = scale;
        previusVelocity = velocity;
        previusHorizontalInput = horizontalInput;
    }

    public void TimeTravel()
    {
        if (echo.TryGetComponent(out Echo E) && (E.collider == null || E.collider == gameObject))
        {
            transform.SetPositionAndRotation(previusPos, previusRot);
            transform.localScale = previusScale;
            rb.velocity = previusVelocity;
        }
    }
}