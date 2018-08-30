using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TwentyFortyEight
{
	public class GameManager : MonoBehaviour
	{
		[Header("Grid Settings")]
		public int cols;
		public int rows;		
		public TextGrid grid;

		[Header("Input Settings")]
		public SwipeInput swipeInput;
		public KeyInput keyboardInput;

		[Header("Game Settings")]
		public GameObject gameOverObject;
		public bool isGameOver = false;

		
		void Start()
		{
			SetInput();
			CreateGrid();
			PopulateNextBlock();
		}

		private void SetInput()
		{
			swipeInput.enabled = false;
			keyboardInput.enabled = false;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
			swipeInput.enabled = true;
#endif

#if UNITY_STANDALONE || UNITY_EDITOR
			keyboardInput.enabled = true;
#endif
		}

		private void CreateGrid()
		{
			grid.ReSizeGrid(Screen.height, Screen.height);
			grid.InitGrid(cols, rows);
			grid.CreateGrid();
			grid.ResetGrid();
		}

		private void PopulateNextBlock()
		{
			bool canPlaceBlock = grid.PopulateNextBlock();

			if (!canPlaceBlock)
			{
				bool hasMove = grid.CheckForPossibleMove();

				if (!hasMove)
				{
					GameOver();
				}
			}
		}

		private void GameOver()
		{
			gameOverObject.SetActive(true);
			isGameOver = true;
		}

		// Input Functions
		public void ResetGame()
		{
			grid.ResetGrid();
			PopulateNextBlock();

			gameOverObject.SetActive(false);
			isGameOver = false;
		}

		public void MoveUp()
		{
			if (isGameOver)
			{
				ResetGame();
				return;
			}

			grid.MoveBlocksUp();
			PopulateNextBlock();
		}

		public void MoveDown()
		{
			if (isGameOver)
			{
				ResetGame();
				return;
			}

			grid.MoveBlocksDown();
			PopulateNextBlock();
		}

		public void MoveLeft()
		{
			if (isGameOver)
			{
				ResetGame();
				return;
			}

			grid.MoveBlocksLeft();
			PopulateNextBlock();
		}

		public void MoveRight()
		{
			if (isGameOver)
			{
				ResetGame();
				return;
			}

			grid.MoveBlocksRight();
			PopulateNextBlock();
		}
	}
}
