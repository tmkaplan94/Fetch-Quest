using UnityEngine;

public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : ScriptableObject
{
    // the one and only instance of type T
    private static T _instance = null;

    // property returns the only instance
    public static T Instance
    {
        get
        {
            // if _instance is not set yet
            if (_instance == null)
            {
                // find out if there is an instance anywhere
                T[] results = Resources.FindObjectsOfTypeAll<T>();

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

                // otherwise,set _instance to the only result found
                _instance = results[0];
            }

            // return the only instance
            return _instance;
        }
    }
}
