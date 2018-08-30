using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwentyFortyEight
{
	public class TextGrid : Grid<NumBlockInfo>
	{
		public TextBlock blockPrefab;
		
		[Space]
		public NumBlockInfo[] numBlockInfoArray;
		
		public void ReSizeGrid(float width, float height)
		{
			// Change the size of the element.
			rectTransform.sizeDelta = new Vector2(width, height);

			// Change the position to recenter the element.
			Vector3 position = rectTransform.anchoredPosition;
			position.y = -(height / 2f);
			rectTransform.anchoredPosition = position;
		}

		public override Block<NumBlockInfo> GetBlockPrefab()
		{
			return blockPrefab;
		}
				
		protected NumBlockInfo GetNumBlock(int value)
		{
			NumBlockInfo numBlockInfo = new NumBlockInfo();

			for (int i = 0; i < numBlockInfoArray.Length; i++)
			{
				if (numBlockInfoArray[i].number == value)
				{
					numBlockInfo = numBlockInfoArray[i];
					return numBlockInfo;
				}
			}

			if (numBlockInfoArray.Length > 0)
			{
				numBlockInfo = numBlockInfoArray[numBlockInfoArray.Length];
				numBlockInfo.number = value;
				return numBlockInfo;
			}

			return numBlockInfo;
		}

		public NumBlockInfo DefaultNumBlockInfo()
		{
			return GetNumBlock(2);
		}

		public bool PopulateNextBlock()
		{
			List<Location> emptyBlocks = GetEmptyBlocks();

			if (emptyBlocks.Count > 0)
			{
				NumBlockInfo numBlockInfo = DefaultNumBlockInfo();

				int randomBlock = Random.Range(0, emptyBlocks.Count);
				UseBlock(emptyBlocks[randomBlock].x, emptyBlocks[randomBlock].y, numBlockInfo.number, numBlockInfo);
				return true;
			}
			else
			{
				return false;
			}
		}

		public void AddToNewBlock(int row, int col, int newRow, int newCol)
		{
			intGrid[newRow][newCol] += intGrid[row][col];
			intGrid[row][col] = 0;

			BlockGrid[row][col].ResetBlock();
			BlockGrid[newRow][newCol].UseBlock(GetNumBlock(intGrid[newRow][newCol]));
		}


		public void MoveBlocksLeft()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					// Find a block in use
					if (intGrid[row][col] != 0)
					{
						// Find if there's any blocks that match it to the left
						for (int leftcol = (col-1); leftcol >= 0; leftcol--)
						{
							bool moveSuccessful = MoveInDirection(row, col, row, leftcol, row, leftcol +1, leftcol == 0);
							if (moveSuccessful) break;
						}
					}					
				}
			}
		}

		public void MoveBlocksRight()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = BlockGrid[row].Length - 1; col >= 0 ; col--)
				{
					// Find a block in use
					if (intGrid[row][col] != 0)
					{
						// Find if there's any blocks that match it to the right
						for (int rightCol = (col + 1); rightCol < BlockGrid[row].Length; rightCol++)
						{
							bool moveSuccessful = MoveInDirection(row, col, row, rightCol, row, rightCol - 1, rightCol == BlockGrid[row].Length - 1);
							if (moveSuccessful) break;
						}
					}
				}
			}
		}

		public void MoveBlocksUp()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					// Find a block in use
					if (intGrid[row][col] != 0)
					{
						// Find if there's any blocks that match it to the down
						for (int upRow = (row - 1); upRow >= 0; upRow--)
						{
							bool moveSuccessful = MoveInDirection(row, col, upRow, col, upRow + 1, col, upRow == 0);
							if (moveSuccessful) break;
						}
					}
				}
			}
		}

		public void MoveBlocksDown()
		{
			for (int row = BlockGrid.Length - 1; row >= 0 ; row--)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					// Find a block in use
					if (intGrid[row][col] != 0)
					{
						// Find if there's any blocks that match it to the down
						for (int downRow = (row + 1); downRow < BlockGrid.Length; downRow++)
						{
							bool moveSuccessful = MoveInDirection(row, col, downRow, col, downRow - 1, col, downRow == BlockGrid.Length - 1);
							if (moveSuccessful) break;
						}
					}
				}
			}
		}

		public bool MoveInDirection(int row, int col, int newRow, int newCol, int nextRow, int nextCol, bool isLastPos)
		{
			// If we find a match, add our block to the one found
			if (intGrid[newRow][newCol] == intGrid[row][col])
			{
				AddToNewBlock(row, col, newRow, newCol);
				return true;
			}
			// If we are blocked, try to assign it to the empty spot next to it.
			else if (intGrid[newRow][newCol] != intGrid[row][col] && intGrid[newRow][newCol] != 0)
			{ 
				if (!(row == nextRow && col == nextCol) && intGrid[nextRow][nextCol] == 0)
				{
					AddToNewBlock(row, col, nextRow, nextCol);
				}
				return true;
			}

			// If we get to the end, then just assign it to the very left
			if (isLastPos && intGrid[newRow][newCol] == 0)
			{
				AddToNewBlock(row, col, newRow, newCol);
				return true;
			}

			return false;
		}

		public bool CheckForPossibleMove()
		{
			for (int row = 0; row < BlockGrid.Length; row++)
			{
				for (int col = 0; col < BlockGrid[row].Length; col++)
				{
					// Find a block in use
					if (intGrid[row][col] != 0)
					{
						if (row - 1 >= 0 && intGrid[row][col] == intGrid[row - 1][col]) { return true; }
						else if (row + 1 < BlockGrid.Length && intGrid[row][col] == intGrid[row + 1][col]) { return true; }
						else if (col - 1 >= 0 && intGrid[row][col] == intGrid[row][col - 1]) { return true; }
						else if (col + 1 < BlockGrid[row].Length && intGrid[row][col] == intGrid[row][col + 1]) { return true; }
					}
				}
			}

			return false;
		}
	}
}
