using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class CircleDetector2D : ShapeDetector
{
    [Tooltip("Leaving this blank will filter to players")]
    public GameObject[] objectsToDetect;
    public float CircleRadius = 2.0f;
    public Vector2 Offset = Vector2.zero;
    protected override void CheckDetector()
    {
        if (objectsToDetect.Length == 0)
        {
            if (ObjectSingleton.Instance.playerList.Count != 0)
            {
                foreach (GameObject player in ObjectSingleton.Instance.playerList)
                {
                    if (Vector2.Distance(player.gameObject.transform.position, gameObject.transform.position + new Vector3(Offset.x, Offset.y, player.gameObject.transform.position.z)) <= CircleRadius)
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

                if (Vector2.Distance(detectMe.transform.position, gameObject.transform.position + new Vector3(Offset.x, Offset.y)) <= CircleRadius)
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
            base.OnDrawGizmos();
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(gameObject.transform.position + new Vector3(Offset.x, Offset.y), CircleRadius);
        }
    }
#endif

}
