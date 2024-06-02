using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] protected Node parentNode;
    [SerializeField] protected List<Node> children = new List<Node>();

    float nodeLen = 1;
    Vector3 childPositionTemp = new Vector3();
    bool isHaveParent = false;

    protected virtual void Start()
    {
        initNodes();
    }

    protected virtual void Update()
    {
        
    }

    private void initNodes()
    {
        foreach (Node childNode in children)
        {
            childNode.SetParent(this);
        }
    }

    public void SetParent(Node parent)
    {
        parentNode = parent;
        isHaveParent = true;
    }

    public int GetChildrennNum()
    {
        int result = 1;
        foreach (Node childNode in children)
        {
            result += childNode.GetChildrennNum();
        }
        return result;
    }
    float friction = 5;
    protected void MoveNode()
    {
        Vector3 parentPosition = parentNode.transform.position;
        float xDelta = 0;
        float yDelta = 0;
        float xDiff = parentPosition.x - transform.position.x;
        float yDiff = parentPosition.y - transform.position.y;
        float nodeDiff = Mathf.Pow(xDiff * xDiff + yDiff * yDiff, 0.5f);
        float xForce = 0;
        float yForce = 0;
        if (nodeLen < nodeDiff)
        {
            float tension = (nodeDiff - nodeLen) / nodeDiff;
            xForce = tension * xDiff * friction;
            yForce = tension * yDiff * friction;
        }
        float deltaTime = Time.deltaTime;
        xDelta += xForce * friction * deltaTime;
        yDelta += yForce * friction * deltaTime;
        transform.position += new Vector3(xDelta, yDelta, 0);
        
    }
}
