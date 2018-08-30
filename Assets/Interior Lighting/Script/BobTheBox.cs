using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobTheBox : MonoBehaviour
{
	public Rigidbody targetRigidbody;
	public Renderer targetRenderer;
	protected MaterialPropertyBlock materialPropertyBlock;

	private void Awake()
	{
		materialPropertyBlock = new MaterialPropertyBlock();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveForward()
	{
		
	}

	public void MoveBack()
	{
	}

	public void MoveLeft()
	{
	}

	public void MoveRight()
	{
	}

	public void ChangeColor(Color _color)
	{
		targetRenderer.GetPropertyBlock(materialPropertyBlock);
		materialPropertyBlock.SetColor("_Color", _color);
		targetRenderer.SetPropertyBlock(materialPropertyBlock);
	}
}
