using UnityEngine;
using System.Collections;

public class Clown : BaseEnemy
{
    [SerializeField]
    private float attackDamage;

    protected override void Moving()
    {
        base.Moving();

    }

    protected override void Attacking()
    {
        base.Attacking();

    }

    protected override void Died()
    {
        base.Died();

    }

    protected override void Spawned()
    {
        base.Spawned();
        ChangeState(State.Moving);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        float healthPCT = (healthMAX / health) * 100;
        if (healthPCT > 75) { }
        else if (healthPCT > 50) { }
        else if (healthPCT > 25) { }
        else { }
    }
}
