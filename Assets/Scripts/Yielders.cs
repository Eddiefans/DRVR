using System.Collections.Generic;
using UnityEngine;

public class Yielders
{
    static Dictionary<float, WaitForSeconds> time = new Dictionary<float, WaitForSeconds>(100);
    static Dictionary<float, WaitForSecondsRealtime> realTime = new Dictionary<float, WaitForSecondsRealtime>(50);
 
    static WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();
    public static WaitForEndOfFrame EndOfFrame {
        get{ return endOfFrame;}
    }
 
    static WaitForFixedUpdate fixedUpdate = new WaitForFixedUpdate();
    public static WaitForFixedUpdate FixedUpdate{
        get{ return fixedUpdate; }
    }
 
    public static WaitForSeconds Get(float seconds){
        if(!time.ContainsKey(seconds))
            time.Add(seconds, new WaitForSeconds(seconds));
        return time[seconds];
    }
    
    public static WaitForSecondsRealtime GetRealtime(float seconds){
        if(!realTime.ContainsKey(seconds))
            realTime.Add(seconds, new WaitForSecondsRealtime(seconds));
        return realTime[seconds];
    }
}
