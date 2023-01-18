using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    enum EnemyState {
        Idle,
        Moving
    }

    private EnemyState currentState = EnemyState.Idle;
    private SpriteRenderer sr;

    // The value we found in Debug.Log above.
    private const float SIGHT_DISTANCE = 8.0f;

    // The two points that the Enemy will move between in the Moving state.
    private const float RIGHT_MAX = 7.16f;
    private const float LEFT_MAX = -0.16f;

    private int direction = -1;
    private float xSpeed = 0.02f;

    public GameObject player;

    void IdleState(float distance) {
        sr.color = Color.white;

        // switch to moving if in sight range
        if (distance <= SIGHT_DISTANCE) {
            currentState = EnemyState.Moving;
        }
    }

    void MovingState(float distance) {
        sr.color = Color.yellow;

        // move back and forth
         if (transform.position.y >= RIGHT_MAX) {
            direction = -1;
        } else if (transform.position.y <= LEFT_MAX) {
            direction = 1;
        }
         transform.position = new Vector3(transform.position.x, transform.position.y + direction * xSpeed, transform.position.z);

        // switch to idle if out of sight range
        if (distance > SIGHT_DISTANCE) {
            currentState = EnemyState.Idle;
        }
    }
    

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
        if (currentState == EnemyState.Idle) {
            IdleState(distance);
        } else if (currentState == EnemyState.Moving) {
            MovingState(distance);
        }
    }
}
