using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Clown;
    [SerializeField]
    private GameObject FootBallPlayer;
        
    [SerializeField]
    [Space(10)]
    private Type spawnType;
    private enum Type
    {
        Clown = 0,
        Tommy,
        None
    }

    [SerializeField]
    private float range;
    private float spawnDelay;
    private int waveSize;
    private int numberOfWaves;
    private bool endlessSpawn;

    void Start()
    {
        StartCoroutine(test_cr());
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
            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-range, range);
            pos.y = Random.Range(-range, range);
            pos += this.transform.position;

            switch (spawnType)
            {
                case Type.Clown: Instantiate(Clown, pos, Quaternion.identity); break;
                case Type.Tommy: Instantiate(FootBallPlayer, pos, Quaternion.identity); break;
                default: break;
            }

            if (endlessSpawn) { i--; }
            if (i > 0) { yield return new WaitForSeconds(spawnDelay); }
            else { yield return null; }
        }
    }
}
