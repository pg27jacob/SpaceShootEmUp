using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    public float lifetime = 3f;
    public int damage = 1;

    private Vector3 direction;

    public void Initialize(Vector3 shootDirection)
    {
        direction = shootDirection.normalized;
        Destroy(gameObject, lifetime); // Destroy after some time
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Make sure your enemies have the "Enemy" tag
        {
            Destroy(other.gameObject); // Apply damage or destroy enemy
            Destroy(gameObject); // Destroy the projectile on hit
        }
    }
}
