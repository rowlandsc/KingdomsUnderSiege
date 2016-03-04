using UnityEngine;
using System.Collections;




public class VertsColor : MonoBehaviour {

	void Start () {
		MeshFilter mf = GetComponent<MeshFilter>();
		if (mf == null) return;
		Mesh mesh = mf.mesh;
		int[] triangles = mesh.triangles;
		Vector3[] vertices = mesh.vertices;
		int[] trianglesNew = new int[triangles.Length];
		Vector3[] verticesNew = new Vector3[triangles.Length];
		for (int i = 0; i < trianglesNew.Length; i++) {
			Vector3 v3Pos = vertices[triangles[i]];
			trianglesNew[i] = i;
			verticesNew[i] = v3Pos;
		}

		Color colorT = Color.red;
		Color[] colors = new Color[trianglesNew.Length];
		for (int i = 0; i < colors.Length; i++) {
			colorT = new Color(Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
			colors[i] = colorT;
		}

		mesh.vertices = verticesNew;
		mesh.triangles = trianglesNew;
		mesh.colors = colors;
		mesh.RecalculateBounds();
		mesh.RecalculateNormals ();
	}
}
