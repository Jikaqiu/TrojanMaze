using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour {
    [SerializeField] float speed = 10f;
    [SerializeField] private FieldOfView filedOfView;
    Vector2 moveInput;
    Rigidbody2D rigidBody;

    public static float HP;

    void Start() {
        rigidBody = GetComponent<Rigidbody2D>();
        HP = 1;
    }

    void Update() {
        Run();
        FlipPlayer();
        filedOfView.SetOrigin(transform.position);
        
        if (HP > 1)
        {
            HP = 1;
        }
        // mute it because if it is kept,when hp is reduced to 0 by a trap, cannot reload the scene and send the player to the start position
        /*if (HP <= 0) {
            Destroy(gameObject);
        }*/

    }

    void Run() {
        Vector2 moveSpeed = new Vector2(moveInput.x * speed, moveInput.y * speed);
        rigidBody.velocity = moveSpeed;
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void FlipPlayer() {
        bool hasHorizontalSpeed = Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon;
        if(hasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(rigidBody.velocity.x) * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.CompareTag("Zombie")) {
            HP -= 0.1f;
        }
    }

}
