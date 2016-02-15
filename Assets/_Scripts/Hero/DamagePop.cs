using UnityEngine;
using System.Collections;

public class DamagePop : MonoBehaviour 
{
	private Color[] colors = new Color[]{Color.red, Color.yellow, Color.blue, Color.green, Color.white};

	private TextMesh textMesh;
	private Vector3 targetPosition;

	void Awake()
	{
		this.textMesh = this.GetComponentInParent<TextMesh> ();
	}

	public void ChangeData(float value)
	{
		this.textMesh.text = "- " + value.ToString();
		this.textMesh.color = this.colors[Random.Range(0, this.colors.Length)];

	}

	void Update()
	{
		
	}
}