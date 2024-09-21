using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    private bool movingRight = false;  
    public LayerMask wallLayer;  // Capa de las paredes para detectar colisiones
    private bool isActive = false; 
    private Renderer enemyRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Activar el movimiento cuando el enemigo es visible en la cámara
        if (enemyRenderer.isVisible)
        {
            isActive = true;
        }

        // Si el enemigo está activo, empieza a moverse
        if (isActive)
        {
            rb.velocity = new Vector2(moveSpeed * (movingRight ? 1 : -1), rb.velocity.y);

            // Usar raycast para detectar colisiones con paredes
            RaycastHit2D hit = Physics2D.Raycast(transform.position, movingRight ? Vector2.right : Vector2.left, 0.5f, wallLayer);

            if (hit)
            {
                // Cambiar de dirección si el raycast detecta una pared
                movingRight = !movingRight;
            }
        }
        else
        {
            // Si el enemigo no es visible, no se mueve
            rb.velocity = Vector2.zero;
        }
    }

    // Detectar colisiones físicas con otros enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Cambiar de dirección si colisiona con otro enemigo
            movingRight = !movingRight;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (movingRight ? Vector3.right : Vector3.left) * 0.5f);
    }
}
