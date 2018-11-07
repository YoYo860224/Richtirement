using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentAssetsValue : MonoBehaviour {
    Slider sliders;
    public Text value;

	// Use this for initialization
	void Start () {
        sliders = this.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        value.text = ((int)sliders.value).ToString();
	}
}
