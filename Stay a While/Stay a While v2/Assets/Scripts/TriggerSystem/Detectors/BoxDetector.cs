using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BoxDetector : ShapeDetector
{
    [Tooltip("Leaving this blank will filter to players")]
    public GameObject[] objectsToDetect;
    public Vector3 halfExtents = Vector3.one;
    public Vector3 Offset = Vector3.zero;

    protected override void CheckDetector()
    {
        if (objectsToDetect.Length == 0)
        {
            if (ObjectSingleton.Instance.playerList.Count != 0)
            {
                Collider[] colls = new Collider[16];

                Vector3 pos = transform.TransformPoint(Offset);

                if(Physics.OverlapBoxNonAlloc(pos,halfExtents,colls,gameObject.transform.rotation) != 0)
                {
                    for( int i = 0; i < colls.Length - 1; i++)
                    {
                        if (ObjectSingleton.Instance.playerList.Contains(colls[i].gameObject))
                        {
                            bool inside = false;
                            if (detectedObjects.Count != 0)
                            {
                                foreach (objAdder obj in detectedObjects)
                                {
                                    if (ObjectSingleton.Instance.playerList.Contains(obj.obj))
                                    {
                                        inside = true;
                                    }
                                }
                            }
                            if (inside == true)
                            {
                                OnDetectionStay(colls[i].gameObject);
                            }
                            else
                            {
                                OnDetectionStart(colls[i].gameObject);
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
                Collider[] colls = new Collider[0];

                Vector3 pos = transform.TransformPoint(Offset);

                if (Physics.OverlapBoxNonAlloc(pos, halfExtents, colls, gameObject.transform.rotation) != 0)
                {
                    for (int i = 0; i < colls.Length - 1; i++)
                    {
                        if (colls[i] == detectMe.GetComponent<Collider>())
                        {
                            bool inside = false;
                            if (detectedObjects.Count != 0)
                            {
                                foreach (objAdder obj in detectedObjects)
                                {
                                    if (obj.obj == detectMe.gameObject)
                                    {
                                        inside = true;
                                    }
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
    }



#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {

        base.OnDrawGizmos();


        if (Selection.Contains(gameObject))
        {
            Matrix4x4 defRotMatrix = Gizmos.matrix;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.matrix = rotationMatrix;

            Gizmos.color = Color.white;
            Gizmos.DrawWireCube(Offset, halfExtents * 2.0f);

            Gizmos.matrix = defRotMatrix;
        }

    }


#endif

}
