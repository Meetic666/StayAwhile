using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour
{
    private bool shaking;

    private float duration;
    private float force;
    private Vector3 axis;
    private int playerIndex;
    private float movement;

    private Vector3 offset;
    private Vector3 origin;
    private Camera _camera;
    private Transform playerOne;
    private Transform playerTwo;
    [SerializeField]
    private float lerpSpeed;
    
    void Start()
    {
        offset = Vector3.zero;
        origin = this.transform.position;
        _camera = GetComponent<Camera>();
        StartCoroutine(update_cr());

        EventManager.Instance.RegisterListener(typeof(ShootEventData), new GameEventDelegate(OnReceiveShootEvent));
        EventManager.Instance.RegisterListener(typeof(DamageEventData), new GameEventDelegate(OnReceiveDamageEvent));
    }

    private IEnumerator update_cr()
    {
        yield return null;

        playerOne = ObjectSingleton.Instance.playerList[0].transform;
        if (ObjectSingleton.Instance.playerList.Count > 1) { playerTwo = ObjectSingleton.Instance.playerList[1].transform; }

        bool multiplayer = false;
        if (playerTwo != null) { multiplayer = true; }
        while(true)
        {
            if(!multiplayer)
            {
                Vector3 pos = Vector3.Lerp(this.transform.position, playerOne.position, lerpSpeed);
                pos.z = -10.0f;
                
                this.transform.position = pos;
            }
            else
            {
                Vector3 pos = playerOne.position;
                pos += (playerTwo.position - playerOne.position) / 2.0f;
                float multiplier = Vector3.Distance(playerOne.position, playerTwo.position) / 21.0f;
                pos.z = -10.0f;
                if (multiplier > 1.0f) { _camera.orthographicSize = 9.42f * Mathf.Abs(multiplier); }
                this.transform.position = origin = pos;
            }

            
            yield return null;
        }
    }

    public void CameraShake(float force = 0.3f)
    {
        axis = Vector3.one;
        axis.z = 0;
        duration = 0.5f;
        this.force = force;
        axis *= force;

        StartCoroutine(cameraShake_cr());
    }

    public void CameraShake(Vector3 axis, float force)
    {
        this.force = force;
        this.axis = axis;
        this.axis.z = 0;
        duration = 0.5f;
        axis *= force;

         StartCoroutine(cameraShake_cr());
    }

    public void CameraShake(Vector3 axis, float force, float duration)
    {
        this.force = force;
        this.axis = axis;
        this.axis.z = 0;
        this.duration = duration;
        axis *= force;

        StartCoroutine(cameraShake_cr());
    }

    private IEnumerator cameraShake_cr()
    {
        if (!shaking)
        {
            shaking = true;
            Vector3 offset = Vector3.zero;
            while (duration > 0.0f)
            {
                origin = this.transform.position;

                offset.x = Random.Range(-axis.x, axis.x);
                offset.y = Random.Range(-axis.y, axis.y);

                this.transform.position += offset;

                duration -= Time.deltaTime;
                yield return null;
            }
            shaking = false;
        }
    }

    public void PlayerFocus(int playerIndex, float movement = 0.1f)
    {
        this.playerIndex = playerIndex;
        this.movement = movement;
        StartCoroutine(playerFocus_cr());
    }

    private IEnumerator playerFocus_cr()
    {
        Vector3 origin = this.transform.position;
        while(duration > 0)
        {
            this.transform.position = Vector3.Lerp(origin, ObjectSingleton.Instance.playerList[playerIndex].transform.position, movement);

            duration -= Time.deltaTime;
            yield return null;
        }
    }

    void OnReceiveShootEvent(EventData data)
    {
        CameraShake();
    }

    void OnReceiveDamageEvent(EventData data)
    {
        CameraShake();
    }
}
