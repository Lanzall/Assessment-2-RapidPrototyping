using UnityEngine;
using System.Collections;

public class BranchingSpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject prefab;                // The object to spawn
    public float spawnInterval = 0.5f;       // Time between spawns
    public float spawnDuration = 5f;         // Total time to keep spawning
    public float moveSpeed = 2f;             // How fast each object moves
    public float maxRotation = 30f;          // Max Y-axis rotation change per step (degrees)

    private float elapsedTime = 0f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (elapsedTime < spawnDuration)
        {
            // Use the current position and rotation of the spawner
            Vector3 spawnPosition = transform.position;
            Quaternion baseRotation = transform.rotation;

            // Apply a small random Y-axis rotation offset
            float yRotationOffset = Random.Range(-maxRotation, maxRotation);
            Quaternion spawnRotation = baseRotation * Quaternion.Euler(0f, yRotationOffset, 0f);

            // Instantiate the object
            GameObject obj = Instantiate(prefab, spawnPosition, spawnRotation);

            // Try to move it forward using Rigidbody
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = obj.transform.forward * moveSpeed;
            }

            // Wait before spawning the next one
            yield return new WaitForSeconds(spawnInterval);
            elapsedTime += spawnInterval;
        }
    }
}
