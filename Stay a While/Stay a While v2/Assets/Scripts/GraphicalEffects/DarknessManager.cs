using UnityEngine;
using System.Collections;

public class DarknessManager : MonoBehaviour
{
    public Material m_DarknessMaterial;

    public CircleDetector2D m_FireCircle;
    public float m_MaxRadius = 10.0f;

    public Texture[] m_Masks;

    public int m_MaskAnimationFPS = 1;

    int m_CurrentMaskFrame = 0;

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Vector3 radiusVector = Camera.main.transform.position + Vector3.left * m_FireCircle.CircleRadius;
        radiusVector = Camera.main.WorldToViewportPoint(radiusVector) - Camera.main.WorldToViewportPoint(Camera.main.transform.position);

        radiusVector.z = 0.0f;

        Vector3 maxRadiusVector = Camera.main.transform.position + Vector3.left * m_MaxRadius;
        maxRadiusVector = Camera.main.WorldToViewportPoint(maxRadiusVector) - Camera.main.WorldToViewportPoint(Camera.main.transform.position);

        maxRadiusVector.z = 0.0f;

        Vector3 lightPositionInViewport = Camera.main.WorldToViewportPoint(m_FireCircle.transform.position);

        m_DarknessMaterial.SetVector("m_LightPositions", lightPositionInViewport);
        m_DarknessMaterial.SetFloat("m_LightRanges", radiusVector.magnitude);
        m_DarknessMaterial.SetFloat("m_MaskUVSize", maxRadiusVector.magnitude);
        m_DarknessMaterial.SetTexture("m_Masks", m_Masks[m_CurrentMaskFrame]);
        m_DarknessMaterial.SetFloat("m_MaskThreshold", (1.0f - m_FireCircle.CircleRadius / m_MaxRadius));

        Graphics.Blit(source, destination, m_DarknessMaterial);
    }
}
