using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestGraph : MonoBehaviour
{
    [SerializeField]
    private List<TestData> data;

    public TestData Entry
    {
        get
        {
            if(data == null || data.Count == 0)
            {
                data = new List<TestData>();
                data.Add(new TestData("Entry"));
            }

            return data[0];
        }
        set 
        {
            if(data.Count == 0)
            {
                data.Add(value);
            }

            data[0] = value;
        }
    }
}
