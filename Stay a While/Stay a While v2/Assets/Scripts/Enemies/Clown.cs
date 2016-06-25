using UnityEngine;
using System.Collections;

public class Clown : BaseEnemy
{
    [SerializeField]
    private float attackDamage;

    protected override IEnumerator attacking_cr()
    {
        base.attacking_cr();
        animator.SetTrigger("StateChange");

        if(Vector3.Distance(ObjectSingleton.Instance.playerList[targetIndex].transform.position, this.transform.position) <= attackRange)
        {
            ObjectSingleton.Instance.playerList[targetIndex].GetComponent<BasePlayer>().Damage(attackDamage);
        }
        
        yield return new WaitForSeconds(attackSpeed);
        yield return null;
    }

    protected override IEnumerator died_cr()
    {
        base.died_cr();
        //Spawn feul
        Instantiate(Fuel, this.transform.position, Quaternion.identity);

        //Temp
        StopAllCoroutines();
        Destroy(this.gameObject);

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
            
            if(Vector3.Distance(ObjectSingleton.Instance.playerList[1].transform.position, this.transform.position) < dist)
            { target = ObjectSingleton.Instance.playerList[1].transform; }
            else { target = ObjectSingleton.Instance.playerList[0].transform; }

            yield return null;
        }
    }
}
