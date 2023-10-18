using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Unicorn : MonoBehaviour
{
    [SerializeField] private float launchForce = 500;
    [SerializeField] private float maxDragDistance = 5;
    [SerializeField] private int startingLives = 3; // Initial number of lives.

    private Vector2 startPosition;
    private Rigidbody2D rb2D;
    private SpriteRenderer spriteRenderer;
    private int lives; // Current number of lives.

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        startPosition = rb2D.position;
        rb2D.isKinematic = true;
        lives = startingLives; // Initialize lives.
    }

    private void OnMouseDown()
    {
        spriteRenderer.color = Color.magenta;
    }

    private void OnMouseUp()
    {
        Vector2 currentPosition = rb2D.position;
        Vector2 direction = (startPosition - currentPosition).normalized;

        rb2D.isKinematic = false;
        rb2D.AddForce(direction * launchForce);
        spriteRenderer.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = new Vector2(mousePosition.x, mousePosition.y);

        float distance = Vector2.Distance(desiredPosition, startPosition);
        if (distance > maxDragDistance)
        {
            Vector2 direction = (desiredPosition - startPosition).normalized;
            desiredPosition = startPosition + (direction * maxDragDistance);
        }

        if (desiredPosition.x > startPosition.x)
        {
            desiredPosition.x = startPosition.x;
        }

        rb2D.position = desiredPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    private IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        rb2D.position = startPosition;
        rb2D.isKinematic = true;
        rb2D.velocity = Vector2.zero;

        // Deduct a life.
        lives--;

        // Log the remaining lives to the console.
        Debug.Log("Remaining Lives: " + lives);

        // Check for losing conditions (when lives reach 0).
        if (lives <= 0)
        {
            HandleLoss();
        }
    }

    private void HandleLoss()
    {
        // Handle the losing scenario here.
        // For example, you can display a "Game Over" screen or perform other loss-related actions.
        Debug.Log("Game Over");

        // Restart the current scene.
        ReloadCurrentScene();
    }

    private void ReloadCurrentScene()
    {
        // Get the current scene's name and reload it.
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
