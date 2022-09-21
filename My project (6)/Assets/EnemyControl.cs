using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyControl : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public Transform groundCheck;
    public float rad;
    public LayerMask ground;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Patrol();
    }
    void Patrol()
    {
        rb.velocity = new Vector2(speed, 0);
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, rad, ground);
        if(!isGrounded)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            speed *= -1;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            SceneManager.LoadScene(0);
        }
    }
}
