using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
 
public class PlatformManager : MonoBehaviour
{
    [SerializeField] private int platformCount = 10;
    private Platform lastPlatform;

    [SerializeField] private Text _score;

    [SerializeField] private Platform firstPlatform;
    [SerializeField] private float maxHeight = 2;
    [SerializeField] private float minHeight = 4;
    [SerializeField] private float maxWeight = 3.5f;
    [SerializeField] private Platform[] platformShapes;

    private void Start()
    {
        lastPlatform = firstPlatform;
        GeneratePlatforms();
    }

    private void GeneratePlatforms()
    {
        for (int i = 0; i < platformCount; i++)
        {
            var position = GetNextPosition();

            int platformShape = Random.Range(0, platformShapes.Length);
            Platform newPlatform = platformShapes[platformShape];
            if (lastPlatform)
            {
                
            }
            lastPlatform = Instantiate(newPlatform, position, Quaternion.identity);
            lastPlatform.SetOriginPosition(position);
        }
    }

    private Vector3 GetNextPosition()
    {
        minHeight += float.Parse(_score.text) * 1 / 1000;
        maxHeight += float.Parse(_score.text) * 1 / 1000;
        var xPosition = Random.Range(-maxWeight, maxWeight);
        var yPosition = Random.Range(minHeight, maxHeight);
        return new Vector3(xPosition, lastPlatform.transform.position.y + yPosition, 0);
    }

    private void MovePlatform(Platform platform)
    {
        platform.transform.position = GetNextPosition();
        lastPlatform = platform;
        platform.SetOriginPosition(platform.transform.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            platformCount = 5;
            Destroy(collision.gameObject);
            GeneratePlatforms();
            /*MovePlatform(collision.gameObject.GetComponent<Platform>());*/
        }
    }
}