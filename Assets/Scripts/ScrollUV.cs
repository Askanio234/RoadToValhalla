using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour {
    private MeshRenderer mr;
    Material mat;
    public float parralax = 2f;
	// Use this for initialization
	void Start () {
        mr = GetComponent<MeshRenderer>();
        mat = mr.material;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = mat.mainTextureOffset;
        offset.x = transform.position.x / transform.localScale.x / parralax;
        offset.y = transform.position.y / transform.localScale.y / parralax;
        mat.mainTextureOffset = offset;
	}
}
