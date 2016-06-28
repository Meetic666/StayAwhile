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

    protected override IEnumerator spawned_cr()
    {
        base.spawned_cr();
        animator.SetTrigger("StateChange");
        StartCoroutine(checkTarget_cr());
        ChangeState(State.Moving);
        yield return null;
    }
}
