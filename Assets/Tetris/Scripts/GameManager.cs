using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject BlockPrefab;

	[Space]
	public int rows;
	public int cols;
	
	public static Block[][] Grid;

	public Vector3 gridDirection = new Vector3(1, -1, 1);

	public Shape currentShape;

	// Use this for initialization
	void Start ()
	{
		CreateGrid();
		CreateShape();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (currentShape != null && currentShape.hasLanded)
		{
			CreateShape();
		}

		if (Input.GetKeyDown(KeyCode.UpArrow))
		{
			currentShape.MoveShape(0, -1);
		}
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{
			currentShape.MoveShape(0, 1);
		}
		else if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			currentShape.MoveShape(1, 0);
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			currentShape.MoveShape(-1, 0);
		}
	}
	
	void CreateShape()
	{
		currentShape = new LShape();
		UpdateGrid();
	}
	
	void CreateGrid()
	{
		// Calculates the Grid based on rows and cols
		Grid = new Block[rows][];
		for (int row = 0; row < Grid.Length; row++)
		{
			Grid[row] = new Block[cols];
		}

		// Creates the Grid visuals based off blocks.
		Vector3 localGridPosition = new Vector3();
		
		for (int row = 0; row < Grid.Length; row++)
		{
			for (int col = 0; col < Grid[row].Length; col++)
			{
				GameObject gridObject = Instantiate(BlockPrefab);
				Block gridBlock = gridObject.GetComponent<Block>();

				localGridPosition.x = col * gridDirection.x;
				localGridPosition.y = row * gridDirection.y;
				localGridPosition.z = 0 * gridDirection.z;
				
				Vector3 worldGridPosition = localGridPosition;

				gridObject.transform.position = worldGridPosition;

				Grid[row][col] = gridBlock;
				gridBlock.UpdateBlockColor();
			}
		}
	}
	
	public static void UpdateGrid()
	{
		for (int row = 0; row < Grid.Length; row++)
		{
			for (int col = 0; col < Grid[row].Length; col++)
			{
				Grid[row][col].UpdateBlockColor();
			}
		}
	}
}
