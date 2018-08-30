using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace TwentyFortyEight
{
	public struct Location
	{
		public int x;
		public int y;

		public Location(int _x, int _y)
		{
			x = _x;
			y = _y;
		}
	}

	public abstract class Grid<T> : MonoBehaviour
	{
		public RectTransform rectTransform;

		public abstract Block<T> GetBlockPrefab();

		protected int cols;
		protected int rows;

		float width;
		float height;

		protected int[][] intGrid;
		protected Block<T>[][] BlockGrid;
		float blockWidth;
		float blockheight;
		

		public virtual void InitGrid(int _cols, int _rows)
		{
			cols = _cols;
			rows = _rows;

			width = rectTransform.sizeDelta.x;
			height = rectTransform.sizeDelta.y;

			blockWidth = width / cols;
			blockheight = height / rows;

			intGrid = new int[rows][];
			BlockGrid = new Block<T>[rows][];

			for (int i = 0; i < BlockGrid.Length; i++)
			{
				intGrid[i] = new int[cols];
				BlockGrid[i] = new Block<T>[cols];
			}			
		}


		public virtual void CreateGrid()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					intGrid[row][col] = 0;
					BlockGrid[row][col] = Instantiate(GetBlockPrefab(), rectTransform);

					BlockGrid[row][col].SetSize(blockWidth, blockheight);
					BlockGrid[row][col].SetPosition(blockWidth * col, -blockheight * row);
				}
			}
		}

		public virtual void ResetGrid()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					intGrid[row][col] = 0;
					BlockGrid[row][col].ResetBlock();
				}
			}
		}

		public virtual void DestoryGrid()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					Destroy(BlockGrid[row][col]);
				}
			}
		}


		public List<Location> GetEmptyBlocks()
		{
			List<Location> emptyBlocks = new List<Location>();

			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					if (intGrid[row][col] == 0)
					{
						emptyBlocks.Add(new Location(row, col));
					}
				}
			}

			return emptyBlocks;
		}		

		public void UseBlock(int row, int col, int value, T info)
		{
			if (intGrid[row][col] == 0)
			{
				intGrid[row][col] = value;
				BlockGrid[row][col].UseBlock(info);
			}
		}
	}
}
