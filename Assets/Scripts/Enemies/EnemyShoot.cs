using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyShoot : EnemieBase
    {
        public GunBase gunBase;

        protected override void Init()
        {
            base.Init();

            gunBase.StartShoot();
        }
    }
}
