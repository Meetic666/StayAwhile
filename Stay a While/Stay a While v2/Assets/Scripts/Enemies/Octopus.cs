﻿using UnityEngine;
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
            pos.z = transform.position.z;
            this.transform.position = pos;
            duration -= Time.deltaTime;
            yield return null;
        }
        
        //animator.SetTrigger("StateChange");

        bool pOneDmg = false;
        bool pTwoDmg = false;

        float dist = 0;
        while(dist < attackRange + 2)
        {
            this.transform.position += this.transform.up * chargeSpeed * Time.deltaTime;

            GameObject player = ObjectSingleton.Instance.playerList[0];
            if (!pOneDmg)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) <= 1)
                {
                    if(player.activeSelf)
                    {
                        ObjectSingleton.Instance.playerList[0].GetComponent<BasePlayer>().Damage(attackDamage);
                    }
                    pOneDmg = true;
                }
            }
            
            if(!pTwoDmg)
            {
                if (ObjectSingleton.Instance.playerList.Count > 1)
                {
                    player = ObjectSingleton.Instance.playerList[1];
                    if (Vector3.Distance(player.transform.position, this.transform.position) <= 1)
                    {
                        if(player.activeSelf)
                        {
                            ObjectSingleton.Instance.playerList[1].GetComponent<BasePlayer>().Damage(attackDamage);
                        }
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

    protected override IEnumerator spawned_cr()
    {
        base.spawned_cr();
        StartCoroutine(checkTarget_cr());
        ChangeState(State.Moving);
        yield return null;
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
