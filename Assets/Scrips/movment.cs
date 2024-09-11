using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	Rigidbody2D rigid;
	Animator anim;
	SpriteRenderer sprt;

	float horizontalValue;
	[SerializeField] float speed;
	[SerializeField] float jumpForce;

	[SerializeField] Transform detectionCenter;
	[SerializeField] float detectionRadius;

	[SerializeField] Collider2D[] collisions;
	[SerializeField] bool isGrounded;
	bool jumpActivated = false;

	// Start is called before the first frame update
	void Start()
	{
		rigid = GetComponent<Rigidbody2D>();
		anim = GetComponent<Animator>();
		sprt = GetComponent<SpriteRenderer>();
	}

	void Update() //SE EJECUTA SEGUN EL NUMERO DE FRAMES QUE PUEDA PROCESAR EL COMPUTADOR
	{
		horizontalValue = Input.GetAxis("Horizontal");
		anim.SetBool("InMovement", horizontalValue != 0 ? true : false);

		if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
		{
			jumpActivated = true;
		}

		Flip();
	}

	private void FixedUpdate() //SE EJECUTA UN NUMERO DE VECES FIJAS SIN IMPORTAR EL COMPUTADOR
	{
		rigid.velocity = new Vector2(horizontalValue * speed, rigid.velocity.y);
		anim.SetFloat("YVelocity", rigid.velocity.y);

		collisions = Physics2D.OverlapCircleAll(detectionCenter.position, detectionRadius);

		if (collisions.Length > 0)
		{
			isGrounded = true;
		}

		if (collisions.Length == 1 && collisions[0].gameObject == gameObject)
		{
			isGrounded = false;
		}

		anim.SetBool("IsGrounded", isGrounded);

		if (jumpActivated == true)
		{
			rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
			anim.SetTrigger("Jump");
			jumpActivated = false;
		}

	}

	private void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(detectionCenter.position, detectionRadius);
	}

	public void Flip()
	{
		if (horizontalValue > 0 && sprt.flipX == true || horizontalValue < 0 && sprt.flipX == false)
		{
			sprt.flipX = !sprt.flipX;
		}
	}
}