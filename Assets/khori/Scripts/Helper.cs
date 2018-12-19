using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public static class Helper {

    public static float Longevity(float time, float delay, float period)
    {
        if (period <= 0) { return 1; }
        else if (delay > time) { return 0; }
        //
        return Mathf.Min(Mathf.Max(0.0f, time - delay) / period, 1.0f);
    }



    public static Actor GetNetworkActor(NetworkInstanceId id)
    {
        foreach (Actor a in GameObject.FindObjectsOfType<Actor>())
        {
            if (a.netId == id) { return a; }
        }

        return null;
    }



    public static void ClearMessages()
    {
        GameManager.Instance().DisplayMessage("");
    }
    public static void DisplayMessage(string message)
    {
        GameManager.Instance().AppendMessage(message);
    }
}
