using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class PlayerCache
    {
        public Dictionary<Type,Component> Players { get; private set; } = new Dictionary<Type,Component>();
        //好好想想这里的键值对到底应该是什么
        public bool TryGet<TPlayer>(out TPlayer Player) where TPlayer : Component
        {
            Player = null;

            if (!Players.TryGetValue(
                    typeof(TPlayer),
                    out Component CachedPlayer))
            {
                return false;
            }

            Player = CachedPlayer as TPlayer;

            if (Player == null)
            {
                Players.Remove(typeof(TPlayer));
                Debug.LogWarning(
                    $"Cached Player is invalid: " +
                    $"{typeof(TPlayer).Name}.");
                return false;
            }

            return true;
        }
        public void Set<TPlayer>(TPlayer Player)
              where TPlayer : Component
        {
            if (Player == null)
            {
                Debug.LogError(
                    $"Set PlayerCache failed: " +
                    $"{typeof(TPlayer).Name} is null.");
                return;
            }

            Players[typeof(TPlayer)] = Player;
        }
    }
}