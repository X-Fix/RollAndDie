using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject ball;
    public float lerpRate;

    private GameManager gameManager;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = ball.transform.position - transform.position;
        gameManager = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.gameState == GameState.Started)
            FollowBall();
    }

    void FollowBall()
    {
        Vector3 position = transform.position;
        Vector3 targetPosition = ball.transform.position - offset;
        position = Vector3.Lerp(position, targetPosition, lerpRate * Time.deltaTime);
        transform.position = position;
    }
}
