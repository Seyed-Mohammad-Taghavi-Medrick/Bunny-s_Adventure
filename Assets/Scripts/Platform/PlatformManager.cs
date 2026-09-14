using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>Generates an ascending platform path and replenishes platforms that leave its cleanup trigger.</summary>
public class PlatformManager : MonoBehaviour
{
    // Initial/replenishment count, score source, and prefab configuration for platform generation.
    [SerializeField] private int platformCount = 10;
    private Platform lastPlatform;

    [SerializeField] private Text _score;

    [SerializeField] Platform simplePlatform;
    [SerializeField] private Platform firstPlatform;
    [SerializeField] private float maxHeight = 2;
    [SerializeField] private float minHeight = 4;
    [SerializeField] private float maxWeight = 3.5f;
    [SerializeField] private Platform[] platformShapes;
    private int platformShape;
    private Platform newPlatform;

    private void Start()
    {
        // Start from the scene-placed first platform so generated heights form one continuous chain.
        lastPlatform = firstPlatform;
        GeneratePlatforms();
    }

    private void GeneratePlatforms()
    {
        // Pick a random platform variant, except the guard below prevents a specific consecutive breakable combination.
        for (int i = 0; i < platformCount; i++)
        {
            var position = GetNextPosition();
            int platformShape = Random.Range(0, platformShapes.Length);

            Platform newPlatform = platformShapes[platformShape];
            if (!(platformShape == 8 || platformShape == 9 && lastPlatform.GetComponent<BreakablePlatform>()))
            {
                lastPlatform = Instantiate(newPlatform, position, Quaternion.identity);
            }
            else
            {
                // Fall back to the simple platform when the selected shape violates that original rule.
                lastPlatform = Instantiate(simplePlatform, position, Quaternion.identity);
            }


            lastPlatform.SetOriginPosition(position);
        }
    }

    private Vector3 GetNextPosition()
    {
        // Score slightly widens the height range; positions remain within the configured horizontal bounds.
        minHeight += float.Parse(_score.text) * 1 / 100000;
        maxHeight += float.Parse(_score.text) * 1 / 100000;
        var xPosition = Random.Range(-maxWeight, maxWeight);
        var yPosition = Random.Range(minHeight, maxHeight);
        return new Vector3(xPosition, lastPlatform.transform.position.y + yPosition, 0);
    }

    private void MovePlatform(Platform platform)
    {
        // Legacy pooling/recycle approach, retained though trigger handling currently destroys and respawns platforms.
        platform.transform.position = GetNextPosition();
        lastPlatform = platform;
        platform.SetOriginPosition(platform.transform.position);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Old platforms are destroyed and five new ones are appended above the current chain.
        if (collision.gameObject.tag =="Platform")
        {
            platformCount = 5;
            Destroy(collision.gameObject);
            GeneratePlatforms();
            /*MovePlatform(collision.gameObject.GetComponent<Platform>());*/
        }
    }
}
