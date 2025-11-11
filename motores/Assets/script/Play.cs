using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float velocidade = 40;
    public float forcaDoPulo = 4;

    private bool noChao = false;
    private bool andando = false;
    private bool Dano = false;

    private SpriteRenderer sprite;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // se o jogador levou dano, não deixa mover nem pular
        if (Dano) return;

        andando = false;

        // movimentação
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = true;
            andando = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(velocidade * Time.deltaTime, 0, 0);
            sprite.flipX = false;
            andando = true;
        }

        // pulo
        if (Input.GetKeyDown(KeyCode.Space) && noChao)
        {
            rb.AddForce(new Vector2(0, forcaDoPulo), ForceMode2D.Impulse);
        }

        // animações
        animator.SetBool("Andando", andando);
        animator.SetBool("Pulo", !noChao);
        animator.SetBool("Dano", Dano);
    }

    void OnCollisionEnter2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = true;
        }
    }

    void OnCollisionExit2D(Collision2D colisao)
    {
        if (colisao.gameObject.CompareTag("Chao"))
        {
            noChao = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Perigo"))
        {
            Dano = true;
            animator.SetBool("Dano", true);
            rb.linearVelocity = Vector2.zero;
            
        }
    }
}
