using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LaunchBox : MonoBehaviour
{
    public Text forceText;
    public Text angleText;

    private bool isPressed;
    private float releaseDelay;
    private float maxDragDistance = 2f;
    private Rigidbody2D rb;
    private SpringJoint2D sj;
    private Rigidbody2D slingRb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sj = GetComponent<SpringJoint2D>();
        slingRb = sj.connectedBody;
        releaseDelay = 1 / (sj.frequency * 4);
    }

    void Update()
    {
        if (isPressed)
        {
            DragBox();
            UpdateUI();
        }
    }

    void DragBox()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float distance = Vector2.Distance(mousePos, slingRb.position);

        if (distance > maxDragDistance)
        {
            Vector2 direction = (mousePos - slingRb.position).normalized;
            rb.position = slingRb.position + direction * maxDragDistance;
        }
        else
        {
            rb.position = mousePos;
        }
    }

    void UpdateUI()
    {
        Vector2 direction = (slingRb.position - rb.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        float force = Mathf.Clamp(Vector2.Distance(slingRb.position, rb.position), 0f, maxDragDistance);

        angleText.text = $"Angle: {angle:F2} degrees";
        forceText.text = $"Force: {force:F2}";
    }

    private void OnMouseDown()
    {
        isPressed = true;
        rb.isKinematic = true;
    }

    private void OnMouseUp()
    {
        isPressed = false;
        rb.isKinematic = false;
        StartCoroutine("Release");
    }

    private IEnumerator Release()
    {
        yield return new WaitForSeconds(releaseDelay);
        sj.enabled = false;
    }
}
