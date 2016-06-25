using UnityEngine;
using System.Collections;

public class MoveObject : Triggerable
{
    Vector3 startPosition;
    int index = 0;
    public GameObject[] PathToFollow;
    public bool Reset = true;
    public MoveType moveType = MoveType.StayEnd;
    public float MoveSpeed;
    int dir = 1;
    float DistToChange = 0.2f;

    protected virtual void Start()
    {
        startPosition = gameObject.transform.position;
        switch (moveType)
        {
            case MoveType.ReverseLoop:
                dir = -1;
                break;
            case MoveType.ReversePingPong:
                dir = -1;
                break;
        }
    }

    protected override void TriggerEffect()
    {
        base.TriggerEffect();
        if (index >= 0 && index < PathToFollow.Length)
        {
            if (Vector3.Distance(gameObject.transform.position, PathToFollow[index].transform.position) < DistToChange)
            {
                index += dir;
                switch (moveType)
                {
                    case MoveType.Loop:
                        if (index > PathToFollow.Length - 1)
                        {
                            index = 0;
                        }
                        break;
                    case MoveType.PingPong:
                        if (index == PathToFollow.Length - 1 || index == 0)
                        {
                            dir *= -1;
                        }
                        break;
                    case MoveType.ReverseLoop:

                        if (index < 0)
                        {
                            index = PathToFollow.Length - 1;
                        }
                        break;
                    case MoveType.ReversePingPong:
                        if (index == PathToFollow.Length - 1 || index == 0)
                        {
                            dir *= -1;
                        }
                        break;
                    case MoveType.StayEnd:
                        if (index == PathToFollow.Length - 1)
                        {
                            dir = 0;
                        }
                        break;
                }
            }
            else
            {
                gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, PathToFollow[index].transform.position, MoveSpeed * Time.deltaTime);
            }
        }
    }

    public override void ResetTrigger()
    {
        base.ResetTrigger();
        index = 0;
        gameObject.transform.position = startPosition;
        switch (moveType)
        {
            case MoveType.ReverseLoop:
                dir = -1;
                break;
            case MoveType.ReversePingPong:
                dir = -1;
                break;
            case MoveType.Loop:
                dir = 1;
                break;
            case MoveType.PingPong:
                dir = 1;
                break;
            case MoveType.StayEnd:
                dir = 1;
                break;
        }
    }

}
