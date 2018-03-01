using System.Collections.Generic;
using UnityEngine;

public class MLTestDecision : MonoBehaviour, Decision
{

    public float[] Decide(List<float> state, List<Camera> observation, float reward, bool done, float[] memory)
    {
        string s = "";
        foreach(float f in state) {s += f + " ";}
        //Debug.Log(s);
        return new float[1]{ Random.Range(0, MLTestAgent.ACTION_SIZE) };

    }

    public float[] MakeMemory(List<float> state, List<Camera> observation, float reward, bool done, float[] memory)
    {
        return new float[0];

    }
}
