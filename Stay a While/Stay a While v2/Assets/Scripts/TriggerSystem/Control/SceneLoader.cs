using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class SceneLoader : Triggerable
{
    public string SceneToLoad;
    protected override void TriggerEffect()
    {
        SceneManager.LoadScene(SceneToLoad);
    }

}
