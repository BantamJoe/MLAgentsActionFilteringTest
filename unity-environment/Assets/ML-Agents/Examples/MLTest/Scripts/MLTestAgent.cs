using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MLTestAgent : Agent
{
    public static int ACTION_SIZE;
    public static int STATE_SIZE;
    public static int BLOCK;
    List<float> state = new List<float>();
    int score = 0;
    float total = 0;
    int value = 0;
    void Awake()
    {
        Brain brain = GameObject.Find("Brain").GetComponent<Brain>();
        ACTION_SIZE = brain.brainParameters.actionSize;
        STATE_SIZE = brain.brainParameters.stateSize;
        BLOCK = ACTION_SIZE / 2;
        for (int i = 0; i < STATE_SIZE; i++) { state.Add(0.0f); }
        gen();
    }

    public void gen()
    {
        value = Random.Range(0, ACTION_SIZE);

        for (int i = 0; i < ACTION_SIZE; i++)
        {
            state[i] = ((value < BLOCK && i < BLOCK) || (value >= BLOCK && i >= BLOCK)) ? 1.0f : 0.0f;
            state[ACTION_SIZE + i] = i == value ? 1.0f : 0.0f;
        }

    }

    public override List<float> CollectState()
    {
        return state;
    }

    public override void AgentStep(float[] act)
    {
        int action = (int)act[0];
        Log("state: " + toString(state));
        Log("action: " + action);
        if (action < ACTION_SIZE)
        {
            if (state[action + ACTION_SIZE] == 1)
            {
                score++;
                if (score > 10)
                {
                    Log("Done");
                    addReward(1.0f);
                    Log(total);
                    done = true;
                }
                else {
                    Log("Good");
                    addReward(0.1f);
                    gen();
                }
            }
            else {
                Log("Bad");
                addReward(-0.01f);
            }
        }
    }
    public string toString(List<float> state)
    {
        string s = "";
        foreach (float f in state) { s += f + " "; }
        return s;
    }
    public void Log(object s)
    {
        //Debug.Log(s);
    }

    public override void AgentReset()
    {
        score = 0;
        total = 0;
        gen();
    }

    public void addReward(float r)
    {
        reward += r;
        total += r;
    }

    public override void AgentOnDone()
    {

    }

}
