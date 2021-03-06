﻿using UnityEngine;
using System.Collections;

public class Clown : BaseEnemy
{
    [SerializeField]
    private float attackDamage;

    protected override IEnumerator attacking_cr()
    {
        animator.SetTrigger("StateChange");

        GameObject player = ObjectSingleton.Instance.playerList[targetIndex];
        if (Vector3.Distance(player.transform.position, this.transform.position) <= attackRange)
        {
            if(player.activeSelf)
            {
                ObjectSingleton.Instance.playerList[targetIndex].GetComponent<BasePlayer>().Damage(attackDamage);
            }
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
}
