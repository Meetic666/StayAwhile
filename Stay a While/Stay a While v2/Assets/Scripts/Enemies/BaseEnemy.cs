using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected GameObject Fuel;

    [SerializeField]
    protected string m_EnemyName;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float attackRange;
    [SerializeField]
    protected float attackSpeed;
    [SerializeField]
    protected float health;
    protected float healthMAX;

    protected State currentState;
    protected enum State
    {
        Moving = 0,
        Attacking,
        Dead,
        None,
        Spawned
    }
    protected int targetIndex;
    protected Transform target;
    protected Animator animator;
    protected List<GameObject> obstacles;

    ShootEventData m_ShootEventData;

    void Awake()
    {
        obstacles = new List<GameObject>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
        healthMAX = health;
        currentState = State.Spawned;
        StartCoroutine(update_cr());

        m_ShootEventData = new ShootEventData();
        m_ShootEventData.m_WeaponName = m_EnemyName; 
    }

    protected virtual IEnumerator update_cr()
    {
        yield return new WaitForSeconds(1);
        if (target == null)
        {
            targetIndex = Random.Range(0, ObjectSingleton.Instance.playerList.Count);
            target = ObjectSingleton.Instance.playerList[targetIndex].transform;
        }
        
        while(true)
        {
            switch (currentState)
            {
                case State.Moving: yield return StartCoroutine(moving_cr()); break;
                case State.Attacking: yield return StartCoroutine(attacking_cr()); break;
                case State.Dead: yield return StartCoroutine(died_cr()); break;
                case State.Spawned: yield return StartCoroutine(spawned_cr()); break;    
                default: break;
            }

            Vector3 dir = (target.position - this.transform.position).normalized;
            dir.z = 0;
            // = dir;
            this.transform.up = Vector3.RotateTowards(this.transform.up, dir, 0.3f, 0);

            yield return null;
        }
    }

    protected void ChangeState(State next)
    {
        switch(next)
        {
            case State.Moving: currentState = State.Moving; break;
            case State.Attacking:
                currentState = State.Attacking;
                m_ShootEventData.m_ShotPosition = transform.position;
                EventManager.Instance.SendEvent(m_ShootEventData);
                break;
            case State.Dead: currentState = State.Dead; break;
            case State.Spawned: currentState = State.Spawned; break;
            default: currentState = State.None; break;
        }
    }
    
    protected virtual IEnumerator moving_cr()
    {
        if ((target.position - this.transform.position).magnitude <= attackRange)
        { ChangeState(State.Attacking); yield return null; }
        else
        {
            Vector3 dir = (target.position - this.transform.position).normalized;
            this.transform.position += dir * speed * Time.deltaTime;
            
            for(int i = 0; i < obstacles.Count; i++)
            {
                if(Vector3.Distance(obstacles[i].transform.position, this.transform.position) < obstacles[i].GetComponent<CircleCollider2D>().radius * obstacles[i].transform.localScale.x)
                {
                    Vector3 vec = obstacles[i].transform.position - this.transform.position;
                    vec.Normalize();
                    float range = Vector3.Distance(obstacles[i].transform.position, this.transform.position) - (obstacles[i].GetComponent<CircleCollider2D>().radius * obstacles[i].transform.localScale.x);
                    this.transform.position += vec * range;
                }
            }

            yield return null;
        }
    }

    protected virtual IEnumerator attacking_cr()
    {
        yield return null;
    }

    protected virtual IEnumerator died_cr()
    {
        ChangeState(State.None);
        yield return null;
    }

    protected virtual IEnumerator spawned_cr()
    {
        yield return null;
    }

    public virtual void DealDamage(float dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            ChangeState(State.Dead);
        }
    }

    protected virtual void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            obstacles.Add(other.gameObject);
        }
    }

    protected virtual void OnTriggerExit2D (Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            obstacles.Remove(other.gameObject);
        }
    }
}
