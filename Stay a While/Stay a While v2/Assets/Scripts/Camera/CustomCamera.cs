using UnityEngine;
using System.Collections;

public class CustomCamera : MonoBehaviour
{
    private bool shaking;
    private bool lerping;

    private float duration;
    private float force;
    private Vector3 axis;
    private int playerIndex;
    private float movement;
    
    public void CameraShake(float force = 0.75f)
    {
        axis = Vector3.one;
        axis.z = 0;
        duration = 10;
        this.force = force;

        StartCoroutine(cameraShake_cr());
    }

    public void CameraShake(Vector3 axis, float force)
    {
        this.force = force;
        this.axis = axis;
        this.axis.z = 0;
        duration = 1;

        StartCoroutine(cameraShake_cr());
    }

    public void CameraShake(Vector3 axis, float force, float duration)
    {
        this.force = force;
        this.axis = axis;
        this.axis.z = 0;
        this.duration = duration;

        StartCoroutine(cameraShake_cr());
    }

    private IEnumerator cameraShake_cr()
    {
        if (!shaking)
        {
            shaking = true;
            Vector3 origin = this.transform.position;
            Vector3 pos = Vector3.zero;
            while (duration > 0)
            {
                if (lerping) { force /= 2; }
                axis *= force;
                pos.x = Random.Range(-axis.x, axis.x);
                pos.y = Random.Range(-axis.y, axis.y);

                this.transform.position = origin + pos;

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
        lerping = true;
        while(duration > 0)
        {
            this.transform.position = Vector3.Lerp(origin, ObjectSingleton.Instance.playerList[playerIndex].transform.position, movement);

            duration -= Time.deltaTime;
            yield return null;
        }
        lerping = false;
    }
}
