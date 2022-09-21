using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool grounded;
    public Transform groundCheck;
    public float rad;
    public LayerMask ground;
    bool faceRight = true;
    SpriteRenderer sprite;
    Animator anim;
    const string IDLE_ANIM = "idle";
    const string WALK_ANIM = "walk";
    const string JUMP_ANIM = "jump";
    string currAnim;
    public GameObject bullet;
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        PlayerJump();
        ShootBullet();
    }
    void MovePlayer()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
        if (faceRight && Input.GetKey(KeyCode.A))
        {
            faceRight = false;
            sprite.flipX = true;
        }
        else if (!faceRight && Input.GetKey(KeyCode.D))
        {
            faceRight = true;
            sprite.flipX = false;
        }
        if (rb.velocity.x!=0 && rb.velocity.y==0)
        {
            PlayAnim(WALK_ANIM);
        }
        else if (rb.velocity.x==0 && rb.velocity.y==0)
        {
            PlayAnim(IDLE_ANIM);
        }

    }

    void PlayerJump()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, rad, ground);
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            PlayAnim(JUMP_ANIM);
        }
    }

    void PlayAnim(string nextAnim)
    {
        if (currAnim == nextAnim)
        {
            return;
        }
        anim.Play(nextAnim);
        currAnim = nextAnim;
    }
    void ShootBullet()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bullet, transform.position, Quaternion.identity);
            if(faceRight)
            {
                bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, 0);
            }
            else
            {
                bulletClone.GetComponent<Rigidbody2D>().velocity = new Vector2(-bulletSpeed, 0);
            }
            Destroy(bulletClone, 3);
        }
    }
}
