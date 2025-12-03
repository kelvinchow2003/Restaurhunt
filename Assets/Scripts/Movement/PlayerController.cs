using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator; // Optional: for animations later

    [Header("Combat Settings")]
    public Transform attackPoint; // An empty child object where the attack happens
    public float attackRange = 0.5f;
    public LayerMask enemyLayers; // Layers that count as enemies
    public int attackDamage = 20;
    public float attackRate = 2f; // Attacks per second
    private float nextAttackTime = 0f;

    private Vector2 movement;

    // Update is called once per frame (Input goes here)
    void Update()
    {
        // 1. Input Handling
        movement.x = Input.GetAxisRaw("Horizontal"); // Returns -1 (Left), 0, or 1 (Right)
        movement.y = Input.GetAxisRaw("Vertical");   // Returns -1 (Down), 0, or 1 (Up)

        // Normalize prevents diagonal movement from being faster than straight movement
        movement = movement.normalized;

        // 2. Animation params (If you have an Animator)
        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }

        // 3. Attack Input (Left Click or Space)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (Time.time >= nextAttackTime)
            {
                Attack();
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    // FixedUpdate is called 50 times a second (Physics goes here)
    void FixedUpdate()
    {
        // Move the Rigidbody
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        // Play an attack animation
        if(animator != null) animator.SetTrigger("Attack");

        // Detect enemies in range of the attack point
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Damage them
        foreach(Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit " + enemy.name);
            
            // We will create this script in Step 3!
            // enemy.GetComponent<EnemyAI>().TakeDamage(attackDamage); 
        }
    }

    // Visualize the attack range in the Editor (Gizmos)
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}