using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BasePlayer : MonoBehaviour
{
    public float MaxHealth = 100;
    public bool takesDamage = true;
    public int playerNum = 0;
    public float DefMoveSpeed = 0.6f;
    public List<BaseWeapon> Weapons = new List<BaseWeapon>();
    float MoveSpeed;
    int activeWeaponNum = 0;
    public float Health;
    float FuelAmount = 0;

    protected virtual void Init(int player, float StartingFuel)
    {
        playerNum = player;
        FuelAmount = StartingFuel;
    }

    protected virtual void Start()
    {
        Health = MaxHealth;
        MoveSpeed = DefMoveSpeed;
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

                if (Input.GetKeyDown(KeyCode.Joystick1Button5))
                    RightBumper();

                if (Input.GetKeyDown(KeyCode.Joystick1Button2))
                    ReloadButton();
                break;
            case 1:
                MovePlayer(Input.GetAxis("Joy2LeftHorizontal"), Input.GetAxis("Joy2LeftVertical"));
                RotatePlayer(Input.GetAxis("Joy2RightHorizontal"), Input.GetAxis("Joy2RightVertical"));

                if (Input.GetAxis("Joy2Trigger") > 0.6f)
                    RightTrigger();

                if (Input.GetKeyDown(KeyCode.Joystick2Button5))
                    RightBumper();

                if (Input.GetKeyDown(KeyCode.Joystick2Button2))
                    ReloadButton();
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

        for(int i = 0; i < Weapons.Count; i++)
        {
            if(i != activeWeaponNum)
            {
                Weapons[i].enabled = false;
            }

        }
    }
    protected virtual void ReloadButton()
    {
        Weapons[activeWeaponNum].enabled = true;
        if(Weapons[activeWeaponNum].reloading == false)
        {
            StartCoroutine(Weapons[activeWeaponNum].ReloadCR());
        }

    }

    protected virtual void RightTrigger()
    {
        Weapons[activeWeaponNum].enabled = true;
        Weapons[activeWeaponNum].Fire();
    }

    protected virtual void MovePlayer(float horizontal, float vertical)
    {
        if(horizontal < 0.25f && horizontal > -0.25f)
        {
            horizontal = 0.0f;
        }
        if (vertical < 0.25f && vertical > -0.25f)
        {
            vertical = 0.0f;
        }
        gameObject.transform.position += new Vector3(horizontal * MoveSpeed * Time.deltaTime, vertical * MoveSpeed * Time.deltaTime);
    }

    protected virtual void RotatePlayer(float horizontal, float vertical)
    {
        Vector3 vec = new Vector3(horizontal, vertical);
        vec = gameObject.transform.position - (vec + gameObject.transform.position);
        vec.Normalize();
        gameObject.transform.up = vec;
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

            if (Health <= 0.0f)
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
