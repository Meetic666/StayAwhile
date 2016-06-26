using UnityEngine;
using System.Collections;

public class TommyWiseau : BaseEnemy
{
    [SerializeField]
    private GameObject FootBall;
    [SerializeField]
    private Transform SpawnPoint;

    protected override IEnumerator attacking_cr()
    {
        animator.SetTrigger("StateChange");
        yield return new WaitForSeconds(attackSpeed);
        if ((target.position - this.transform.position).magnitude > attackRange)
        { ChangeState(State.Moving); }
        yield return null;
    }

    private void ThrowFootBall()
    {
        Instantiate(FootBall, SpawnPoint.position, Quaternion.Euler(0.0f,0.0f,transform.rotation.eulerAngles.z/2.0f));
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
        animator.SetTrigger("StateChange");
        StartCoroutine(checkTarget_cr());
        ChangeState(State.Moving);
        yield return null;
    }

    private IEnumerator checkTarget_cr()
    {
        while (true)
        {
            float dist = Vector3.Distance(ObjectSingleton.Instance.playerList[0].transform.position, this.transform.position);
            if(ObjectSingleton.Instance.playerList.Count > 1)
            {
                if (Vector3.Distance(ObjectSingleton.Instance.playerList[1].transform.position, this.transform.position) < dist)
                { target = ObjectSingleton.Instance.playerList[1].transform; }
                else { target = ObjectSingleton.Instance.playerList[0].transform; }
            }
            
            yield return null;
        }

    }
}
