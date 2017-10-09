using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb;
    private bool alive = true;
    private BoxCollider2D col;

    // Use this for initialization
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Instance.transform.position.x < transform.position.x && Vector3.Distance(transform.position, Player.Instance.transform.position) < 2f)
        {
            animator.SetTrigger("throw");
        }
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector2.left * 400);
    }

    public void Hitted()
    {
        if (alive)
        {
            Die();
        }
    }

    public void Throw()
    {
        if(Player.Instance.transform.position.x < transform.position.x && Vector3.Distance(transform.position, Player.Instance.transform.position) < 1f)
        {
            Player.Instance.CurrentHealth--;
        }
    }

    private void Die()
    {
        animator.SetTrigger("die");
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        alive = false;
        GameManager.Instance.EnemiesKilled++;
        col.size = new Vector2(col.size.y, col.size.x);
        col.offset = new Vector2(-6.5f, 2);
    }
}
