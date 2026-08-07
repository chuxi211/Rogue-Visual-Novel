using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Running
{
    public class PlayerFactory
    {
        public TPlayer CreatePlayer<TPlayer>(GameObject playerPrefab,Transform parent)where TPlayer:Component
        {
            if (playerPrefab == null)
            {
                Debug.LogError("CreatePlayer failed: PlayerPrefab is null.");
                return null;
            }

            if (parent == null)
            {
                Debug.LogError("CreatePlayer failed: Parent is null.");
                return null;
            }
            GameObject playerObject = GameObject.Instantiate(playerPrefab, parent, false);
            TPlayer player = playerObject.GetComponent<TPlayer>();
            if (player == null)
            {
                Debug.LogError(
                    $"CreatePlayer failed: {typeof(TPlayer).Name} component was not found.");
                GameObject.Destroy(playerObject);
                return null;
            }
            return player;
        }
    }
}