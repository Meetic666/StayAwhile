using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
[System.Serializable]
public struct AttackRay
{
    public Vector2 startPoint;
    public Vector2 endPoint;
    public float Damage;
}
public class Katana : BaseWeapon 
{
    public List<AttackRay> AttackSequence;
    int attackIndex = 0;
    public float RestartTime;
    float restartTimer;
    public LayerMask maskToHit = 1 << 9;
    public bool HideGizmos = false;
    protected override void Start()
    {
        base.Start();
        FireRate = 0.2f;
    }

    public override void Fire()
    {
        if (FireCD > 0.0f)
        {
        }
        else
        {
            restartTimer = RestartTime;
            Vector2 dir = transform.TransformPoint(AttackSequence[attackIndex].startPoint) - transform.TransformPoint(AttackSequence[attackIndex].endPoint);
            RaycastHit2D[] hits = Physics2D.RaycastAll((Vector2)transform.TransformPoint(AttackSequence[attackIndex].startPoint),dir.normalized, Vector2.Distance(transform.TransformPoint(AttackSequence[attackIndex].startPoint), transform.TransformPoint(AttackSequence[attackIndex].endPoint)), maskToHit);

            if (hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    hits[i].collider.gameObject.GetComponent<BaseEnemy>().DealDamage(AttackSequence[attackIndex].Damage);
                    
                }
            }

            Debug.DrawLine(transform.TransformPoint(AttackSequence[attackIndex].startPoint), transform.TransformPoint(AttackSequence[attackIndex].endPoint), Color.red, 0.5f);
            attackIndex++;
            FireCD = FireRate;
            if (attackIndex >= AttackSequence.Count)
            {
                FireCD = FireRate * 3.0f;
                attackIndex = 0;
            }
            restartTimer = RestartTime;

            
        }
    }


    protected override void Update()
    {
        if(restartTimer > 0.0f)
        {
            restartTimer -= Time.deltaTime;
        }
        else
        {
            attackIndex = 0;
        }

        if(FireCD > 0.0f)
        {
            FireCD -= Time.deltaTime;
        }
    }

    #if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if(Selection.Contains(gameObject) && HideGizmos == false)
            for(int i = 0; i < AttackSequence.Count; i++)
            {
                if( i % 3 == 0)
                {
                    Gizmos.color = Color.red;
                }
                else if (i % 3 == 1)
                {
                    Gizmos.color = Color.yellow;
                }
                else
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawLine(transform.TransformPoint(AttackSequence[i].startPoint), transform.TransformPoint(AttackSequence[i].endPoint));


            }
    }
#endif

}
