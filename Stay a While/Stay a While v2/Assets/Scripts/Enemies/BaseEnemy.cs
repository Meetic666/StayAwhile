using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour
{
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
    protected float statePause;
    protected int targetIndex;
    protected Transform target;
    protected IEnumerator UPDATE;

    protected virtual void Start()
    {
        healthMAX = health;
        statePause = 0;
        currentState = State.Spawned;
        targetIndex = Random.Range(0, 2);
        target = ObjectSingleton.Instance.playerList[targetIndex].transform;
        UPDATE = update_cr();
        StartCoroutine(UPDATE);
    }

    protected virtual IEnumerator update_cr()
    {
        while(true)
        {
            switch(currentState)
            {
                case State.Moving: Moving(); break;
                case State.Attacking: Attacking(); break;
                case State.Dead: Died(); break;
                case State.Spawned: Spawned(); break;    
                default: break;
            }
            if (statePause > 0) { yield return new WaitForSeconds(statePause); }
            else { yield return null; }
        }
    }

    protected void ChangeState(State next)
    {
        switch(next)
        {
            case State.Moving: currentState = State.Moving; statePause = 0; break;
            case State.Attacking: currentState = State.Attacking; statePause = attackSpeed; break;
            case State.Dead: currentState = State.Dead; statePause = 0; break;
            case State.Spawned: currentState = State.Spawned; statePause = 0; break;
            default: currentState = State.None; statePause = 0; break;
        }
    }
    
    protected virtual void Moving()
    {
        if((target.position - this.transform.position).magnitude <= attackRange)
        { ChangeState(State.Attacking); return; }

        Vector3 dir = (target.position - this.transform.position).normalized;
        this.transform.up = dir;
        this.transform.position += dir * speed * Time.deltaTime;
    }

    protected virtual void Attacking()
    {

    }

    protected virtual void Died()
    {
        ChangeState(State.None);
    }

    protected virtual void Spawned()
    {

    }

    public virtual void DealDamage(float dmg)
    {
        health -= dmg;
    }
}
