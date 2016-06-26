using UnityEngine;
using System.Collections;

public class Nurse : BaseEnemy
{
    [SerializeField]
    private float attackDamage;

    protected override IEnumerator attacking_cr()
    {
        animator.SetTrigger("StateChange");

        yield return new WaitForSeconds(attackSpeed);
        if ((target.position - this.transform.position).magnitude > attackRange)
        { ChangeState(State.Moving); }
        yield return null;
    }

    private void Attack()
    {
        GameObject player = ObjectSingleton.Instance.playerList[0];
        if (Vector3.Distance(player.transform.position, this.transform.position) <= attackRange / 2)
        {
            if(player.activeSelf)
            {
                ObjectSingleton.Instance.playerList[0].GetComponent<BasePlayer>().Damage(attackDamage);
            }
        }
        if(ObjectSingleton.Instance.playerList.Count > 1)
        {
            player = ObjectSingleton.Instance.playerList[1];
            if (Vector3.Distance(player.transform.position, this.transform.position) <= attackRange / 2)
            {
                if (player.activeSelf)
                {
                    ObjectSingleton.Instance.playerList[1].GetComponent<BasePlayer>().Damage(attackDamage);
                }
            }
        }
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
            if (ObjectSingleton.Instance.playerList[1].activeSelf)
            {
                float dist = Vector3.Distance(ObjectSingleton.Instance.playerList[0].transform.position, this.transform.position);

                if (Vector3.Distance(ObjectSingleton.Instance.playerList[1].transform.position, this.transform.position) < dist)
                { target = ObjectSingleton.Instance.playerList[1].transform; }
                else { target = ObjectSingleton.Instance.playerList[0].transform; }
            }

            yield return null;
        }
    }
}
