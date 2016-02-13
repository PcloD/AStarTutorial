using UnityEngine;
using System.Collections;

public class AStar
{	
	public static NodeSort closedList, openList;
	
	private static float HeuristicEstimateCost(Node curNode, Node goalNode)
	{
		Vector3 vectorCost = goalNode.position - curNode.position;

		return vectorCost.magnitude;	
	}
	
	public static ArrayList FindPath( Node start, Node goal )
	{		
		openList = new NodeSort();
		openList.Push(start);
		start.G_Cost = 0.0f;
		start.H_Cost = HeuristicEstimateCost(start, goal);
		
		closedList = new NodeSort();
		Node node = null;
		
		while (openList.Length != 0)
		{	
			node = openList.First();
			
			//Push the current node to the closed list
			closedList.Push(node);
			//and remove it from openList
			openList.Remove(node);
			
			//Check if the current node is the goal node
			if ( node.position == goal.position )
			{
				return CalculatePath(node);
			}
			
			//Create an ArrayList to store the neighboring nodes
			ArrayList neighbours = new ArrayList();
			NodeManager.instance.GetNeighbours(node, neighbours);
			
			Node neighbourNode;
			
			for ( int i = 0; i < neighbours.Count; i++ )
			{				
				neighbourNode = (Node)neighbours[i];
				
				if ( !closedList.Contains( neighbourNode ) )
				{					
					float cost;
					float totalCost;
					
					float neighbourNodeEstCost;
					
					if ( !openList.Contains( neighbourNode ) )
					{						
						//G
						cost = HeuristicEstimateCost( node, neighbourNode );
						totalCost = node.G_Cost + cost;
						
						//H
						neighbourNodeEstCost = HeuristicEstimateCost( neighbourNode, goal );
						
						neighbourNode.G_Cost = totalCost;
						neighbourNode.parent = node;
						neighbourNode.H_Cost = neighbourNodeEstCost;
						
						openList.Push(neighbourNode);
						
					}
					else
					{	
						cost = HeuristicEstimateCost( node, neighbourNode );
						totalCost = node.G_Cost + cost;
						
						if( neighbourNode.G_Cost > totalCost )
						{
							
							neighbourNode.G_Cost = totalCost;
							neighbourNode.parent = node;							
						}						
					}					
				}				
			}			
		}
		
		if ( node.position != goal.position )
		{			
			Debug.LogError("Goal Not Found");
			return null;			
		}
		
		return CalculatePath(node);		
	}
	
	private static ArrayList CalculatePath(Node node)
	{		
		ArrayList list = new ArrayList();
		
		while (node != null)
		{			
			list.Add( node );
			node = node.parent;			
		}
		
		list.Reverse();
		return LineOfSight( list );
	}

	private static ArrayList LineOfSight( ArrayList path )
	{
		ArrayList list = new ArrayList();
		list.Add( (Node)path[0] );

		Node startNode = (Node)path[0];
		Node nextNode;

		int checkIndex = 1;

		while( checkIndex < path.Count )
		{
			nextNode = ( (Node)path[checkIndex] );

			if( !Physics.Linecast( startNode.position, nextNode.position ) )
			{
				checkIndex++;
			}
			else
			{
				if( checkIndex == path.Count - 1 )
				{
					list.Add( (Node)path[checkIndex - 1] );
					list.Add( (Node)path[checkIndex] );
				}
				else
				{
					list.Add( (Node)path[checkIndex - 1] );
					startNode = (Node)path[checkIndex - 1];
					nextNode = (Node)path[checkIndex];
				}

				checkIndex++;
			}
		}

		return list;
	}
}
