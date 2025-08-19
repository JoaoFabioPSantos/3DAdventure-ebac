using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIGunUpdater> uIGunUpdaters;

    public float maxShoot = 5f;
    public float timeToCharge = 1f;

    private float _currentShoots;
    private bool _recharging = false;
    private void Awake()
    {
        StartCoroutine(RechargeCoroutine());
        GetAllUIs();
    }

    public void ResetBullets()
    {
        _currentShoots = 0;
    }

    protected override IEnumerator ShootCoroutine()
    {
        if(_recharging) yield break;

        while (true)
        {
            if (_currentShoots < maxShoot)
            {
                Shoot();
                _currentShoots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoots);
            }
            else
            {
                CheckRecharge();
                yield return 0.001f;
            }
        }
    }

    private void CheckRecharge()
    {
        if (_currentShoots >= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    private void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time < timeToCharge)
        {
            time += Time.deltaTime;
            uIGunUpdaters.ForEach(i => i.UpdateValue(time/timeToCharge));
            yield return new WaitForEndOfFrame();
        }
        ResetBullets();
        _recharging = false;
    }

    private void UpdateUI()
    {
        uIGunUpdaters.ForEach(i => i.UpdateValue(maxShoot, _currentShoots));
    }

    private void GetAllUIs()
    {
        uIGunUpdaters = GameObject.FindObjectsOfType<UIGunUpdater>().ToList();
    }
}
