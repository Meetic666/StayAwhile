using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class AxisDetector : ShapeDetector
{
    [Tooltip("Leaving this blank will filter to players")]
    public GameObject[] objectsToDetect;
    public float OffsetOnAxis = 0.0f;
    public Axes axis = Axes.Y;
    public Comparers dir = Comparers.LessThan;

    protected override void CheckDetector()
    {
        if (objectsToDetect.Length == 0)
        {
            if (ObjectSingleton.Instance.playerList.Count != 0)
            {
                foreach (GameObject player in ObjectSingleton.Instance.playerList)
                {
                    DetectorFunc(player);
                }
            }
            else
            {
                foreach (GameObject detectMe in objectsToDetect)
                {
                    DetectorFunc(detectMe);
                }
            }
        }
    }


    void DetectorFunc(GameObject obj)
    {
        switch (axis)
        {
            case Axes.X:
                {
                    switch (dir)
                    {
                        case Comparers.LessThan:
                            {
                                if (obj.transform.position.x < gameObject.transform.position.x + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.MoreThan:
                            {
                                if (obj.transform.position.x > gameObject.transform.position.x + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.LessOrEqualTo:
                            {
                                if (obj.transform.position.x <= gameObject.transform.position.x + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.MoreOrEqualTo:
                            {
                                if (obj.transform.position.x >= gameObject.transform.position.x + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.EqualTo:
                            {
                                if (obj.transform.position.x == gameObject.transform.position.x + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                    }
                    break;
                }
            case Axes.Y:
                {
                    switch (dir)
                    {
                        case Comparers.LessThan:
                            {
                                if (obj.transform.position.y < gameObject.transform.position.y + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.MoreThan:
                            {
                                if (obj.transform.position.y > gameObject.transform.position.y + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.LessOrEqualTo:
                            {
                                if (obj.transform.position.y <= gameObject.transform.position.y + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.MoreOrEqualTo:
                            {
                                if (obj.transform.position.y >= gameObject.transform.position.y + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.EqualTo:
                            {
                                if (obj.transform.position.y == gameObject.transform.position.y + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                    }
                    break;
                }
            case Axes.Z:
                {
                    switch (dir)
                    {
                        case Comparers.LessThan:
                            {
                                if (obj.transform.position.z < gameObject.transform.position.z + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.MoreThan:
                            {
                                if (obj.transform.position.z > gameObject.transform.position.z + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.LessOrEqualTo:
                            {
                                if (obj.transform.position.z <= gameObject.transform.position.z + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.MoreOrEqualTo:
                            {
                                if (obj.transform.position.z >= gameObject.transform.position.z + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                        case Comparers.EqualTo:
                            {
                                if (obj.transform.position.z == gameObject.transform.position.z + OffsetOnAxis)
                                {
                                    bool inside = false;
                                    foreach (objAdder go in detectedObjects)
                                    {
                                        if (go.obj == obj.gameObject)
                                        {
                                            inside = true;
                                        }
                                    }
                                    if (inside == true)
                                    {
                                        OnDetectionStay(obj.gameObject);
                                    }
                                    else
                                    {
                                        OnDetectionStart(obj.gameObject);
                                    }
                                }
                                break;
                            }
                    }
                    break;
                }
        }
    }

#if UNITY_EDITOR
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (Selection.Contains(this.gameObject) == true)
        {
            Gizmos.color = new Color(1,0,0,0.75f);
            Vector3 pos = gameObject.transform.position;
            switch (axis)
            {
                case Axes.X:
                    {
                        pos.x += OffsetOnAxis;
                        Gizmos.DrawCube(pos, new Vector3(1f, 1000.0f, 1000.0f));
                        break;
                    }
                case Axes.Y:
                    {
                        pos.y += OffsetOnAxis;
                        Gizmos.DrawCube(pos, new Vector3(1000.0f, 1f, 1000.0f));
                        break;
                    }
                case Axes.Z:
                    {
                        pos.z += OffsetOnAxis;
                        Gizmos.DrawCube(pos, new Vector3(1000.0f, 1000.0f, 1f));
                        break;
                    }
            }
        }
    }
#endif
}
