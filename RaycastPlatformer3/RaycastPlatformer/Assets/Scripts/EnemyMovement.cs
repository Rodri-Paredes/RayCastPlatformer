using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f; 
    private Rigidbody2D rb;
    private bool movingRight = false;  
    public LayerMask wallLayer;  
    private bool isActive = false;  
    private Renderer enemyRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<Renderer>();  
    }

    void Update()
    {
        if (enemyRenderer.isVisible)
        {
            isActive = true;  
        }

        if (isActive)
        {

            rb.velocity = new Vector2(moveSpeed * (movingRight ? 1 : -1), rb.velocity.y);

            RaycastHit2D hit = Physics2D.Raycast(transform.position, movingRight ? Vector2.right : Vector2.left, 0.5f, wallLayer);

            if (hit)
            {
                movingRight = !movingRight;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (movingRight ? Vector3.right : Vector3.left) * 0.5f);
    }
}
