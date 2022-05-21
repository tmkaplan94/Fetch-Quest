using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Players", menuName = "ScriptableObject/List/Players")]
public class Players : ScriptableObject
{
    private Dictionary<int, bool> _playersReadyToPlay = new Dictionary<int, bool>();

    // if item is not in list, it gets added
    public void Add(int id)
    {
        if (!_playersReadyToPlay.ContainsKey(id))
        {
            _playersReadyToPlay.Add(id, false);
        }
        else
        {
            _playersReadyToPlay[id] = false;
        }

        Debug.Log("Added player " + id);
    }
    
    // make this player ready
    public void ImReady(int id)
    {
        if (_playersReadyToPlay.ContainsKey(id))
        {
            _playersReadyToPlay[id] = true;
        }
    }

    // if item is in list, it gets removed
    public void Remove(int id)
    {
        if (_playersReadyToPlay.ContainsKey(id))
        {
            _playersReadyToPlay.Remove(id);
        }
    }

    // returns how many items are in dictionary
    public int Length()
    {
        return _playersReadyToPlay.Count;
    }

    // clears all items from the dictionary
    public void ClearAll()
    {
        _playersReadyToPlay.Clear();
    }
    
    // checks if all players are ready
    public bool AllAreReadyToPlay()
    {
        bool ready = false;
        foreach (var player in _playersReadyToPlay)
        {
            ready = player.Value;
            Debug.Log("Player" + player.Key + " ready: " + ready);
            if (!ready)
            {
                break;
            }
        }
        return ready;
    }
}