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
        Debug.DrawLine(e.gunEndPointPosition, e.shootPosition, Color.red, .1f);
        CreateWeaponProjectile(e.gunEndPointPosition, e.shootPosition);
        CreateShootFlash(e.gunEndPointPosition);
    }


    private void CreateWeaponProjectile(Vector3 fromPosition, Vector3 targetPosition)
    {
        Vector3 dir = (targetPosition - fromPosition).normalized;
        float eulerZ = UtilsClass.GetAngleFromVectorFloat(dir) - 90;
        float distance = Vector3.Distance(fromPosition, targetPosition);
        Vector3 projectileSpawnPosition = fromPosition + dir * distance * .5f;

        Material tempWeaponProjectileMaterial = new Material(weaponProjectileMaterial);
        tempWeaponProjectileMaterial.SetTextureScale("_MainTex", new Vector2(1f, distance / 256f));

        WorldMesh worldMesh = WorldMesh.Create(projectileSpawnPosition, eulerZ, 6f, distance, tempWeaponProjectileMaterial, null, 10000);

        float timer = .1f;
        FunctionUpdater.Create(() =>
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                worldMesh.DestroySelf();

                return true;
            }

            return false;
        });
    }


    private void CreateShootFlash(Vector3 spawnPosition)
    {
        WorldSprite worldSprite = WorldSprite.Create(spawnPosition, shootFlashSprite);

        FunctionTimer.Create(worldSprite.DestroySelf, .1f);
    }
}
