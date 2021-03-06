﻿using UnityEngine;
using System.Collections;

public class ShootEventData : EventData
{
    public string m_WeaponName;
    public Vector3 m_ShotPosition;
}

public class BaseWeapon : MonoBehaviour 
{
    public int clipCount = 0;
    protected int MaxClipCount = 8;
    public int ammoCount;
    protected int defAmmoCount = 24;
    protected int maxAmmoCount = 128;
    protected float FireCD;
    protected float FireRate = 0.2f;
    public bool reloading = false;
    public GameObject bulletPrefab;
    public float WeaponSpread = 0.5f;
    AnimationClip reloadAnimClip;
    AnimationClip idleAnimClip;
    public string WeaponName = "Base";
    protected Animation anim;

    protected ShootEventData m_ShootEventData;

    public virtual void Init(int ClipCount, int DefAmmoCount, int MaxAmmo, float newFireRate, float newWeaponSpread, string newWeaponName)
    {
        MaxClipCount = ClipCount;
        defAmmoCount = DefAmmoCount;
        maxAmmoCount = MaxAmmo;
        FireRate = newFireRate;
        WeaponSpread = newWeaponSpread;
        WeaponName = newWeaponName;
    }

    protected virtual void Start()
    {
        this.enabled = false;
        bulletPrefab = Resources.Load("Prefabs/Weapons/Bullets/" + WeaponName + "Projectile") as GameObject;
        reloadAnimClip = Resources.Load("Animations/Weapons/Player" + GetComponent<BasePlayer>().playerNum + WeaponName + "Reload" ) as AnimationClip;
        idleAnimClip = Resources.Load("Animations/Weapons/Player" + GetComponent<BasePlayer>().playerNum + WeaponName + "Idle" ) as AnimationClip;
        anim = GetComponentInChildren<Animation>();
        for(int i = 0; i < GetComponent<BasePlayer>().Weapons.Count; i++)
        {
            if(GetComponent<BasePlayer>().Weapons[i].WeaponName == WeaponName)
            {
                GetComponent<BasePlayer>().Weapons[i].ammoCount += MaxClipCount;

                if(GetComponent<BasePlayer>().Weapons[i].ammoCount > maxAmmoCount)
                {
                    GetComponent<BasePlayer>().Weapons[i].ammoCount = maxAmmoCount;
                }

                Destroy(this);
                return;
            }

        }

        GetComponent<BasePlayer>().Weapons.Add(this);
        ammoCount = defAmmoCount;

        m_ShootEventData = new ShootEventData();

        m_ShootEventData.m_WeaponName = WeaponName;
    }
    protected virtual void Update()
    {
        if(FireCD > 0.0f)
        {
            FireCD -= Time.deltaTime;
        }
    }
    void OnEnable()
    {
        //anim.clip = idleAnimClip;
        if(bulletPrefab != null)
            ObjectPool.Instance.RegisterPrefab(bulletPrefab, MaxClipCount);
    }

    void OnDisable()
    {
        StopAllCoroutines();
    }


    public virtual void Fire()
    {
        if(FireCD <= 0.0f)
        {
            if (clipCount > 0)
            {
                Vector3 shotPosition = gameObject.transform.position + transform.up * 0.3f;

                GameObject newProjectile = ObjectPool.Instance.Instantiate(bulletPrefab, shotPosition, Quaternion.Euler(0.0f,0.0f,transform.eulerAngles.z/2.0f + Random.Range(-WeaponSpread,WeaponSpread)));

                FireCD = FireRate;
                clipCount--;

                m_ShootEventData.m_ShotPosition = shotPosition;

                EventManager.Instance.SendEvent(m_ShootEventData);
            }
            else
            {
                StartCoroutine(ReloadCR());
            }
        }
    }
    

    public IEnumerator ReloadCR()
    {
        if (clipCount != MaxClipCount)
        {
            if (reloading == false)
            {
                reloading = true;


                //anim.clip = reloadAnimClip;

                //yield return new WaitForSeconds(reloadAnimClip.length);

                clipCount = ammoCount < MaxClipCount ? ammoCount : MaxClipCount;
                ammoCount -= clipCount;

                reloading = false;
                //anim.clip = idleAnimClip;
                yield return null;
            }
        }

    }

}
