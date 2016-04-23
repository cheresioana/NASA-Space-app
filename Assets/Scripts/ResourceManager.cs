using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour {

    [SerializeField]
    int m_defaultOxyigen = 100;
    [SerializeField]
    int m_defaultEnergy = 100;
    [SerializeField]
    int m_defaultFood = 100;
    [SerializeField]
    int m_defaultWater = 100;
    
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

    // timers
    [SerializeField]
    int m_oxygenMaxValue = 100;
    [SerializeField]
    int m_energyMaxValue = 100;
    [SerializeField]
    int m_foodMaxValue = 100;
    [SerializeField]
    int m_waterMaxValue = 100;

    // timers
    [SerializeField]
    Transform oxygenBar = null;
    [SerializeField]
    Transform foodBar = null;
    [SerializeField]
    Transform waterBar = null;
    [SerializeField]
    Transform energyBar = null;

    //[SerializeField]
    public List<Resource> m_resourceArr;
    
    static Dictionary<Resource.ResourceType, Resource> m_resources;

    void Awake()
    {
        m_resources = new Dictionary<Resource.ResourceType, Resource>();
    }

    // Use this for initialization
    void Start () {
        m_resources.Clear();
        // create default resources
        // duplicate resource type key...have it here and remove from Resource??
        m_resources[Resource.ResourceType.OXYGEN] = new Resource(oxygenBar, Resource.ResourceType.OXYGEN, m_defaultOxyigen, m_defaultOxyigenDepletionRate, m_oxyigenTimeToDeplete, m_oxygenMaxValue);
        m_resources[Resource.ResourceType.ENERGY] = new Resource(energyBar, Resource.ResourceType.ENERGY, m_defaultEnergy, m_defaultEnergyDepletionRate, m_energyTimeToDeplete, m_energyMaxValue);
        m_resources[Resource.ResourceType.FOOD] = new Resource(foodBar, Resource.ResourceType.FOOD, m_defaultFood, m_defaultFoodDepletionRate, m_foodTimeToDeplete, m_foodMaxValue);
        m_resources[Resource.ResourceType.WATER] = new Resource(waterBar, Resource.ResourceType.WATER, m_defaultWater, m_defaultWaterDepletionRate, m_waterTimeToDeplete, m_waterMaxValue);

        // oh well
        foreach (KeyValuePair<Resource.ResourceType, Resource> entry in m_resources)
        {
            Resource res = m_resources[entry.Key];
            if (res == null)
                continue;
            RectTransform rt = res.GetUIObjectTransform().GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            float panelWidth = Mathf.Abs(corners[0].x - corners[2].x);
            res.SetUIObjectWidth(panelWidth);
            UpdateBarScale(res);
        }
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

                // bound check with 0
                if (m_resources[entry.Key].GetVal() < 0)
                {
                    m_resources[entry.Key].SetVal(0);

                    // play death phase
                }
                //Debug.Log("Value: " + m_resources[entry.Key].GetVal());
                UpdateBarScale(m_resources[entry.Key]);
            }
        }
    }

    // can also use this for subtract
    public static void AddToResource(Resource.ResourceType resource, int val)
    {
        m_resources[resource].AddToVal(val);
        if (m_resources[resource].GetVal() > m_resources[resource].GetMaxValue())
        {
            m_resources[resource].SetVal(m_resources[resource].GetMaxValue());
        }

        UpdateBarScale(m_resources[resource]);
    }

    public static int GetResourceVal(Resource.ResourceType type)
    {
        return m_resources[type].GetVal();
    }

    public static int GetMaxResourceVal(Resource.ResourceType type)
    {
        return m_resources[type].GetMaxValue();
    }

    private static void UpdateBarScale(Resource res)
    {
        Transform obj = res.GetUIObjectTransform();
        if (obj)
        {
            RectTransform rt = obj.GetComponent<RectTransform>();
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);

            float panelWidthOrig = Mathf.Abs(corners[0].x - corners[2].x);
            float leftX = corners[0].x;

            const float defaultScale = 1; // this is full length
            float newScale = defaultScale * res.GetVal() / (float)m_resources[res.GetResourceType()].GetMaxValue();

            float scaleDiff = (obj.localScale.x - newScale);

            //if (scaleDiff != 0)
            //    scaleDiff /= 2;
            float diff = scaleDiff * res.GetUIObjectWidth() / 2;

            obj.localScale = new Vector3(newScale, obj.localScale.y, obj.localScale.z);

            obj.Translate(new Vector3(-diff, 0f, 0f));
        }
    }
}
