using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyResistant : EnemieBase
{
    public override void OnDamage(float f)
    {
        if (flashColor != null) flashColor.Flash();
        if (enemyParticleSystem != null) enemyParticleSystem.Emit(15);

        if(f == 2) _currentLife -= 0.5f; Debug.Log("Não é efetivo");
        if(f != 2)_currentLife -= f;

        if (_currentLife <= 0)
        {
            Kill();
        }
    }
}
