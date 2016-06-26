using UnityEngine;
using System.Collections;

public class ExplosiveProjectile : BaseProjectile 
{
    public float ExplosionRadius = 5.0f;
    public float m_ExplosionTime = 1.0f;
    public float MinDetTime = 0.5f;
    float defaultMinDetTime;
    public float TimeToDetonate = 3.0f;
    float defaultTimeToDetonate;
    public float SlowSpeed = 0.3f;
    float defaultSpeed = 0.0f;
    Animator animator;

    float m_ExplosionTimer;

    ShootEventData m_EventData;

    void Start()
    {
        animator = GetComponent<Animator>();
        defaultSpeed = projectileSpeed;
        defaultTimeToDetonate = TimeToDetonate;

        m_EventData = new ShootEventData();
        m_EventData.m_WeaponName = "Explosion";
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
            if(m_ExplosionTimer <= 0.0f)
            {
                if (TimeToDetonate < 0.0f)
                {
                    if(animator)
                    {
                        animator.SetTrigger("Death");
                    }

                    RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, Radius, transform.up, 0.1f, maskToHit);
                    if (hit == true)
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
                    }

                    EventManager.Instance.SendEvent(m_EventData);

                    m_ExplosionTimer = m_ExplosionTime;
                }
            }
            else
            {
                m_ExplosionTimer -= Time.deltaTime;

                if (m_ExplosionTimer <= 0.0f)
                {
                    Death();
                }
            }
        }
    }

    private void Death()
    {
        gameObject.transform.position = new Vector3(float.MaxValue, float.MaxValue);
        gameObject.SetActive(false);
    }
}
