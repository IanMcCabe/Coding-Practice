using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TwentyFortyEight
{
	public class Block<T> : MonoBehaviour
	{
		public RectTransform postionTransform;
		public RectTransform sizeTransform;

		[Space]
		public Color defaultImageColor;
		public Image image;
		
		public virtual void ResetBlock()
		{
			SetColor(defaultImageColor);
		}

		public virtual void SetSize(float _width, float _height)
		{
			sizeTransform.anchoredPosition3D = new Vector3(_width / 2f, -_height/ 2, 0f);
			sizeTransform.sizeDelta = new Vector2(_width, _height);
		}

		public virtual void SetPosition(float _x, float _y)
		{
			postionTransform.anchoredPosition3D = new Vector3(_x, _y, 0f);
		}

		public virtual void SetColor(Color blockColor)
		{
			image.color = blockColor;
		}

		public virtual void UseBlock(T info)
		{
		}
	}
}
