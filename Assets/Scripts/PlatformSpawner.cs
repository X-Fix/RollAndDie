using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public GameObject diamondGreen;
    public GameObject diamondYellow;
    public GameObject diamondRed;

    private GameManager gameManager;
    private Vector3 lastPosition;
    private string lastDirection;
    private float size;

    private static int diamondCount;

    void Start()
    {
        gameManager = GameManager.instance;
        diamondCount = 0;
        lastPosition = new Vector3(0, 0, 6);
        lastDirection = "z";
        size = platform.transform.localScale.x;
    }

    public void StartSpawningPlatforms()
    {
        InvokeRepeating(nameof(SpawnNext), 0f, 0.2f);
    }

    public void StopSpawningPlatforms()
    {
        CancelInvoke(nameof(SpawnNext));
    }

    private void SpawnNext()
    {
        Vector3 position = GetNextPosition();

        SpawnPlatform(position);

        int frequency = gameManager.gameMode == GameMode.WarmUp
            ? 2
            : 4;

        if (OneIn(frequency))
            SpawnDiamond(position);
    }

    private void SpawnDiamond(Vector3 position)
    {
        GameObject diamond;
        if (diamondCount < 10)
            diamond = diamondGreen;
        else if (diamondCount < 25)
            diamond = diamondYellow;
        else
            diamond = diamondRed;

        Instantiate(
            diamond,
            new Vector3(position.x, position.y + 1, position.z),
            diamond.transform.rotation
        );

        diamondCount++;
    }

    private void SpawnPlatform(Vector3 position)
    {
        Instantiate(platform, position, Quaternion.identity);
    }

    private Vector3 GetNextPosition()
    {
        int frequency = gameManager.gameMode == GameMode.WarmUp
            ? 4
            : 2;

        return OneIn(frequency)
            ? ChangeDirection()
            : MaintainDirection();
    }

    private Vector3 ChangeDirection()
    {
        if (lastDirection == "x")
        {
            lastDirection = "z";
            return GetNextZPosition();
        }
        else
        {
            lastDirection = "x";
            return GetNextXPosition();
        }
    }

    private Vector3 MaintainDirection() => lastDirection == "x"
            ? GetNextXPosition()
            : GetNextZPosition();

    private Vector3 GetNextXPosition() => new Vector3(lastPosition.x += size, lastPosition.y, lastPosition.z);
    private Vector3 GetNextZPosition() => new Vector3(lastPosition.x, lastPosition.y, lastPosition.z += size);
    private bool OneIn(int chance) => Random.Range(0, chance) < 1;
}
