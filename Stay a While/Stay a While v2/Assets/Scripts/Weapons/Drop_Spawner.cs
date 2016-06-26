using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Drop_Spawner : MonoBehaviour
{
    private Type spawnType;
    private enum Type
    {
        Pistol = 0,
        SMG,
        AssaultRifle,
        CrossBow,
        MissileLauncher,
        Gernades,
        AirStrike,
        Katana
    }

    [SerializeField]
    private float range;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private int waveSize;
    private int numberOfWaves;
    private bool endlessSpawn;
    [SerializeField]
    private GameObject prefab;

    void Start()
    {
        spawnType = (Type)Random.Range(0, 8);
        switch (spawnType)
        {
            //case Type.Pistol: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Pistol; break;
            case Type.SMG: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.SMG; break;
            case Type.AssaultRifle: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.AssaultRifle; break;
            case Type.CrossBow: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Crossbow; break;
            case Type.MissileLauncher: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.MissileLauncher; break;
            case Type.Gernades: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Grenades; break;
            //case Type.AirStrike: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Airstrike; break;
            //case Type.Katana: prefab.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Katana; break;
            default: break;
        }
        ObjectPool.Instance.RegisterPrefab(prefab, 20);
        SpawnWave(waveSize, spawnDelay, true);
    }

    public void SpawnWave(int size = 1)
    {
        waveSize = size;
        numberOfWaves = 1;
        StartCoroutine(spawnWave_cr());
    }

    public void SpawnWave(int size, int numOfWaves, float delay)
    {
        waveSize = size;
        numberOfWaves = numOfWaves;
        spawnDelay = delay;
        StartCoroutine(spawnWave_cr());
    }

    public void SpawnWave(int size, float delay, bool endless = true)
    {
        waveSize = size;
        spawnDelay = delay;
        endlessSpawn = endless;
        numberOfWaves = 2;
        StartCoroutine(spawnWave_cr());
    }

    private IEnumerator spawnWave_cr()
    {
        for (int i = 0; i < numberOfWaves; i++)
        {
            for (int j = 0; j < waveSize; j++)
            {
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-range, range);
                pos.y = Random.Range(-range, range);
                pos += this.transform.position;

                pos.z = SpriteLayerConstants.PICK_UP_SPRITE_LAYER;

                GameObject box = ObjectPool.Instance.Instantiate(prefab, pos, Quaternion.identity);
                spawnType = (Type)Random.Range(0, 8);
                switch (spawnType)
                {
                    //case Type.Pistol: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Pistol; break;
                    case Type.SMG: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.SMG; break;
                    case Type.AssaultRifle: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.AssaultRifle; break;
                    case Type.CrossBow: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Crossbow; break;
                    case Type.MissileLauncher: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.MissileLauncher; break;
                    case Type.Gernades: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Grenades; break;
                    //case Type.AirStrike: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Airstrike; break;
                    //case Type.Katana: box.GetComponent<AddBasicWeapon>().weapon = BasicWeapon.Katana; break;
                    default: break;
                }
            }

            if (endlessSpawn) { i--; }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
