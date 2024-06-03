using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{

    private LineRenderer line;
    [SerializeField] private Node node1;
    [SerializeField] private Node node2;

    [SerializeField]
    private float minDistance = 0.1f;
    [SerializeField]
    private float width = 0.1f;

    public void InitLine(Node pNode, Node cNode)
    {
        node1 = pNode;
        node2 = cNode;
    }

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
        line.startWidth = line.endWidth = width;
    }
    private void Update()
    {

        line.SetPosition(0, node1.transform.position);
        line.SetPosition(1, node2.transform.position);

    }
}
