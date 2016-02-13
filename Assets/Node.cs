using UnityEngine;
using System.Collections;
using System;

public class Node : IComparable
{
	//G cost is current cost
	public float G_Cost;
	
	//H cost is estimate cost
	public float H_Cost;

	//Desicion the node can move or not
	public bool isObstacle;
	//Record the node's parent
	public Node parent;
	//the position of the node
	public Vector3 position;


	//Initial the node
	public Node()
	{
		
		this.G_Cost = 0.0f;
		this.H_Cost = 0.0f;
		this.isObstacle = false;
		this.parent = null;
	}

	//Initial the node with position
	public Node(Vector3 pos)
	{
		
		this.G_Cost = 0.0f;
		this.H_Cost = 0.0f;
		this.isObstacle = false;
		this.parent = null;
		this.position = pos;
	}

	//Set the node to be an obstacle
	public void MarkAsObstacle()
	{	
		this.isObstacle = true;	
	}

	//Because our Node calss in herits form IComparable, we need to override this CompareTo method.
	//We need to sort our list of node arrays based on the total cost of G and H.
	//The ArrayList  type has a method called Sort.
	//Sort basically looks for this CompareTo methond, implemented inside the object ( in this case our Node objects ) from th list.
	//The IComparable.CompareTo method can be found at http://msdn.microsoft.com/en-us/library/system.icomparable.compareto.aspx.
	public int CompareTo( object obj )
	{
		Node node = (Node)obj;
		
		//If new node's total cost is bigger than old node, don't change
		if ( this.G_Cost + this.H_Cost < node.G_Cost + this.H_Cost )
			return -1;
		
		//If new node's total cost is smaller than old node, change
		if ( this.G_Cost + this.H_Cost > node.G_Cost + this.H_Cost )
			return 1;
				
		return -1;
	}
}