using UnityEngine;
using System.Collections;

public class Resource {

    public enum ResourceType { OXYGEN, FOOD, WATER, ENERGY };

    ResourceType type;
    int val;

    public Resource (ResourceType type, int val)
    {
        this.type = type;
        this.val = val;
    }

    public int GetVal()
    {
        return val;
    }

    public void AddToVal(int val)
    {
        this.val += val;
    }

    public void SetVal(int val)
    {
        this.val = val;
    }
}
