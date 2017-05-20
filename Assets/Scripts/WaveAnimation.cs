using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnimation : MonoBehaviour {


    private SkinnedMeshRenderer skinnedMeshRenderer;

    // Use this for initialization
    void Start () {

        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();

    }
	
	// Update is called once per frame
	void Update () {

        skinnedMeshRenderer.SetBlendShapeWeight(0, Mathf.PingPong(Time.time * 100.0f, 100.0f));
        skinnedMeshRenderer.SetBlendShapeWeight(1, Mathf.PingPong(Time.time * 100.0f, 100.0f));

    }
}
