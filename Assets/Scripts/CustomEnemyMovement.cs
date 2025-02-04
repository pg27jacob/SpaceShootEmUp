using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour
{
    public float moveSpeed = 5f;           // Movement speed
    public float attackDistance = 2f;      // Attack range
    public float attackDelay = 2f;         // Time between attacks
    public float lockedYPosition = 0f;    // Locked Y position (adjust this value to whatever Y you want)

    private Transform player;              // Reference to the player's transform
    private float lastAttackTime;          // Time of the last attack
    private Rigidbody rb;                  // Rigidbody component for physics-based movement

    private void Start()
    {
        // Find the player by the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        if (player == null)
        {
            Debug.LogError("Player not found. Make sure the player has the tag 'Player'.");
            return;
        }

        // Get the Rigidbody component of the enemy
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found! Make sure the enemy has a Rigidbody.");
        }

        // Set up collisions to ignore with specific walls
        SetupWallCollisions();
    }

    private void Update()
    {
        if (player == null || rb == null) return;

        // Move towards the player using velocity
        MoveTowardsPlayer();

        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Attack if in range
        if (distanceToPlayer <= attackDistance && Time.time > lastAttackTime + attackDelay)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void MoveTowardsPlayer()
    {
        // Calculate direction from enemy to player
        Vector3 direction = (player.position - transform.position).normalized;

        // Lock the Y position by setting it to the desired value
        direction.y = 0;  // Ensure no movement in the Y-axis

        // Apply velocity to the Rigidbody to move towards the player
        rb.linearVelocity = direction * moveSpeed;

        // Keep the enemy locked on the specified Y position (optional)
        transform.position = new Vector3(transform.position.x, lockedYPosition, transform.position.z);

        // Rotate the enemy to face the player
        RotateTowardsPlayer(direction);
    }

    private void RotateTowardsPlayer(Vector3 direction)
    {
        // Calculate the rotation to face the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // Smoothly rotate the enemy towards the player
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    }

    private void Attack()
    {
        // Attack logic (e.g., raycast to check if the player is hit)
        Debug.Log("Attacking player!");

        // Example: Raycast to detect player in attack range
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                // Damage or destroy the player
                Destroy(hit.collider.gameObject);
                Debug.Log("Player destroyed!");
            }
        }
    }

    private void SetupWallCollisions()
    {
        // Example wall names to ignore
        GameObject[] walls = { GameObject.Find("TopWall"), GameObject.Find("RightWall"), GameObject.Find("LeftWall"), GameObject.Find("Cube"), GameObject.Find("CubeOne") };

        Collider enemyCollider = GetComponent<Collider>();

        foreach (GameObject wall in walls)
        {
            if (wall != null)
            {
                Collider wallCollider = wall.GetComponent<Collider>();
                if (enemyCollider != null && wallCollider != null)
                {
                    // Ignore collisions between the enemy and specific walls
                    Physics.IgnoreCollision(enemyCollider, wallCollider);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check for collision with the "BottomWall" or any other specific wall
        if (collision.gameObject.name == "BottomWall")
        {
            Destroy(gameObject);
            Debug.Log("Enemy destroyed by bottom wall!");
        }
    }
}
