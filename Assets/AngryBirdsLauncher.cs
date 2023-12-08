using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AngryBirdsLauncher : MonoBehaviour
{
    public GameObject box;
    public float shootingPowerMultiplier = 5f;
    public TextMeshProUGUI infoText;

    private Vector2 initialPosition;
    private bool isBoxSelected = false;
    private Camera mainCamera;

    // Lägg till en collider för scenens gränser
    public Collider2D sceneBounds;

    void Start()
    {
        initialPosition = box.transform.position;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isBoxSelected)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null && hit.collider.gameObject == box)
            {
                isBoxSelected = true;
            }
        }

        if (isBoxSelected)
        {
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float distanceX = Mathf.Clamp(mousePosition.x - initialPosition.x, -5f, 5f);
            box.transform.position = new Vector2(initialPosition.x + distanceX, initialPosition.y);

            if (Input.GetMouseButtonUp(0))
            {
                Rigidbody2D boxRigidbody = box.GetComponent<Rigidbody2D>();

                float shootingPower = distanceX * shootingPowerMultiplier;
                Vector2 shootingDirection = new Vector2(1f, 0.5f);
                Vector2 shootingForce = shootingDirection * shootingPower;

                boxRigidbody.AddForce(shootingForce, ForceMode2D.Impulse);

                isBoxSelected = false;
            }
        }

        CheckMissionStatus();
    }

    void CheckMissionStatus()
    {
        if (box.transform.position.y > 10f)
        {
            infoText.text = "Mission Completed!";
            RestartLevel();
        }
        else if (!sceneBounds.bounds.Contains(box.transform.position))
        {
            infoText.text = "Mission Failed!";
            RestartLevel();
        }
        else
        {
            infoText.text = "";
        }
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
