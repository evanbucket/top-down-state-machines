using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {
        Idle,
        Moving,
    }
    private PlayerState currentState = PlayerState.Idle;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;
    private const int SPEED_UNIT = 1000;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void IdleState(Vector2 direction) {
        if (direction != new Vector2(0,0)){
            currentState = PlayerState.Moving;
        }
    }

    void MoveState(Vector2 direction) {
        if (direction == new Vector2(0,0)){
            currentState = PlayerState.Idle;
        }
    }

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
        // If current state is moving, the player is moving and is doing walking animation
        if (currentState == PlayerState.Moving) {
            MoveState(direction);
            animator.SetBool("Moving", true);
        // If current state is idle, the player is idle, and is not doing the walking animation
        } else if (currentState == PlayerState.Idle) {
            IdleState(direction);
            animator.SetBool("Moving", false);
        }
        
        // Flip sprite
        if (direction.x < 0) {
            sr.flipX = true;
        } else if (direction.x > 0) {
            sr.flipX = false;
        }  
    }

     void OnCollisionEnter2D(Collision2D collision) {
         if (collision.gameObject.tag == "Enemy") {
            // Respawn
            SceneManager.LoadScene(0);
        }
     }
}
