using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour 
{
    public float lifeTime = 5;
    public float DamageToDeal = 15.0f;
    protected float Radius = 0.3f;
    public float projectileSpeed = 0.5f;
    public LayerMask maskToHit = 1 << 9;
    public bool Piercing = false;

    float m_LifeTimer;

    void OnEnable()
    {
        m_LifeTimer = lifeTime;
    }

    void Update()
    {
        gameObject.transform.Translate(transform.up * projectileSpeed);

        RaycastHit2D hit = Physics2D.CircleCast(gameObject.transform.position, Radius, transform.up, 0.1f, maskToHit);
        if (hit == true)
        {
            if (hit.collider.gameObject.layer == 9)
            {
                hit.collider.GetComponent<BaseEnemy>().DealDamage(DamageToDeal);
            }

            if (hit.collider.gameObject.layer == 8)
            {
                hit.collider.GetComponent<BasePlayer>().Damage(DamageToDeal);
            }

            if (Piercing == true)
            {
                gameObject.transform.position = new Vector3(float.MaxValue, float.MaxValue);
                gameObject.SetActive(false);
            }
        }
        m_LifeTimer -= Time.deltaTime;
        if (m_LifeTimer <= 0)
        {
            gameObject.SetActive(false);
        }
    }

}
