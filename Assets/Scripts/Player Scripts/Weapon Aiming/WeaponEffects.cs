using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private Sprite shootFlashSprite;

    private void Start()
    {
        // Subscribe to the "onShoot" event of the PlayerAimWeapon script
        playerAimWeapon.onShoot += PlayerAimWeapon_OnShoot;
    }

    // Event handler for the "onShoot" event of the PlayerAimWeapon script
    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        // Shake the camera when the player shoots
        UtilsClass.ShakeCamera(.02f, .1f);

        // Create a shoot flash effect at the gun's endpoint position
        CreateShootFlash(e.gunEndPointPosition);
    }

    // Create a shoot flash effect at the specified spawn position
    private void CreateShootFlash(Vector3 spawnPosition)
    {
        // Create a WorldSprite at the spawn position with the shoot flash sprite
        WorldSprite worldSprite = WorldSprite.Create(spawnPosition, shootFlashSprite);

        // Destroy the WorldSprite after a delay of 0.1 seconds
        FunctionTimer.Create(worldSprite.DestroySelf, .1f);
    }
}
