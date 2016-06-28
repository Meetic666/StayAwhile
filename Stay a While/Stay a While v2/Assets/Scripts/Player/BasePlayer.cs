using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DamageEventData : EventData
{
    public Vector3 m_Position;
}

public class PlayerDeathEventData : EventData
{
    public BasePlayer m_Player;
}

public class PlayerRespawnEventData : EventData
{
    public BasePlayer m_Player;
}

public class BasePlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject healthBar;

    public float MaxHealth = 100;
    public bool takesDamage = true;
    public int playerNum = 0;
    public float DefMoveSpeed = 0.6f;
    public List<BaseWeapon> Weapons = new List<BaseWeapon>();
    float MoveSpeed;
    int activeWeaponNum = 0;
    public float Health;
    public float FuelAmount = 0;
    public float Defense = 0;

    Animator animator;
    DamageEventData m_DamageEventData;
    PlayerDeathEventData m_DeathEventData;
    PlayerRespawnEventData m_RespawnEventData;

    protected virtual void Init(int player, float StartingFuel)
    {
        playerNum = player;
        FuelAmount = StartingFuel;
    }

    void OnEnable()
    {
        Reset();
    }

    void Reset()
    {
        Health = MaxHealth;

        if (m_RespawnEventData != null)
        {
            EventManager.Instance.SendEvent(m_RespawnEventData);
        }
    }

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        Health = MaxHealth;
        MoveSpeed = DefMoveSpeed;
        ObjectSingleton.Instance.playerList.Add(this.gameObject);

        m_DamageEventData = new DamageEventData();

        m_DeathEventData = new PlayerDeathEventData();
        m_DeathEventData.m_Player = this;

        m_RespawnEventData = new PlayerRespawnEventData();
        m_RespawnEventData.m_Player = this;

        if (Input.GetJoystickNames().Length < 2)
        {
            if (playerNum > 0) { Destroy(); }
        }
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
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

                Debug.LogError("Fine, I'll add more players in another patch"); // lol.. the lies we tell ourself <3 Greate work though! -TP
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
        animator.SetInteger("Weapon", activeWeaponNum);
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
        if (horizontal != 0 || vertical != 0) { animator.SetBool("Walking", true); } else { animator.SetBool("Walking", false); }
        gameObject.transform.position += new Vector3(horizontal * MoveSpeed * Time.deltaTime, vertical * MoveSpeed * Time.deltaTime);
    }

    protected virtual void RotatePlayer(float horizontal, float vertical)
    {
        Vector3 vec = new Vector3(horizontal, vertical);
        vec = gameObject.transform.position - (vec + gameObject.transform.position);
        vec.Normalize();
        gameObject.transform.up = vec /2.0f;
    }
    protected virtual void OnDestroy()
    {
        ObjectSingleton.Instance.playerList.Remove(this.gameObject);
    }
    public void AddDefense(float value)
    {
        if(Defense < 50.0f)
        {
            Defense += value;
        }

        if (Defense > 50.0f)
            Defense = 50.0f;
    }
    public void Damage(float value)
    {
        if (takesDamage == true)
        {
            if(value > 0)
            {
                m_DamageEventData.m_Position = transform.position;

                EventManager.Instance.SendEvent(m_DamageEventData);
            }

            if(Defense >= value)
            {
                Defense -= value;
            }
            else
            {
                value -= Defense;
                Defense = 0;
                Health -= value;
            }

            if (Health <= 0.0f)
            {
                Health = 0.0f;

                Death();
            }
        }
    }

    protected void Death()
    {
        EventManager.Instance.SendEvent(m_DeathEventData);
    }

    public void Destroy()
    {
        //call this func from an animation event at the end of your death anim
        //ObjectSingleton.Instance.playerList.RemoveAt(playerNum);
        Destroy(healthBar);
        Destroy(gameObject);
    }
}
