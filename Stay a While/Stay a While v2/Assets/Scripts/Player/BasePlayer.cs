using UnityEngine;
using System.Collections;

public class BasePlayer : MonoBehaviour
{
    public float MaxHealth = 100;
    public bool takesDamage = true;
    float Health;
    
    protected virtual void Start()
    {
        ObjectSingleton.Instance.playerList.Add(this.gameObject);
    }
    protected virtual void OnDestroy()
    {
        ObjectSingleton.Instance.playerList.Remove(this.gameObject);
    }

    public void Damage(float value)
    {
        if(takesDamage == true)
        {
            Health -= value;

            if(Health <= 0)
            {
                Death();
            }
        }
    }

    protected void Death()
    {
        //play death anim here
    }

    public void Destroy()
    {
        //call this func from an animation event at the end of your death anim
        Destroy(gameObject);

    }






}
