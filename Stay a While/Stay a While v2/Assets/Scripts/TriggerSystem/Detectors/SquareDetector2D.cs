using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class SquareDetector2D : ShapeDetector
{

    [Tooltip("Leaving this blank will filter to players")]
    public GameObject[] objectsToDetect;
    public Vector2 BoxSize = Vector2.one;
    public Vector2 Offset = Vector2.zero;
    GameObject player;
    protected override void CheckDetector()
    {
        if (objectsToDetect.Length == 0)
        {
            if (ObjectSingleton.Instance.playerList.Count != 0)
            {
                foreach (GameObject player in ObjectSingleton.Instance.playerList)
                {


                    if ((gameObject.transform.position.x + Offset.x) - BoxSize.x / 2.0f < player.transform.position.x && (gameObject.transform.position.x + Offset.x) + BoxSize.x / 2.0f > player.transform.position.x)
                    {
                        if ((gameObject.transform.position.y + Offset.y) - BoxSize.y / 2.0f < player.transform.position.y && (gameObject.transform.position.y + Offset.y) + BoxSize.y / 2.0f > player.transform.position.y)
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
        }
        else
        {
            foreach (GameObject detectMe in objectsToDetect)
            {

                if ((gameObject.transform.position.x + Offset.x) - BoxSize.x / 2.0f < detectMe.transform.position.x && (gameObject.transform.position.x + Offset.x) + BoxSize.x / 2.0f > detectMe.transform.position.x)
                {
                    if ((gameObject.transform.position.y + Offset.y) - BoxSize.y / 2.0f < detectMe.transform.position.y && (gameObject.transform.position.y + Offset.y) + BoxSize.y / 2.0f > detectMe.transform.position.y)
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
    }
#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (Selection.Contains(gameObject))
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(gameObject.transform.position + new Vector3(Offset.x, Offset.y), BoxSize);
        }
    }
#endif
}
