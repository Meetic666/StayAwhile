using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public enum MoveType
{
    Loop,
    ReverseLoop,
    PingPong,
    ReversePingPong,
    StayEnd
}


public enum PickupType
{
    Food,
    Water,
    Sleep,
    Item
}

public enum AIState
{
    Idle,
    Patroling,
    Combat,
    Attacking
}
public enum Team
{
    Friendly = 0,
    Zombies = 1,
    Hostile1 = 2,
    Hostile2 = 3
}
public enum Axes
{
    X,
    Y,
    Z
}
public enum Comparers
{
    LessThan,
    MoreThan,
    EqualTo,
    LessOrEqualTo,
    MoreOrEqualTo
}

public abstract class Functions : MonoBehaviour
{
    public static Object[] LoadResourceVariations(string path)
    {
        bool breaker = true;
        int index = 1;
        List<Object> ReturnList = new List<Object>();
        while (breaker)
        {
             
            Object load = Resources.Load(path + index) as Object;
            if (load == null)
            {
                breaker = false;
            }
            else
            {
                ReturnList.Add(load);
                index++;
            }
        }
        return ReturnList.ToArray();

    }
}

public class SpriteLayerConstants
{
    public const int ENEMY_SPRITE_LAYER = -2;
    public const int BLOOD_SPRITE_LAYER = 0;
    public const int PICK_UP_SPRITE_LAYER = -1;
}
