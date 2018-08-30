using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LShape : Shape
{
	public override void CreateShape()
	{
		shapeGrid = new int[][]
		{
			new int[]{ 1,0},
			new int[]{ 1,0},
			new int[]{ 1,1},
		};
	}
}

public abstract class Shape
{
	public int x;
	public int y;
	public int[][] shapeGrid;

	public bool hasLanded = false;

	public Shape()
	{
		CreateShape();
		x = 0;
		y = 0;

		UpdateCurrentShape(1);
	}

	public abstract void CreateShape();

	public void MoveShape(int colMove, int rowMove)
	{
		int newX = x + colMove;
		int newY = y + rowMove;

		// Ensures the shape can't move outside the grid
		if (MovedOutsideGrid(newX, newY)) return;

		// Disables the shape in this location.
		UpdateCurrentShape(0);

		if (MovedCollideWithBlock(newX, newY))
		{
			UpdateCurrentShape(1);
			return;
		}

		// Moves the shapes location.
		x = newX;
		y = newY;

		// Enables the shape in the new location.
		UpdateCurrentShape(1);

		if (HitTheBottom() || HasBlockBelow())
		{
			hasLanded = true;
		}

		// Update the grid now that things have changed.
		GameManager.UpdateGrid();
	}

	//protected void RotateShape()
	//{
	//	int shapeRow = shapeGrid.Length;
	//	int shapeCol = shapeGrid[0].Length;

	//	int[][] rotatedShapeGrid = new int[shapeCol][];

	//	for (int row = 0; row < shapeGrid.Length; row++)
	//	{
	//	}
	//}

	protected bool MovedOutsideGrid(int newX, int newY)
	{
		if (newX < 0 || (newX + shapeGrid[0].Length) > GameManager.Grid[0].Length) return true;
		if (newY < 0 || (newY + shapeGrid.Length) > GameManager.Grid.Length) return true;

		return false;
	}
	
	protected bool MovedCollideWithBlock(int newX, int newY)
	{
		for (int row = 0; row < shapeGrid.Length; row++)
		{
			for (int col = 0; col < shapeGrid[row].Length; col++)
			{
				if (shapeGrid[row][col] != 0)
				{
					int gridRow = row + newY;
					int gridCol = col + newX;

					if (GameManager.Grid[gridRow][gridCol].value == 1)
					{
						return true;
					}
				}
			}
		}

		return false;
	}

	protected bool HitTheBottom()
	{
		if ((y + shapeGrid.Length) == GameManager.Grid.Length) return true;
		return false;
	}

	protected bool HasBlockBelow()
	{
		int row = shapeGrid.Length - 1;

		// Add +1 to check the row below it.
		int gridRow = row + y + 1;

		if (gridRow < GameManager.Grid.Length)
		{
			for (int col = 0; col < shapeGrid[row].Length; col++)
			{
				if (shapeGrid[row][col] != 0)
				{
					int gridCol = col + x;

					if (GameManager.Grid[gridRow][gridCol].value == 1)
					{
						return true;
					}
				}
			}
		}

		return false;
	}

	void UpdateCurrentShape(int _value)
	{
		for (int row = 0; row < shapeGrid.Length; row++)
		{
			for (int col = 0; col < shapeGrid[row].Length; col++)
			{
				if (shapeGrid[row][col] != 0)
				{
					int gridRow = row + y;
					int gridCol = col + x;

					GameManager.Grid[gridRow][gridCol].value = _value;
				}
			}
		}
	}

}
