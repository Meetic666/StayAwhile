using UnityEngine;
using System.Collections;

public enum BasicWeapon
{
    Pistol,
    SMG,
    AssaultRifle,
    Crossbow,
    MissileLauncher,
    Grenades,
    Airstrike,
    Katana
}

public class AddBasicWeapon : Triggerable 
{
    public BasicWeapon weapon;

    protected override void TriggerEffect()
    {
        base.TriggerEffect();
        for( int i = 0; i < ObjectSingleton.Instance.playerList.Count; i++)
        {

            switch (weapon)
            {

                case BasicWeapon.Pistol:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>();
                    break;
                case BasicWeapon.SMG:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>().Init(35, 70, 175, 0.02f, 15f, "SMG");
                    break;
                case BasicWeapon.AssaultRifle:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>().Init(25, 25, 75, 0.1f, 1.5f, "AR");
                    break;
                case BasicWeapon.Crossbow:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>().Init(1, 3, 10, 0.5f, 0.3f, "XBow");
                    break;
                case BasicWeapon.Grenades:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>().Init(1, 1, 5, 2.0f, 3.0f, "Grenade");
                    break;
                case BasicWeapon.MissileLauncher:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>().Init(1, 1, 1, 1.0f, 0.0f, "Missile");
                    break;
                case BasicWeapon.Airstrike:
                    ObjectSingleton.Instance.playerList[i].AddComponent<BaseWeapon>().Init(1, 1, 1, 0f, 5.0f, "Airstrike");
                    break;
                case BasicWeapon.Katana:
                    ObjectSingleton.Instance.playerList[i].AddComponent<Katana>();
                    break;
            }


        }

        gameObject.SetActive(false);
    }

}
