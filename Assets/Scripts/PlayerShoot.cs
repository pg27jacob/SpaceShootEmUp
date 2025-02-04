using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab; // Assign in Inspector
    public Transform shootPoint; // Empty GameObject at gun/barrel
    public float shootRange = 100f;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main;
    }

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnAttack();
        }
    }

    void OnAttack()
    {
        Ray ray = playerCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        Plane plane = new Plane(Vector3.up, transform.position);
        float distance;

        if (plane.Raycast(ray, out distance))
        {
            Vector3 targetPoint = ray.GetPoint(distance);
            Vector3 shootDirection = (targetPoint - shootPoint.position).normalized;

            // Instantiate the projectile and set its direction
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
            projectile.GetComponent<Projectile>().Initialize(shootDirection);
        }
    }
}
