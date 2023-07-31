using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WeaponEffects : MonoBehaviour
{
    [SerializeField] private PlayerAimWeapon playerAimWeapon;
    [SerializeField] private Material weaponProjectileMaterial;
    [SerializeField] private Sprite shootFlashSprite;


    private void Start()
    {
        playerAimWeapon.onShoot += PlayerAimWeapon_OnShoot;
    }


    private void PlayerAimWeapon_OnShoot(object sender, PlayerAimWeapon.OnShootEventArgs e)
    {
        UtilsClass.ShakeCamera(.02f, .1f);
        CreateShootFlash(e.gunEndPointPosition);
    }


    private void CreateShootFlash(Vector3 spawnPosition)
    {
        WorldSprite worldSprite = WorldSprite.Create(spawnPosition, shootFlashSprite);

        FunctionTimer.Create(worldSprite.DestroySelf, .1f);
    }
}
