using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : BaseProjectile 
{
    public float ExplosionRadius = 5.0f;
    public float MinDetTime = 0.5f;
    float defaultMinDetTime;
    public float TimeToDetonate = 3.0f;
    float defaultTimeToDetonate;
    public float SlowSpeed = 0.3f;
    float defaultSpeed = 0.0f;

    void Start()
    {
        defaultSpeed = projectileSpeed;
        defaultTimeToDetonate = TimeToDetonate;
    }

    void OnEnable()
    {
        if(defaultSpeed != 0.0f)
            projectileSpeed = defaultSpeed;
        if (defaultTimeToDetonate != 0.0f)
            TimeToDetonate = defaultTimeToDetonate;
        if (defaultMinDetTime != 0.0f)
            MinDetTime = defaultMinDetTime;
    }

    void Update()
    {
        TimeToDetonate -= Time.deltaTime;
        gameObject.transform.Translate(transform.up * projectileSpeed);
        if(projectileSpeed >= 0)
            projectileSpeed -= SlowSpeed;
        else
        {
            projectileSpeed += SlowSpeed;
        }
        if (MinDetTime > 0.0f)
        {
            MinDetTime -= Time.deltaTime;
        }
        else
        {

            RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, Radius, transform.up, 0.1f, maskToHit);
            if (hit == true || TimeToDetonate < 0.0f)
            {
                Vector2 point = Vector2.zero;
                if (hit == true)
                    point = hit.point;
                else
                    point = gameObject.transform.position;


                Collider2D[] colls = Physics2D.OverlapCircleAll(point, ExplosionRadius, maskToHit);
                for (int i = 0; i < colls.Length; i++)
                {

                    float Damage = DamageToDeal / (Vector2.Distance(point, colls[i].transform.position) < 1 ? 1 : Vector2.Distance(point, colls[i].transform.position));
                    if (colls[i].GetComponent<BaseEnemy>() != null)
                        colls[i].GetComponent<BaseEnemy>().DealDamage(Damage);
                    if (colls[i].GetComponent<BasePlayer>() != null)
                        colls[i].GetComponent<BasePlayer>().Damage(Damage);
                }


                gameObject.transform.position = new Vector3(float.MaxValue, float.MaxValue);
                gameObject.SetActive(false);
            }
        }
    }

}
