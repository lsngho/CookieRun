using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    private GameObject[] platforms;
    public GameObject platformPrefab;
    private int currentIndex = 0;
    public Transform spawnPoint;
    private float yRange = 2f;
    public float interval = 1f;
    private float intervalRange = 0.25f;
    private float spawnTimer = 0f;
    public float currentInterval;

    private void Awake()
    {
        platforms = new GameObject[5];
        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i] = Instantiate(platformPrefab);
            platforms[i].SetActive(false);
        }
    }
    private void Start()
    {
        spawnTimer = currentInterval;
    }
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > currentInterval)
        {
            spawnTimer = 0;
            currentInterval = interval + Random.Range(-intervalRange, intervalRange);
            Spawn();
        }
    }
    private void Spawn()
    {
        platforms[currentIndex].SetActive(false);
        Vector3 point = spawnPoint.position;
        point.y += Random.Range(-yRange, yRange);
        platforms[currentIndex].transform.position = point;
        platforms[currentIndex].SetActive(true);

        currentIndex = (currentIndex + 1) % platforms.Length;
    }
}
