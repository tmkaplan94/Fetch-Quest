using Photon.Pun;
using UnityEngine;

public class MonoBehaviorPunCallbacksSingleton<T> : MonoBehaviourPunCallbacks where T : Component
{
    
    // the one and only instance of type T
    private static T _instance;
    
    // property of single instance with a getter
    public static T Instance
    {
        get
        {
            // if _instance has not been set yet
            if (_instance == null)
            {
                // find out if it exists
                T[] results = FindObjectsOfType<T>();

                // check if there are no instances
                if (results.Length == 0)
                {
                    Debug.LogError("ScriptableObjectSingleton Instance returns 0 instances of type " +typeof(T));
                    return null;
                }

                // check if there is more than one instance
                if (results.Length > 1)
                {
                    Debug.LogError("ScriptableObjectSingleton Instance returns more than one instance of type " +typeof(T));
                    return null;
                }

                // otherwise, set _instance to the only result found
                _instance = results[0];
            }
            
            // return the only instance
            return _instance;
        }
    }
    
}