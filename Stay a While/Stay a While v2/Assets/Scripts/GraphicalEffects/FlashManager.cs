using UnityEngine;
using System.Collections;

public class FlashManager : MonoBehaviour
{
    public Material m_FlashMaterial;
    public float m_MaxRangeOffset = 0.01f;
    public float m_RangeIncreaseSpeed = 0.05f;
    public float m_FlashDuration = 0.5f;

    Vector3 m_FlashPosition;

    float m_CurrentTime = 0.0f;

    void Start()
    {
        EventManager.Instance.RegisterListener(typeof(ShootEventData), new GameEventDelegate(OnReceiveShootEvent));
        EventManager.Instance.RegisterListener(typeof(DamageEventData), new GameEventDelegate(OnReceiveDamageEvent));
    }

    void Update()
    {
        if(m_CurrentTime > 0.0f)
        {
            m_CurrentTime -= Time.deltaTime;

            if(m_CurrentTime <= 0.0f)
            {
                m_CurrentTime = 0.0f;
            }
        }
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        m_FlashMaterial.SetInt("m_LightEnabled", m_CurrentTime > 0.0f ? 1 : 0);

        if(m_CurrentTime > 0.0f)
        {
            Vector3 lightPositionInViewport = Camera.main.WorldToViewportPoint(m_FlashPosition);

            m_FlashMaterial.SetVector("m_LightPositions", lightPositionInViewport);

            m_FlashMaterial.SetFloat("m_LightRanges", Mathf.Lerp(0.0f, m_FlashMaterial.GetFloat("m_MaskUVSize") + m_MaxRangeOffset, 1.0f - m_CurrentTime / m_FlashDuration));
        }


        Graphics.Blit(source, destination, m_FlashMaterial);
    }
    
    void StartFlash(Vector3 position)
    {
        m_CurrentTime = m_FlashDuration;

        m_FlashPosition = position;
    }

    void OnReceiveDamageEvent(EventData data)
    {
        StartFlash(((DamageEventData)data).m_Position);
    }

    void OnReceiveShootEvent(EventData data)
    {
        StartFlash(((ShootEventData)data).m_ShotPosition);
    }
}
