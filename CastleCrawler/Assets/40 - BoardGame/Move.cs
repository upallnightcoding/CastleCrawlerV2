using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move 
{
    private List<Step> move = new List<Step>();

    public void Add(Step step)
    {
        move.Add(step);
    }
}
