using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TwentyFortyEight
{
	[System.Serializable]
	public struct NumBlockInfo
	{
		public Color blockColor;
		public Color textColor;
		public int number;
	}

	public class TextBlock : Block<NumBlockInfo>
	{
		public Text text;

		public override void ResetBlock()
		{
			Color textColor = defaultImageColor;
			textColor.a = 0;

			SetColor(defaultImageColor, textColor);
		}

		public void SetColor(Color blockColor, Color textColor)
		{
			SetColor(blockColor);
			text.color = textColor;
		}

		public override void UseBlock(NumBlockInfo info)
		{
			SetColor(info.blockColor, info.textColor);
			text.text = info.number.ToString();
			base.UseBlock(info);
		}
	}
}
