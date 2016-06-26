using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private List<Transform> obstacles;
    [SerializeField]
    private GameObject Clown;
    [SerializeField]
    private GameObject FootBallPlayer;
    [SerializeField]
    private GameObject Octopus;
    [SerializeField]
    private GameObject Nurse;
        
    [Space(10)]
    public Type spawnType;
    public enum Type
    {
        Clown = 0,
        Tommy,
        Octopus,
        Nurse,
        None
    }

    [SerializeField]
    private float range;
    [SerializeField]
    private float spawnDelay;
    [SerializeField]
    private int waveSize;
    private int numberOfWaves;
    private bool endlessSpawn;
    private GameObject prefab;

    void Start()
    {
        switch(spawnType)
        {
            case Type.Clown: prefab = Clown; break;
            case Type.Tommy: prefab = FootBallPlayer; break;
            case Type.Octopus: prefab = Octopus; break;
            case Type.Nurse: prefab = Nurse; break;
            default: prefab = Clown; break;
        }
        ObjectPool.Instance.RegisterPrefab(prefab, 20);
        SpawnWave(waveSize, spawnDelay, true);
    }

    private IEnumerator test_cr()
    {
        while(true)
        {
            SpawnWave();
            yield return new WaitForSeconds(1);
        }
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
        for(int i = 0; i < numberOfWaves; i++)
        {
            for(int j = 0; j < waveSize; j++)
            {
                Vector3 pos = Vector3.zero;
                pos.x = Random.Range(-range, range);
                pos.y = Random.Range(-range, range);
                pos += this.transform.position;

                for(int k = 0; k < obstacles.Count; k++)
                {
                    if(Vector3.Distance(obstacles[k].position, pos) <= obstacles[k].GetComponent<CircleCollider2D>().radius * obstacles[k].localScale.x)
                    {
                        pos = Vector3.zero;
                        pos.x = Random.Range(-range, range);
                        pos.y = Random.Range(-range, range);
                        pos += this.transform.position;
                        k = -1;
                    }
                }

                ObjectPool.Instance.Instantiate(prefab, pos, Quaternion.identity);
            }
            
            if (endlessSpawn) { i--; }
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    public void ChangeSpawnType(Type type)
    {
        switch(type)
        {
            case Type.Clown: prefab = Clown; break;
            case Type.Tommy: prefab = FootBallPlayer; break;
            case Type.Octopus: prefab = Octopus; break;
            case Type.Nurse: prefab = Nurse; break;
            default: break;
        }
    }
}
