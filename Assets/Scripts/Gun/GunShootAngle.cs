using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GunShootAngle : GunShootLimit
{
    public int amountPerShoot = 4;
    public float shootAngle = 15f;

    public override void Shoot()
    {
        int mult = 0;

        for(int i = 0; i < amountPerShoot; i++)
        {
            if(i%2== 0)
            {
                mult++;
            }

            var projectile = Instantiate(prefabProjectileBase, positionToShoot);
            projectile.transform.position = positionToShoot.position;
            projectile.transform.localEulerAngles = Vector3.zero + Vector3.up * (i%2 == 0 ? shootAngle:-shootAngle) * mult ;
            
            projectile.speed = projectile.speed = speed;
            projectile.transform.parent = null;
        }
    }
}
