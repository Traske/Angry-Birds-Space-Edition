using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float launchForceMultiplier = 5f;

    private Vector2 dragStartPos;
    private Vector2 dragEndPos;
    private float dragDistance; // Declare dragDistance at the class level
    private bool isDragging = false;

    // Additional variables for new features
    private bool isMouseOverBox = false;
    private bool canLaunch = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDragging();
        }

        if (isDragging)
        {
            UpdateDragging();
        }

        if (Input.GetMouseButtonUp(0) && canLaunch)
        {
            EndDragging();
            LaunchProjectile();
        }
    }

    void OnGUI()
    {
        // Display force and angle on the screen
        if (isDragging)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.white;

            // Calculate angle
            Vector2 dragDirection = (dragEndPos - dragStartPos).normalized;
            float angle = Mathf.Atan2(dragDirection.y, dragDirection.x) * Mathf.Rad2Deg;

            // Display force and angle on the screen
            GUI.Label(new Rect(10, 10, 200, 20), "Force: " + (int)(dragDistance * launchForceMultiplier), style);
            GUI.Label(new Rect(10, 30, 200, 20), "Angle: " + (int)angle, style);
        }
    }

    void StartDragging()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            // Check if the mouse is over the box
            isMouseOverBox = true;
            canLaunch = false;
        }
        else
        {
            isMouseOverBox = false;
        }

        dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void UpdateDragging()
    {
        dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate the force and angle based on the drag direction
        Vector2 dragDirection = (dragEndPos - dragStartPos).normalized;
        dragDistance = Vector2.Distance(dragStartPos, dragEndPos);

        // Check if the mouse is over the box and set canLaunch accordingly
        canLaunch = isMouseOverBox && dragDistance > 0.1f;
    }

    void EndDragging()
    {
        isDragging = false;
    }

    void LaunchProjectile()
    {
        // Check if a projectile already exists before launching a new one
        if (GameObject.FindObjectOfType<Rigidbody2D>() == null)
        {
            // Instantiate a new projectile at the position of the launcher
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            // Get the launch direction based on the drag
            Vector2 launchDirection = (dragEndPos - dragStartPos).normalized;

            // Calculate the launch force based on the drag distance
            float launchForce = dragDistance * launchForceMultiplier;

            // Apply an impulse force to the projectile
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
        }
    }
}
