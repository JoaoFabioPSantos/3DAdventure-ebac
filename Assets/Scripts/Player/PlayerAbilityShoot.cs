using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    [Header("Types Guns Reference")]
    public GunShootLimit firstSlotGun;
    public GunShootLimit secondSlotGun;

    [Header("PFB_Gun Reference")]
    public GameObject firstGun;
    public GameObject secondGun;

    [Header("Gun Preferences")]
    public Transform gunPosition;
    public GameObject uiGunNotChange;

    private GunShootLimit _currentGun;

    protected override void Init()
    {
        base.Init();
        CreateGun();

        firstSlotGun = firstGun.GetComponent<GunShootLimit>();
        secondSlotGun = secondGun.GetComponent<GunShootLimit>();

        //ctx = context
        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();
        inputs.Gameplay.SwitchWeapon1.performed += ctx => SwitchGunFirstSlot();
        inputs.Gameplay.SwitchWeapon2.performed += ctx => SwitchGunSecondSlot();
    }

    private void CreateGun()
    {
        firstGun = Instantiate(firstSlotGun.gameObject, gunPosition);
        firstGun.transform.localPosition = firstGun.transform.localEulerAngles = Vector3.zero;
        firstGun.SetActive(true); // Começa com a primeira ativa

        secondGun = Instantiate(secondSlotGun.gameObject, gunPosition);
        secondGun.transform.localPosition = secondGun.transform.localEulerAngles = Vector3.zero;
        secondGun.SetActive(false); // Começa desativada

        _currentGun = firstGun.GetComponent<GunShootLimit>();
    }

    private bool CheckSwitchGun()
    {
        if (_currentGun.CheckGunStatusToReload())
        {
            uiGunNotChange.SetActive(true);
            Invoke("CloseShowUiNotSwitch", 1f);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CloseShowUiNotSwitch()
    {
        uiGunNotChange.SetActive(false);
    }

    private void SwitchGunFirstSlot()
    {
        if(CheckSwitchGun())return;
        secondGun.SetActive(false);
        Debug.Log("First Weapon");
        firstGun.SetActive(true);
        _currentGun = firstGun.GetComponent<GunShootLimit>();
    }

    private void SwitchGunSecondSlot()
    {
        if (CheckSwitchGun()) return;
        firstGun.SetActive(false);
        Debug.Log("Second Weapon");
        secondGun.SetActive(true);
        _currentGun = secondGun.GetComponent<GunShootLimit>();
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
    }
    private void CancelShoot()
    {
        _currentGun.StopShoot();
    }
}
