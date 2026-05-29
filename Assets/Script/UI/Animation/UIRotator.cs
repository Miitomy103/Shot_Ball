using UnityEngine;

public class UIRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f; // “x/•b

    private void Update()
    {
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }
}