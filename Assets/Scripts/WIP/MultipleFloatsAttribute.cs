using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can use it on:
// FLOAT
// then you specify in the MultipleFloat arguments (if the template doesn't work)
public class MultipleFloatsAttribute : PropertyAttribute
{
    // $ becomes the name of the max property
    // example: [MinTo] float duration; float durationMin;
    public string minName = "$Min";

    public float? fourth;
    public float? third;
    public float? second;
    public float first;
    public MultipleFloatsAttribute(string minName = null)
    {
        if (minName != null)
            this.minName = minName;
    }
    
    public MultipleFloatsAttribute(float first, float second, float third, float fourth, string minName = null) : this(minName)
    {
        this.fourth = fourth;
        this.third = third;
        this.second = second;
        this.first = first;
    }
}
