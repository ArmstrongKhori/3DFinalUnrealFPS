using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {

    public static float Longevity(float time, float delay, float period)
    {
        if (period <= 0) { return 1; }
        else if (delay > time) { return 0; }
        //
        return Mathf.Min(Mathf.Max(0.0f, time - delay) / period, 1.0f);
    }

}
