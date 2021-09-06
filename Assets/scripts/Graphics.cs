using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Graphics : MonoBehaviour
{
    public AIPath ai;

    // Update is called once per frame
    void Update()
    {
        if (ai.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (ai.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
