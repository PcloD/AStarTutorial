using System.Collections;

public class NodeSort
{
	private ArrayList nodes = new ArrayList();

	//Return length of arrayList
	public int Length
	{
		get { return this.nodes.Count; }
	}

	//If the arrayList contains the node, return true
	//else return false
	public bool Contains(object node)
	{	
		for( int cnt = 0; cnt < nodes.Count; cnt++ )
		{
			
			if( ( (Node)node ).position == ( (Node)nodes[cnt] ).position )
				return true;
			
		}
		
		return false;
	}

	//Get the first node in the arrayList
	public Node First()
	{	
		if (this.nodes.Count > 0)
		{
			return (Node)this.nodes[0];
		}
		return null;
	}

	//Add a new node in the arrayList
	public void Push(Node node)
	{
		
		this.nodes.Add(node);
		//Ensure the list is sorted
		this.nodes.Sort();
	}

	//Remove the node from arrayList
	public void Remove(Node node)
	{	
		this.nodes.Remove(node);
		//Ensure the list is sorted
		this.nodes.Sort();
	}
}
