using UnityEngine;

public enum FiringTypes {Semi, Fully}
public enum ReloadTypes {Clip, Round}
public class WeaponController : MonoBehaviour
{
    [Header("Gun Stats")]
    public FiringTypes firingType = FiringTypes.Semi;
    public ReloadTypes reloadType = ReloadTypes.Clip;
    public float firingRate = 0.5f;
    public int clipSize = 6;
    public float reloadTime = 2f;

    [Header("Projectile Settings")]
    public GameObject projectile;
    public GameObject projectileSpawnLocation;
    public float projectileSpeed = 14f;
    public float projectileLifeSpan = 0.5f;
    public float projectileVerticalSpeed = 5f;

    PlayerController playerController;
    PlayerAimController playerAimController;
    int currentClip;
    float reloadTimeLeft;
    float firingRateTimeLeft;

    void HandleShootInput()
    {
        float aimUpOrDownValue = playerAimController.AimUpOrDownAction.ReadValue<float>();
        bool playerShouldAim = playerAimController.GetShouldAim();
        if(firingType == FiringTypes.Semi)
        {
            if(playerShouldAim && Mathf.Approximately(aimUpOrDownValue,0) && playerAimController.ShootAction.WasPressedThisFrame() && firingRateTimeLeft <= 0)
            {
                Shoot();
                firingRateTimeLeft = firingRate;
            }
            else if(playerShouldAim && Mathf.Approximately(aimUpOrDownValue,1) && playerAimController.ShootAction.WasPressedThisFrame() && firingRateTimeLeft <= 0)
            {
                ShootUp();
                firingRateTimeLeft = firingRate;
            }
            else if(playerShouldAim && Mathf.Approximately(aimUpOrDownValue,-1) && playerAimController.ShootAction.WasPressedThisFrame() && firingRateTimeLeft <= 0)
            {
                ShootDown();
                firingRateTimeLeft = firingRate;
            }
        }
    }

    void Shoot()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, projectileSpawnLocation.transform.position, Quaternion.identity);
        PlayerProjectileBehavior playerProjectileBehavior = instantiatedProjectile.GetComponent<PlayerProjectileBehavior>();
        if(!playerController.facingRight) playerProjectileBehavior.horizontalProjectileDirection = HorizontalProjectileDirections.Left;
        playerProjectileBehavior.launchForce = projectileSpeed;
        playerProjectileBehavior.projectileLifespan = projectileLifeSpan;
    }

    void ShootUp()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, projectileSpawnLocation.transform.position, Quaternion.identity);
        PlayerProjectileBehavior playerProjectileBehavior = instantiatedProjectile.GetComponent<PlayerProjectileBehavior>();
        if(!playerController.facingRight) playerProjectileBehavior.horizontalProjectileDirection = HorizontalProjectileDirections.Left;
        playerProjectileBehavior.launchForce = projectileSpeed;
        playerProjectileBehavior.projectileLifespan = projectileLifeSpan;
        playerProjectileBehavior.verticalProjectileDirection = VerticalProjectileDirections.Up;
        playerProjectileBehavior.projectileVerticalSpeed = projectileVerticalSpeed;
    }

    void ShootDown()
    {
        GameObject instantiatedProjectile = Instantiate(projectile, projectileSpawnLocation.transform.position, Quaternion.identity);
        PlayerProjectileBehavior playerProjectileBehavior = instantiatedProjectile.GetComponent<PlayerProjectileBehavior>();
        if(!playerController.facingRight) playerProjectileBehavior.horizontalProjectileDirection = HorizontalProjectileDirections.Left;
        playerProjectileBehavior.launchForce = projectileSpeed;
        playerProjectileBehavior.projectileLifespan = projectileLifeSpan;
        playerProjectileBehavior.verticalProjectileDirection = VerticalProjectileDirections.Down;
        playerProjectileBehavior.projectileVerticalSpeed = -projectileVerticalSpeed;
    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAimController = GetComponentInParent<PlayerAimController>();
        playerController = GetComponentInParent<PlayerController>();
        Debug.Log(playerAimController+" "+playerController);
    }
    
    // Update is called once per frame
    void Update()
    {
        HandleShootInput();
        if(firingRateTimeLeft > 0) firingRateTimeLeft -= Time.deltaTime;
    }
}
