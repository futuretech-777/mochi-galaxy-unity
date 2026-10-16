using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace StitchApp
{
    /**
     * MochiNetworkManager
     * 
     * Handles the Photon Fusion 2 lifecycle.
     * Manages session connection, player joining, and spawning the Mochi prefab.
     */
    public class MochiNetworkManager : MonoBehaviour, INetworkRunnerCallbacks
    {
        [Header("Prefabs")]
        [SerializeField] private NetworkPrefabRef playerPrefab;
        
        private NetworkRunner _runner;
        private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

        private async void Start()
        {
            // Auto-start a session for testing
            StartGame(GameMode.Shared);
        }

        async void StartGame(GameMode mode)
        {
            _runner = gameObject.AddComponent<NetworkRunner>();
            _runner.ProvideInput = true;

            var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
            var sceneInfo = new NetworkSceneInfo();
            if (scene.IsValid) sceneInfo.AddSceneRef(scene, LoadSceneMode.Single);

            await _runner.StartGame(new StartGameArgs()
            {
                GameMode = mode,
                SessionName = "MochiWorldTest",
                Scene = scene,
                SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
            });
            
            Debug.Log("✨ Mochi Galaxy Network Started!");
        }

        // --- INetworkRunnerCallbacks ---

        public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
        {
            if (runner.IsServer || runner.Topology == SimulationConfig.Topologies.Shared)
            {
                Debug.Log($"🧸 Player {player} joined the AstroPuff World!");
                
                // Spawn the player mascot
                Vector3 spawnPosition = new Vector3(0, 1, 0);
                NetworkObject networkPlayerObject = runner.Spawn(playerPrefab, spawnPosition, Quaternion.identity, player);
                
                _spawnedCharacters.Add(player, networkPlayerObject);
            }
        }

        public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
        {
            if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
            {
                runner.Despawn(networkObject);
                _spawnedCharacters.Remove(player);
                Debug.Log($"💔 Player {player} left the Mochi Planet.");
            }
        }

        // Required Boilerplate for INetworkRunnerCallbacks
        public void OnInput(NetworkRunner runner, NetworkInput input) { }
        public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }
        public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }
        public void OnConnectedToServer(NetworkRunner runner) { }
        public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }
        public void OnConnectRequest(NetworkRunner runner, NetworkControlMessageMeta metadata, byte[] buffer) { }
        public void OnConnectFailed(NetworkRunner runner, NetAddress address, NetConnectReason reason) { }
        public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
        public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) { }
        public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }
        public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }
        public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ArraySegment<byte> data) { }
        public void OnSceneLoadDone(NetworkRunner runner) { }
        public void OnSceneLoadStart(NetworkRunner runner) { }
        public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }
        public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, float progress) { }
    }
}
