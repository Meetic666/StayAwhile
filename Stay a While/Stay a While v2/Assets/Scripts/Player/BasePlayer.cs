using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasePlayer : MonoBehaviour
{
    public float MaxHealth = 100;
    public bool takesDamage = true;
    public float Health;

    public int playerNum = 0;
    public List<BaseWeapon> Weapons = new List<BaseWeapon>();
    int activeWeaponNum = 0;
    protected virtual void Start()
    {
        ObjectSingleton.Instance.playerList.Add(this.gameObject);
    }

    protected virtual void Update()
    {
        switch (playerNum)
        {
            case 0:
                MovePlayer(Input.GetAxis("Joy1LeftHorizontal"), Input.GetAxis("Joy1LeftVertical"));
                RotatePlayer(Input.GetAxis("Joy1RightHorizontal"), Input.GetAxis("Joy1RightVertical"));

                if (Input.GetAxis("Joy1Trigger") > 0.6f)
                    RightTrigger();

                if (Input.GetKeyDown(KeyCode.Joystick2Button5))
                    RightBumper();
                break;
            case 1:
                MovePlayer(Input.GetAxis("Joy2LeftHorizontal"), Input.GetAxis("Joy2LeftVertical"));
                RotatePlayer(Input.GetAxis("Joy2RightHorizontal"), Input.GetAxis("Joy2RightVertical"));

                if (Input.GetAxis("Joy2Trigger") > 0.6f)
                    RightTrigger();

                if (Input.GetKeyDown(KeyCode.Joystick2Button5))
                    RightBumper();
                break;
            default:

                Debug.LogError("Fine, I'll add more players in another patch");
                return;
        }
    }

    protected virtual void RightBumper()
    {

        activeWeaponNum++;
        if(activeWeaponNum > Weapons.Count - 1)
        {
            activeWeaponNum = 0;
        }
    }


    protected virtual void RightTrigger()
    {

    }

    protected virtual void MovePlayer(float horizontal, float vertical)
    {
        gameObject.transform.position += new Vector3(horizontal, vertical);
    }

    protected virtual void RotatePlayer(float horizontal, float vertical)
    {
        Vector3 vec = new Vector3(horizontal, vertical);
        vec.Normalize();
        vec *= 5.0f;
        gameObject.transform.forward = gameObject.transform.position - vec;


    }


    protected virtual void OnDestroy()
    {
        ObjectSingleton.Instance.playerList.Remove(this.gameObject);
    }

    public void Damage(float value)
    {
        if (takesDamage == true)
        {
            Health -= value;

            if (Health <= 0)
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
    public virtual void Damage(float Damage)
    {
        Health -= Damage;
    }





}
