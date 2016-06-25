using UnityEngine;
using System.Collections;

public class DarknessManager : MonoBehaviour
{
    public Material m_DarknessMaterial;

    public Vector3[] m_LightPositions = new Vector3[10];
    public int[] m_LightEnabled = new int[10];
    public float[] m_LightRanges = new float[10];
    public Texture[] m_Masks = new Texture[10];

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        int loopSize = Mathf.Min(10, m_LightEnabled.Length);

        for(int i = 0; i < loopSize; i++)
        {
            m_DarknessMaterial.SetVector("m_LightPositions", m_LightPositions[i]);
            m_DarknessMaterial.SetInt("m_LightEnabled", m_LightEnabled[i]);
            m_DarknessMaterial.SetFloat("m_LightRanges", m_LightRanges[i]);
            m_DarknessMaterial.SetTexture("m_Masks", m_Masks[i]);
        }

        Graphics.Blit(source, destination, m_DarknessMaterial);
    }
}
