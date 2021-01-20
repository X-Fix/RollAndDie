using UnityEngine;

public class BallController : MonoBehaviour
{
    private float velocity;
    public GameObject particle;

    private Vector3 velocityX;
    private Vector3 velocityZ;
    private Rigidbody rigidBody;
    private ScoreManager scoreManager;
    private GameManager gameManager;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        UpdateVelocity(5f);

        scoreManager = ScoreManager.instance;
        gameManager = GameManager.instance;
    }

    void Update()
    {
        if (velocity < 8 && gameManager.gameMode == GameMode.Normal)
        {
            UpdateVelocity(8);
        }
        else if (velocity < 10 && gameManager.gameMode == GameMode.Hard)
        {
            UpdateVelocity(10);
        }

        if (gameManager.gameState == GameState.Started)
        {
            if (IsOutOfBounds())
            {
                gameManager.EndGame();
                return;
            }

            if (Input.GetMouseButtonDown(0))
                SwitchDirection();

            MaintainSpeed();
        }
        else if (gameManager.gameState == GameState.End)
        {
            rigidBody.constraints = RigidbodyConstraints.None;
            rigidBody.velocity = new Vector3(0, -25f, 0);
        }
        else if (gameManager.gameState == GameState.Idle && Input.GetMouseButtonDown(0))
        {
            rigidBody.velocity = velocityZ;

            gameManager.StartGame();
        }
    }

    private void UpdateVelocity(float newVelocity)
    {
        velocity = newVelocity;
        velocityX = new Vector3(newVelocity, 0, 0);
        velocityZ = new Vector3(0, 0, newVelocity);
    }

    private void MaintainSpeed()
    {
        if (rigidBody.velocity.z > 0)
        {
            rigidBody.velocity = velocityZ;
        }
        else if (rigidBody.velocity.x > 0)
        {
            rigidBody.velocity = velocityX;
        }
    }

    private void SwitchDirection()
    {
        if (rigidBody.velocity.z > 0)
        {
            rigidBody.transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidBody.velocity = velocityX;
        }
        else if (rigidBody.velocity.x > 0)
        {
            rigidBody.transform.rotation = Quaternion.Euler(0, 0, 0);
            rigidBody.velocity = velocityZ;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.CompareTag("Diamond"))
            return;

        GetComponent<AudioSource>().Play();
        Destroy(collider.gameObject);

        GameObject particleInstance = Instantiate(particle, collider.gameObject.transform.position, particle.transform.rotation);
        Destroy(particleInstance, 1f);

        scoreManager.IncrementScore();

        if (gameManager.gameMode == GameMode.WarmUp && scoreManager.score >= 10
            || gameManager.gameMode == GameMode.Normal && scoreManager.score >= 25)
            gameManager.SpeedUp();
    }

    private bool IsOutOfBounds()
    {
        Debug.DrawRay(transform.position, Vector3.down, Color.red);
        return !Physics.Raycast(transform.position, Vector3.down, 1f);
    }
}
