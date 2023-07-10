using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeController : MonoBehaviour
{
    public SimplyLinkedList<NodeController> allAdjacentNodes;
    public float positionX;
    public float positionY;
    public int nodeTag;

    private void Awake()
    {
        allAdjacentNodes = new SimplyLinkedList<NodeController>();
    }

    private void Update()
    {
    }

    public void SetInitialValues(float posX, float posY, int tag)
    {
        positionX = posX;
        positionY = posY;
        transform.position = new Vector3(positionX, 0.5f, positionY);
        nodeTag = tag;
    }

    public void AddAdjacentNode(NodeController node)
    {
        allAdjacentNodes.AddNodeAtStart(node);
    }

    public NodeController SelectNextNode()
    {
        int nodeIndex = Random.Range(0, allAdjacentNodes.Count);
        return allAdjacentNodes.GetNodeAtPosition(nodeIndex);
    }

   
}
