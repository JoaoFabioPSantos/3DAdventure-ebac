using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase firstSlotGun;
    public GunBase secondSlotGun;
    
    public GameObject firstGun;
    public GameObject secondGun;

    public Transform gunPosition;

    private GunBase _currentGun;

    protected override void Init()
    {
        base.Init();
        CreateGun();

        firstSlotGun = firstGun.GetComponent<GunBase>();
        secondSlotGun = secondGun.GetComponent<GunBase>();

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

        _currentGun = firstGun.GetComponent<GunBase>();
    }

    private void SwitchGunFirstSlot()
    {
        secondGun.SetActive(false);
        Debug.Log("First Weapon");
        firstGun.SetActive(true);
        _currentGun = firstGun.GetComponent<GunBase>();
    }

    private void SwitchGunSecondSlot()
    {
        firstGun.SetActive(false);
        Debug.Log("Second Weapon");
        secondGun.SetActive(true);
        _currentGun = secondGun.GetComponent<GunBase>();
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
