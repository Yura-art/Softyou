using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movment : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;

    [SerializeField] Transform detectionCenter;
    [SerializeField] float detectionRadius = 0.2f;

    private Rigidbody2D rigid;
    private Animator anim;
    private SpriteRenderer sprt;

    private float horizontalValue;
    private bool isGrounded;
    private bool jumpRequested = false;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprt = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Captura la entrada horizontal
        horizontalValue = Input.GetAxis("Horizontal");

        // Detecta si el jugador quiere saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            jumpRequested = true;
        }

        // Voltea al jugador según la dirección del movimiento
        Flip();
    }

    void FixedUpdate()
    {
        // Movimiento del jugador
        float moveSpeed = horizontalValue * speed;
        rigid.velocity = new Vector2(moveSpeed, rigid.velocity.y);

        // Detecta si el jugador está en el suelo
        isGrounded = Physics2D.OverlapCircle(detectionCenter.position, detectionRadius);

        // Actualiza los parámetros del Animator
        anim.SetBool("isRunning", Mathf.Abs(horizontalValue) > 0);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("YVelocity", rigid.velocity.y);

        if (jumpRequested)
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            jumpRequested = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(detectionCenter.position, detectionRadius);
    }

    private void Flip()
    {
        if (horizontalValue > 0 && sprt.flipX || horizontalValue < 0 && !sprt.flipX)
        {
            sprt.flipX = !sprt.flipX;
        }
    }
}
