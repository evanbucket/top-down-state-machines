using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {
        Idle,
        Moving,
    }
    private PlayerState currentState = PlayerState.Idle;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private const int SPEED_UNIT = 1000;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void IdleState(Vector2 direction) {
        sr.color = Color.white;
        if (direction != new Vector2(0,0)){
            currentState = PlayerState.Moving;
        }
    }

    void MoveState(Vector2 direction) {
        sr.color = Color.red;
        if (direction == new Vector2(0,0)){
            currentState = PlayerState.Idle;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
         // +1 for right/up, -1 for left/down.
        // Example: Vector2(1, -1) = diagonal direction right and down.
        Vector2 direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        ).normalized;

        // Take the previous position and add any change to it
        // in the x and y directions.
        transform.position = new Vector2(
            transform.position.x + speed * direction.x / SPEED_UNIT,
            transform.position.y + speed * direction.y / SPEED_UNIT
        );

        if (currentState == PlayerState.Moving) {
            MoveState(direction);
        } else if (currentState == PlayerState.Idle) {
            IdleState(direction);
        }
        Debug.Log(direction);
    }
}
