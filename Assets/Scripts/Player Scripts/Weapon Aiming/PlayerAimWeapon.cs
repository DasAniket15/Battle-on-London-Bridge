using System;
using UnityEngine;
using Utils;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAimWeapon : MonoBehaviour
{
    // Event to notify when shooting occurs
    public event EventHandler<OnShootEventArgs> onShoot;

    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    // Aim related transforms and components
    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Animator aimAnimator;

    // Minimum distance required for aiming
    [SerializeField] private float minAimDistance = 1.5f;

    // Projectile prefab to be instantiated when shooting
    public GameObject projectilePrefab;

    // Reference to the PauseMenu script
    public PauseMenu pauseMenu;

    private void Awake()
    {
        // Initialize aim transforms and animator
        aimTransform = transform.Find("Aim");
        aimAnimator = GetComponent<Animator>();
        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
    }

    private void Update()
    {
        // Only handle aiming and shooting if the game is not paused
        if (!PauseMenu.GamePaused)
        {
            HandleAiming();
            HandleShooting();
        }
    }

    private void HandleAiming()
    {
        // Get the mouse position in the world
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        // Calculate angle for aiming the weapon
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        // Adjust the angle based on character facing direction
        if (mousePosition.y > 0 && angle > 0)
        {
            if (!GetComponentInParent<MovementScript>().isFacingRight)
            {
                angle -= 180f;
            }

            // Apply the rotation to the aimTransform
            aimTransform.eulerAngles = new Vector3(0, 0, angle);

            // Flip the aimTransform's local scale based on the angle
            Vector3 aimLocalScale = Vector3.one;
            if (angle > 90 || angle < -90)
            {
                aimLocalScale.y = -1f;
            }
            else
            {
                aimLocalScale.y = +1f;
            }

            aimTransform.localScale = aimLocalScale;
        }
    }

    private void HandleShooting()
    {
        // Check for mouse button down to initiate shooting
        if (Input.GetMouseButtonDown(0) && !PauseMenu.GamePaused) // Check if the game is not paused
        {
            // Get the mouse position in the world
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

            // Calculate the distance between the mouse and player
            float distanceToPlayer = Vector3.Distance(mousePosition, transform.position);

            // Check if the player can shoot based on the distance and direction
            if (distanceToPlayer > minAimDistance && mousePosition.y >= transform.position.y)
            {
                // Calculate the direction in which the projectile will be shot
                Vector3 shootDirection = (mousePosition - transform.position).normalized;

                // Calculate the angle to apply to the projectile's rotation
                float projectile_angle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                aimGunEndPointTransform.eulerAngles = new Vector3(0, 0, projectile_angle);

                // Instantiate the projectile and set its initial position and rotation
                GameObject projectile = Instantiate(projectilePrefab, aimGunEndPointTransform.position, aimGunEndPointTransform.rotation);

                // Get the ProjectileController component from the instantiated projectile
                ProjectileController projectileController = projectile.GetComponent<ProjectileController>();

                // Trigger the "Shoot" animation
                aimAnimator.SetTrigger("Shoot");

                // Invoke the "onShoot" event to notify listeners of the shot event
                onShoot?.Invoke(this, new OnShootEventArgs
                {
                    gunEndPointPosition = aimGunEndPointTransform.position,
                    shootPosition = mousePosition,
                });
            }
            else
            {
                // Player cannot shoot in this direction, display a message for debugging.
                Debug.Log("Cannot shoot in this direction.");
            }
        }
    }
}
