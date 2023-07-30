using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerAimWeapon : MonoBehaviour
{
    public event EventHandler<OnShootEventArgs> onShoot;
    public class OnShootEventArgs : EventArgs
    {
        public Vector3 gunEndPointPosition;
        public Vector3 shootPosition;
    }

    private Transform aimTransform;
    private Transform aimGunEndPointTransform;
    private Animator aimAnimator;

   
    public GameObject projectilePrefab;


    private void Awake()
    {
        aimTransform = transform.Find("Aim");
        aimAnimator = GetComponent<Animator>();

        aimGunEndPointTransform = aimTransform.Find("GunEndPointPosition");
    }


    private void Update()
    {
        HandleAiming();
        HandleShooting();
    }
   

    private void HandleAiming()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (!GetComponentInParent<MovementScript>().isFacingRight)
        {
            angle -= 180f;
        }

        aimTransform.eulerAngles = new Vector3(0, 0, angle);

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


    private void HandleShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
            Vector3 shootDirection = (mousePosition - aimGunEndPointTransform.position).normalized;

            GameObject projectile = Instantiate(projectilePrefab, aimGunEndPointTransform.position, Quaternion.identity);

            projectile.transform.right = shootDirection;

            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            if (projectileController != null)
            {
                //projectileController.damage = 10; // Adjust damage amount as needed
            }

            aimAnimator.SetTrigger("Shoot");

            onShoot?.Invoke(this, new OnShootEventArgs
            {
                gunEndPointPosition = aimGunEndPointTransform.position,
                shootPosition = mousePosition,
            });

            
        }
    }
}
