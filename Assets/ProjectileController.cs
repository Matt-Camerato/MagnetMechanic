using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [Header("Projectile Settings")]
    [SerializeField] private float _throwPower;
    [SerializeField] private GameObject _projectilePrefab;

    private void OnEnable() => PlayerController.Throw += ThrowProjectile;
    private void OnDisable() => PlayerController.Throw -= ThrowProjectile;

    private void ThrowProjectile()
    {
        //create the projectile and apply an impulse force to it in the direction of the player's mouse
        GameObject projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
        Vector2 forceDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        projectile.GetComponent<Rigidbody2D>()?.AddForce(forceDir.normalized * _throwPower, ForceMode2D.Impulse);
    }
}
