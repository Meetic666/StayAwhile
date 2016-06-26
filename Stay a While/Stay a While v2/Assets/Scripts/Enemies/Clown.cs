using UnityEngine;
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

    protected override IEnumerator died_cr()
    {
        base.died_cr();
        //Spawn fuel
        Instantiate(Fuel, this.transform.position, Quaternion.identity);

        GameObject fab = (GameObject)Instantiate(bloodPrefab, this.transform.position + Vector3.forward, this.transform.rotation);
        fab.GetComponent<SpriteRenderer>().sprite = bloodSprites[Random.Range(0, bloodSprites.Length)];

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
            if(ObjectSingleton.Instance.playerList[1].activeSelf)
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
