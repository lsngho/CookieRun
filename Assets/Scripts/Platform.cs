using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject[] obstacles;
    public GameObject platform;
    public GameObject coin;
    private float positionX;
    public float obstacleRatio = 0.2f;
    public float coinRatio = 0.5f;
    private int minLength = 2;

    private void Start()
    {
        Vector3 worldParentLeft = platform.GetComponent<BoxCollider2D>().bounds.min;
        Vector3 worldParentRight = platform.GetComponent<BoxCollider2D>().bounds.max;
        float localParentLeft = transform.InverseTransformPoint(worldParentLeft).x;
        float localParentRight = transform.InverseTransformPoint(worldParentRight).x;
        for (positionX = localParentLeft + 2f; positionX <= localParentRight - 2f; positionX++)
        {
            bool isSpawned = false;

            if (Random.value < obstacleRatio)
            {
                isSpawned = true;
            }

            if (isSpawned)
            {
                SpawnOnParent(obstacles);
            }
            else
            {
                SpawnOnParent(coin);
            }
        }
    }
    private void SpawnOnParent(GameObject[] gameObjects)
    {
        Debug.Log("장애물 생성해");
        int obstacleIndex = Random.Range(0, gameObjects.Length);

        GameObject instantiatedObstacle = Instantiate(gameObjects[obstacleIndex]);
        instantiatedObstacle.transform.localPosition = new Vector2(positionX, instantiatedObstacle.transform.position.y);
        instantiatedObstacle.SetActive(false);
        instantiatedObstacle.transform.SetParent(transform, false);

        instantiatedObstacle.SetActive(true);

        positionX += minLength;
    }
    private void SpawnOnParent(GameObject gameObject)
    {

        Debug.Log("코인 생성해");
        GameObject instantiatedObstacle = Instantiate(gameObject);
        instantiatedObstacle.transform.localPosition = new Vector2(positionX, instantiatedObstacle.transform.position.y);
        instantiatedObstacle.SetActive(false);
        instantiatedObstacle.transform.SetParent(transform, false);

        instantiatedObstacle.SetActive(true);

        positionX += minLength;
    }
}
