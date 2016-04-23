using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {


    enum ResourceType { OXYGEN, FOOD, WATER, ENERGY};

    Dictionary<ResourceType, int> depletionRates;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
