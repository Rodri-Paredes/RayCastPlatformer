using UnityEngine;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;

    public LayerMask wallLayer;
    public LayerMask enemyLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }

        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, wallLayer);

        if (isTouchingWall && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
        }

        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (rb.velocity.y < 0)
            {
                Destroy(collision.gameObject); 
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2);
            }
            else
            {
                RestartGame();
            }
        }
    }


    void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1f);
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * Mathf.Sign(transform.localScale.x) * 0.6f);
    }
}
