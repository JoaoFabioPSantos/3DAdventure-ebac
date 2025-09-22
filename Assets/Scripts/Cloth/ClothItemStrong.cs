using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cloth
{
    public class ClothItemStrong : ClothItemBase
    {
        public float newDamageMultiplier = .5f;

        public override void Collect()
        {
            base.Collect();
            Player.Instance.healthBase.ChangeDamageMultiplier(newDamageMultiplier);
        }
    }
}

