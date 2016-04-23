using UnityEngine;
using System.Collections;

public class Resource {

    public enum ResourceType { OXYGEN, FOOD, WATER, ENERGY };

    ResourceType type;
    int val = 0;
    int depletionRate = 0;
    float timeToDeplete = 0; // in seconds
    float accumulatedTime = 0; // in seconds
    int maxValue = 100;

    public Resource(ResourceType type, int val)
    {
        this.type = type;
        this.val = val;
        this.depletionRate = 1;
    }

    public Resource (ResourceType type, int val, int depletionRate)
    {
        this.type = type;
        this.val = val;
        this.depletionRate = depletionRate;
    }

    public Resource(ResourceType type, int val, int depletionRate, int timeToDeplete)
    {
        this.type = type;
        this.val = val;
        this.depletionRate = depletionRate;
        this.timeToDeplete = timeToDeplete;
    }

    public Resource(ResourceType type, int val, int depletionRate, int timeToDeplete, int maxValue)
    {
        this.type = type;
        this.val = val;
        this.depletionRate = depletionRate;
        this.timeToDeplete = timeToDeplete;
        this.maxValue = maxValue;
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

    public void AddToAccumulatedTime(float val)
    {
        this.accumulatedTime += val;
    }

    public void ResetAccumulatedTime()
    {
        this.accumulatedTime = 0;
    }

    public float GetAccumulatedTime()
    {
        return accumulatedTime;
    }

    public float GetTimeToDeplete()
    {
        return timeToDeplete;
    }

    public int GetDepletionRate()
    {
        return depletionRate;
    }

    public int GetMaxValue()
    {
        return maxValue;
    }
}
