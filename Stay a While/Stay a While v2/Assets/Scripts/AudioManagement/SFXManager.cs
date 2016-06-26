using UnityEngine;
using System.Collections;

[System.Serializable]
public class WeaponSFX
{
    public string m_WeaponName = "Base";
    public AudioClip m_ShotAudioclip;
    public AudioClip m_ReloadAudioClip;
}

public class SFXManager : MonoBehaviour
{
    public WeaponSFX[] m_PlayerWeaponSFXArray;
    public WeaponSFX[] m_EnemyWeaponSFXArray;

	// Use this for initialization
	void Start ()
    {
        EventManager.Instance.RegisterListener(typeof(ShootEventData), new GameEventDelegate(OnReceiveShootEvent));
	}

    void OnReceiveShootEvent(EventData data)
    {
        bool foundSound = false;

        ShootEventData shootData = (ShootEventData)data;

        foreach(WeaponSFX weaponSFX in m_PlayerWeaponSFXArray)
        {
            if(weaponSFX.m_WeaponName.Equals(shootData.m_WeaponName))
            {
                AudioSource.PlayClipAtPoint(weaponSFX.m_ShotAudioclip, shootData.m_ShotPosition);
                foundSound = true;
                break;
            }
        }

        if(!foundSound)
        {
            foreach (WeaponSFX weaponSFX in m_EnemyWeaponSFXArray)
            {
                if (weaponSFX.m_WeaponName.Equals(shootData.m_WeaponName))
                {
                    AudioSource.PlayClipAtPoint(weaponSFX.m_ShotAudioclip, shootData.m_ShotPosition);
                    foundSound = true;
                    break;
                }
            }
        }
    }
}
