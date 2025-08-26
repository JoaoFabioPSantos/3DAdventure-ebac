using Animation;
using Cloth;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    [SerializeField] private float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uIFillUpdater;

    public float damageMultiplier = 1f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUi();
    }

    protected virtual void Kill()
    {
        if (destroyOnKill)
        {
            Destroy(gameObject, 3f);
        }
        OnKill?.Invoke(this);
    }

    [NaughtyAttributes.Button]
    public void Damage()
    {
        Damage(5f);
    }

    public virtual void Damage(float f)
    {

        _currentLife -= f * damageMultiplier;

        if(_currentLife <= 0)
        {
            Kill();
        }
        UpdateUi();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    private void UpdateUi()
    {
        if(uIFillUpdater != null)
        {
            uIFillUpdater.ForEach(i=> i.UpdateValue((float)_currentLife/startLife));
        }
    }

    public void ChangeDamageMultiplier(float newDamageMultiplier, float duration)
    {
        StartCoroutine(ChangeDamageMultiplierCoroutine(newDamageMultiplier, duration));
    }

    IEnumerator ChangeDamageMultiplierCoroutine(float newDamageMultiplier, float duration)
    {
        damageMultiplier = newDamageMultiplier;
        yield return new WaitForSeconds(duration);
        damageMultiplier = 1;
    }
}
