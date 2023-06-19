using UnityEngine;

[CreateAssetMenu(fileName = "New spawn point", menuName = "Level/LevelSpawn")]
public class LevelSpawnSO : ScriptableObject
{
    public Vector3 CurrentSpawnLocation;
    public Vector3 defaultSpawn;

    public void SetSpawn(Transform spawnPoint)
    {
        CurrentSpawnLocation = spawnPoint.transform.position;
    }
    private void OnDisable()
    {
        CurrentSpawnLocation = defaultSpawn;
    }
    private void OnEnable()
    {
        CurrentSpawnLocation = defaultSpawn;
    }
}