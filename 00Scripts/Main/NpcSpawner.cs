using UnityEngine;
using Random = UnityEngine.Random;

public class NpcSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPointA;
    [SerializeField] private Transform spawnPointB;
    public Transform runDestinationPoint;

    [SerializeField] private GameObject npcAPrefab;
    [SerializeField] private GameObject npcBPrefab;

    public void Spawn()
    {
        foreach (Transform npc in spawnPointA.transform)
        {
            Destroy(npc.gameObject);
        }
        
        foreach (Transform npc in spawnPointB.transform)
        {
            Destroy(npc.gameObject);
        }
        
        int random = Random.Range(0, 2);
        
        Instantiate(random == 0 ? npcAPrefab : npcBPrefab, spawnPointA.position, Quaternion.identity, spawnPointA);
        
        random = Random.Range(0, 2);
        
        Instantiate(random == 0 ? npcAPrefab : npcBPrefab, spawnPointB.position, Quaternion.identity, spawnPointB);
    }
}
