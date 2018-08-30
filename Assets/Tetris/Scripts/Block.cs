using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	public int value = 0;
	public Color defaultBlockColor;
	public Color shapeBlockColor;
	public Renderer blockRenderer;

	protected MaterialPropertyBlock materialPropertyBlock;

	private void Awake()
	{
		materialPropertyBlock = new MaterialPropertyBlock();
	}

	public void UpdateBlockColor()
	{
		// Get the current value of the material properties in the renderer.
		blockRenderer.GetPropertyBlock(materialPropertyBlock);

		if (value == 0)
		{
			materialPropertyBlock.SetColor("_Color", defaultBlockColor);
		}
		else
		{
			materialPropertyBlock.SetColor("_Color", shapeBlockColor);
		}

		// Apply the edited values to the renderer.
		blockRenderer.SetPropertyBlock(materialPropertyBlock);
	}
}
