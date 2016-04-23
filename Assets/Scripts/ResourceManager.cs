using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {

    [SerializeField]
    int m_defaultOxyigen = 10;
    [SerializeField]
    int m_defaultEnergy = 10;
    [SerializeField]
    int m_defaultFood = 10;
    [SerializeField]
    int m_defaultWater = 10;
    
    // depletionRates
    [SerializeField]
    int m_defaultOxyigenDepletionRate = 1;
    [SerializeField]
    int m_defaultEnergyDepletionRate = 1;
    [SerializeField]
    int m_defaultFoodDepletionRate = 1;
    [SerializeField]
    int m_defaultWaterDepletionRate = 1;

    // timers
    [SerializeField]
    int m_oxyigenTimeToDeplete = 1;
    [SerializeField]
    int m_energyTimeToDeplete = 1;
    [SerializeField]
    int m_foodTimeToDeplete = 1;
    [SerializeField]
    int m_waterTimeToDeplete = 1;

    //[SerializeField]
    public List<Resource> m_resourceArr;
    
    Dictionary<Resource.ResourceType, Resource> m_resources;

    // Use this for initialization
    void Start () {
        // create default resources
        // duplicate resource type key...have it here and remove from Resource??
        m_resources = new Dictionary<Resource.ResourceType, Resource>();
        m_resources[Resource.ResourceType.OXYGEN] = new Resource(Resource.ResourceType.OXYGEN, m_defaultOxyigen, m_defaultOxyigenDepletionRate, m_oxyigenTimeToDeplete);
        m_resources[Resource.ResourceType.ENERGY] = new Resource(Resource.ResourceType.ENERGY, m_defaultEnergy, m_defaultEnergyDepletionRate, m_energyTimeToDeplete);
        m_resources[Resource.ResourceType.FOOD] = new Resource(Resource.ResourceType.FOOD, m_defaultFood, m_defaultFoodDepletionRate, m_foodTimeToDeplete);
        m_resources[Resource.ResourceType.WATER] = new Resource(Resource.ResourceType.WATER, m_defaultWater, m_defaultWaterDepletionRate, m_waterTimeToDeplete);
    }
	
	// Update is called once per frame
	void Update () {

        // first update the time values
        foreach (KeyValuePair<Resource.ResourceType, Resource> entry in m_resources)
        {
            m_resources[entry.Key].AddToAccumulatedTime(Time.deltaTime);

            if (m_resources[entry.Key].GetAccumulatedTime() > m_resources[entry.Key].GetTimeToDeplete())
            {
                // reset the accumulated time and deplete resource by rate
                m_resources[entry.Key].ResetAccumulatedTime();
                m_resources[entry.Key].AddToVal(-m_resources[entry.Key].GetDepletionRate());
            }
        }
    }

    // can also use this for subtract
    void AddToResource(Resource.ResourceType resource, int val)
    {
        m_resources[resource].AddToVal(val);
    }
}
