using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SphereDetector : ShapeDetector
{
    [Tooltip("Leaving this blank will filter to players")]
    public GameObject[] objectsToDetect;
    public float SphereRadius = 2.0f;
    public Vector3 Offset = Vector3.zero;
    GameObject player;
    protected override void CheckDetector()
    {
        if (objectsToDetect.Length == 0)
        {
            if (ObjectSingleton.Instance.playerList.Count != 0)
            {
                foreach (GameObject player in ObjectSingleton.Instance.playerList)
                {
                    if (Vector3.Distance(player.gameObject.transform.position, transform.TransformPoint(Offset)) <= SphereRadius)
                    {
                        bool inside = false;
                        foreach (objAdder obj in detectedObjects)
                        {
                            if (obj.obj == player.gameObject)
                            {
                                inside = true;
                            }
                        }
                        if (inside == true)
                        {
                            OnDetectionStay(player.gameObject);
                        }
                        else
                        {
                            OnDetectionStart(player.gameObject);
                        }
                    }
                }

            }
        }
        else
        {
            foreach (GameObject detectMe in objectsToDetect)
            {

                if (Vector3.Distance(detectMe.transform.position, transform.TransformPoint(Offset)) <= SphereRadius)
                {
                    bool inside = false;
                    foreach (objAdder obj in detectedObjects)
                    {
                        if (obj.obj == detectMe.gameObject)
                        {
                            inside = true;
                        }
                    }
                    if (inside == true)
                    {
                        OnDetectionStay(detectMe.gameObject);
                    }
                    else
                    {
                        OnDetectionStart(detectMe.gameObject);
                    }
                }
            }
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        if (Selection.Contains(gameObject))
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.TransformPoint(Offset), SphereRadius);
        }
    }
#endif

}
