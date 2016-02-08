using UnityEngine;
using System.Collections;

public class Tower : MonoBehaviour {

    public float Radius = 2;

    private MapCircleDrawer _mapCircleDrawer = null;

	void Start () {
        TowerPlacer.Instance.TowerList.Add(this);
        _mapCircleDrawer = GetComponent<MapCircleDrawer>();
        _mapCircleDrawer.GameMap = Map.Instance;
        _mapCircleDrawer.CircleRadius = Radius;
        _mapCircleDrawer.UpdateCircle();
        _mapCircleDrawer.SetCircleVisible(false);
    }
	
	void Update () {
	
	}
}
