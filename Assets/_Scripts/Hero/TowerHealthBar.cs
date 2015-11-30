using UnityEngine;
using System.Collections;

public class TowerHealthBar : MonoBehaviour {

    public Transform camera;
    private float ratio = 0.02453401f;

    // Use this for initialization
    void Start()
    {
        this.camera = GameObject.Find("Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {

        this.transform.forward = new Vector3(transform.position.x - camera.position.x, 0, transform.position.z - camera.position.z);

        ratio = (GetComponentInParent<Health>().HitPoints) / 100;

        Vector3 temp = this.transform.localScale;
        temp.x = -0.02453401f * ratio;
        this.transform.localScale = temp;

    }
}
