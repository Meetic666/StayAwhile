using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PressAnyKey : MonoBehaviour
{
    public string m_SceneToLoad;

    bool m_IsLoading;
	
	// Update is called once per frame
	void Update ()
    {
	    if(!m_IsLoading
            && Input.anyKey)
        {
            m_IsLoading = true;

            SceneManager.LoadSceneAsync(m_SceneToLoad);
        }
	}
}
