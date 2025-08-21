using Mirror;
using UnityEngine;

public class PlayerCubeSpawner : NetworkBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _spawnDistance;
    
    public override void OnStartClient()
    {
        base.OnStartClient();

        EventsProvider.GameplayEvents.OnCubeSpawn += SpawnCube;
    }

    private void SpawnCube()
    {
        EventsProvider.GameplayEvents.OnCubeSpawn -= SpawnCube;
        
        CmdSpawnCube();
    }
    
    [Command]
    private void CmdSpawnCube()
    {
        Vector3 spawnPosition = transform.position + transform.forward * _spawnDistance;
        GameObject cube = Instantiate(_cubePrefab, spawnPosition, Quaternion.identity);
        
        NetworkServer.Spawn(cube);
    }
}
