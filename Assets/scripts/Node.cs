using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] protected Node parentNode;
    [SerializeField] protected List<Node> children = new List<Node>();

    [SerializeField] GameObject linePrefab;

    float nodeLen = 1;
    Vector3 childPositionTemp = new Vector3();
    bool isHaveParent = false;
    protected int depth;
    float xForce = 0;
    float yForce = 0;
    float xDelta = 0;
    float yDelta = 0;
    float friction = 5;
    protected int nid;
    float nodeDiameter = 0.8f;

    protected virtual void Start()
    {
        initNodes();
    }

    private void initNodes()
    {
        int nidTemp = 0;
        foreach (Node childNode in children)
        {
            childNode.SetNodeDatas(this, depth, nidTemp);
            nidTemp++;
            GameObject temp = Instantiate(linePrefab);
            temp.GetComponent<Line>().InitLine(this, childNode);
        }
    }

    public bool CompareByNid(int otherNid)
    {
        return (nid == otherNid);
    }

    public void SetNodeDatas(Node parent, int depth, int nid)
    {
        this.nid = nid;
        parentNode = parent;
        isHaveParent = true;
        this.depth = depth + 1;
    }

    public List<Node> GetChildren()
    {
        return children;
    }

    public int GetChildrenNum()
    {
        int result = 1;
        foreach (Node childNode in children)
        {
            result += childNode.GetChildrenNum();
        }
        return result;
    }

    private void ForceByParent()
    {
        Vector3 parentPosition = parentNode.transform.position;

        float xDistance = parentPosition.x - transform.position.x;
        float yDistance = parentPosition.y - transform.position.y;
        float nodeDistance = Mathf.Pow(xDistance * xDistance + yDistance * yDistance, 0.5f);
        if (nodeLen < nodeDistance)
        {
            float tension = (nodeDistance - nodeLen) / nodeDistance;
            print(tension * xDistance * friction);
            xForce += tension * xDistance * friction;
            yForce += tension * yDistance * friction;
        }

    }
    private void ForceBySibling()
    {
        foreach (Node sibling in parentNode.GetChildren()){
            if (! sibling.CompareByNid(nid))
            {
                Vector3 siblingPosition = sibling.transform.position;
                float xDistance = siblingPosition.x - transform.position.x;
                float yDistance = siblingPosition.y - transform.position.y; 
                float nodeDistance = Mathf.Pow(xDistance * xDistance + yDistance * yDistance, 0.5f);

                if (nodeDiameter > nodeDistance)
                {
                    xForce -= xDistance;
                    yForce -= yDistance; 
                }

            }
        }
    }

    protected void MoveNode()
    {
        xForce = 0;
        yForce = 0;
        xDelta = 0;
        yDelta = 0;

        ForceByParent();
        ForceBySibling();

        float deltaTime = Time.deltaTime;
        xDelta += xForce * friction * deltaTime;
        yDelta += yForce * friction * deltaTime;
        transform.position += new Vector3(xDelta, yDelta, 0);
        
    }
}
