using UnityEngine;

public enum HorizontalProjectileDirections {Left, Right, None};
public enum VerticalProjectileDirections { Up, Down, None};

public class PlayerProjectileBehavior : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float launchForce = 1f; // The force to apply to the projectile
    public float projectileLifespan = 2f;
    public HorizontalProjectileDirections horizontalProjectileDirection = HorizontalProjectileDirections.Right;
    public VerticalProjectileDirections verticalProjectileDirections = VerticalProjectileDirections.None;
    public float projectileVerticalSpeed = 1f;

    [Header("Projectile References")]
    public GameObject projectileGameObject;
    public GameObject projectileRangeCheckGameObject;

    Rigidbody2D rb;
    Vector2 launchDirection = Vector2.right; // Default direction (to the right)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (horizontalProjectileDirection == HorizontalProjectileDirections.Left)
        {
            launchDirection = Vector2.left;
            projectileGameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        if ((verticalProjectileDirections == VerticalProjectileDirections.Up && horizontalProjectileDirection == HorizontalProjectileDirections.Right) || (verticalProjectileDirections == VerticalProjectileDirections.Down && horizontalProjectileDirection == HorizontalProjectileDirections.Left)) projectileGameObject.transform.rotation = Quaternion.Euler(0, 0, -45);
        else if ((verticalProjectileDirections == VerticalProjectileDirections.Down && horizontalProjectileDirection == HorizontalProjectileDirections.Right) || (verticalProjectileDirections == VerticalProjectileDirections.Up && horizontalProjectileDirection == HorizontalProjectileDirections.Left)) projectileGameObject.transform.rotation = Quaternion.Euler(0, 0, 45);

    }

    void Launch ()
    {
        // Apply force to the Rigidbody2D in the specified direction
        rb.MovePosition(rb.position + launchForce * Time.deltaTime * launchDirection);
    }

    void ApplyProjectileVerticalSpeed ()
    {
        if (verticalProjectileDirections == VerticalProjectileDirections.Up || verticalProjectileDirections == VerticalProjectileDirections.Down) projectileGameObject.transform.position += new Vector3(0, projectileVerticalSpeed * Time.deltaTime);
        // Logic that simulates hitting floor
        if (projectileGameObject.transform.position.y < projectileRangeCheckGameObject.transform.position.y) DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (projectileLifespan > 0f) projectileLifespan -= Time.deltaTime;
        else DestroyProjectile();
        
        ApplyProjectileVerticalSpeed();
    }

    private void FixedUpdate()
    {
        Launch();
    }
}
