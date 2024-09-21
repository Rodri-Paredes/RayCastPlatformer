using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement2D : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float jumpForce = 10f;  
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;

    public LayerMask wallLayer;  
    public LayerMask enemyLayer;  
    public LayerMask goalLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        //detectar si el jugador está tocando el suelo usando un raycast hacia abajo
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));

        //detectar colisiones con paredes usando un raycast hacia la dirección del movimiento
        isTouchingWall = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, wallLayer);

        //si el jugador está tocando una pared y no está en el suelo, resbala hacia abajo
        if (isTouchingWall && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, -2f);
        }

        //detectar colisión con el Goal usando un raycast hacia adelante
        RaycastHit2D hitGoal = Physics2D.Raycast(transform.position, Vector2.right * Mathf.Sign(moveInput), 0.6f, goalLayer);

        //si se detecta el Goal, cambiar a la winscene
        if (hitGoal.collider != null)
        {
            SceneManager.LoadScene("WinScene");
        }

        //saltar si el player está en el suelo
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
    }

    //detectar colisiones físicas con enemigos
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // si el jugador está cayendo sobre el enemigo, lo destruye
            if (rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce / 2); //rebote del jugador (estetica)
            }
            else
            {
                // si el enemigo toca al jugador desde el lado o arriba, reinicia el juego
                RestartGame();
            }
        }
    }

    //reiniciar el juego cargando la misma escena
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 1f);  
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * Mathf.Sign(transform.localScale.x) * 0.6f);
    }
}
