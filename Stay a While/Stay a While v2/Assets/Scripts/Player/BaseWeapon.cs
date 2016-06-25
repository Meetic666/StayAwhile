﻿using UnityEngine;
using System.Collections;

public class BaseWeapon : MonoBehaviour 
{
    public int clipCount = 0;
    protected int MaxClipCount = 8;
    int ammoCount;
    protected int defAmmoCount = 24;
    protected int maxAmmoCount = 128;
    float FireCD;
    protected float FireRate = 0.2f;
    public bool reloading = false;
    public GameObject bulletPrefab;
    public float WeaponSpread = 0.5f;
    public AnimationClip reloadAnimClip;
    public AnimationClip idleAnimClip;
    public string WeaponName = "Pistol";
    Animation anim;
    void Start()
    {
        this.enabled = false;
        bulletPrefab = Resources.Load("Prefabs/Weapons/Bullets/BaseProjectile") as GameObject;

        anim = GetComponent<Animation>();
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
    }
    void Update()
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
                ObjectPool.Instance.Instantiate(bulletPrefab, gameObject.transform.position + transform.up * 0.3f, Quaternion.Euler(0.0f,0.0f,transform.eulerAngles.z/2.0f + Random.Range(-WeaponSpread,WeaponSpread)));
                FireCD = FireRate;
                clipCount--;

                //TODO: Fire event
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
