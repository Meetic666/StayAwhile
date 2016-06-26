using UnityEngine;
using System.Collections;

public class Octopus : BaseEnemy
{
    [SerializeField]
    private float attackDamage;
    [SerializeField]
    private float chargeTime;
    [SerializeField]
    private float chargeSpeed;

    private bool obstacleCollision;

    protected override IEnumerator attacking_cr()
    {
        Vector3 origin = this.transform.position;
        Vector3 pos = Vector3.zero;
        float duration = chargeTime;

        while(duration > 0)
        {
            pos.x = Random.Range(-0.05f, 0.05f);
            pos.y = Random.Range(-0.05f, 0.05f);
            pos += origin;
            this.transform.position = pos;
            duration -= Time.deltaTime;
            yield return null;
        }
        
        animator.SetTrigger("StateChange");

        bool pOneDmg = false;
        bool pTwoDmg = false;

        float dist = 0;
        while(dist < attackRange + 2)
        {
            this.transform.position += this.transform.up * chargeSpeed * Time.deltaTime;

            if(!pOneDmg)
            {
                if (Vector3.Distance(ObjectSingleton.Instance.playerList[0].transform.position, this.transform.position) <= 1)
                {
                    ObjectSingleton.Instance.playerList[0].GetComponent<BasePlayer>().Damage(attackDamage);
                    pOneDmg = true;
                }
            }
            
            if(!pTwoDmg)
            {
                if (ObjectSingleton.Instance.playerList.Count > 1)
                {
                    if (Vector3.Distance(ObjectSingleton.Instance.playerList[1].transform.position, this.transform.position) <= 1)
                    {
                        ObjectSingleton.Instance.playerList[1].GetComponent<BasePlayer>().Damage(attackDamage);
                        pTwoDmg = true;
                    }
                }
            }
            

            dist += chargeSpeed * Time.deltaTime;
            if (obstacleCollision) { obstacleCollision = false; break; }
            yield return null;
        }
        
        yield return new WaitForSeconds(attackSpeed);
        if ((target.position - this.transform.position).magnitude > attackRange)
        { ChangeState(State.Moving); }
        yield return null;
    }

    protected override IEnumerator died_cr()
    {
        base.died_cr();
        //Spawn fuel
        Instantiate(Fuel, this.transform.position, Quaternion.identity);

        StopAllCoroutines();
        this.gameObject.SetActive(false);

        yield return null;
    }

    protected override IEnumerator spawned_cr()
    {
        base.spawned_cr();
        StartCoroutine(checkTarget_cr());
        ChangeState(State.Moving);
        yield return null;
    }

    private IEnumerator checkTarget_cr()
    {
        float players = ObjectSingleton.Instance.playerList.Count;
        while (players > 1)
        {
            float dist = Vector3.Distance(ObjectSingleton.Instance.playerList[0].transform.position, this.transform.position);

            if (Vector3.Distance(ObjectSingleton.Instance.playerList[1].transform.position, this.transform.position) < dist)
            { target = ObjectSingleton.Instance.playerList[1].transform; }
            else { target = ObjectSingleton.Instance.playerList[0].transform; }

            yield return null;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            obstacleCollision = true;
            obstacles.Add(other.gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Obstacle")
        {
            obstacleCollision = false;
            obstacles.Remove(other.gameObject);
        }
    }
}
