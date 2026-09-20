using System;
using System.Collections.Generic;
using UnityEngine;

namespace Running
{
    public class PlayerProvideRegistry
    {
        private readonly Dictionary<Type, IPlayerProvider> Providers = new Dictionary<Type, IPlayerProvider>();
        public void Register<TPlayer>(IPlayerProvider<TPlayer> playerProvider) where TPlayer : class
        {
            if (playerProvider == null)
            {
                Debug.LogError(
                    $"Register PlayerProvider failed: " +
                    $"{typeof(TPlayer).Name} provider is null.");
                return;
            }
            Providers[typeof(TPlayer)] = playerProvider;
        }
        public bool TryGet<TPlayer>(out IPlayerProvider<TPlayer> playerProvider) where TPlayer : class
        {
            playerProvider = null;
            if (Providers.TryGetValue(typeof(TPlayer), out IPlayerProvider provider))
            {
                playerProvider = provider as IPlayerProvider<TPlayer>;
            }
            if (playerProvider == null)
            {
                Debug.LogError(
                    $"Registered PlayerProvider type mismatch: " +
                    $"{typeof(TPlayer).Name}.");
                return false;
            }

            return true;
        }
    }
}