using UnityEngine;
using UnityEngine.InputSystem;

public class Obstacle : MonoBehaviour
{
    public float minSize = 0.5f;
    public float maxSize = 2.0f;
    Rigidbody2D rb;
    public float minSpeed = 100f;
     public float maxSpeed = 250f;
    public float maxSpinSpeed = 10f;

    private bool speedBoosted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float randomSize = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(randomSize, randomSize, 1);
        //randomizes size

        rb = GetComponent<Rigidbody2D>();

        Vector2 randomDirection = Random.insideUnitCircle;
        // Creates random direction

        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        rb.AddForce(randomDirection * randomSpeed);
        // Creates the random speed and direction for obstacles

        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb.AddTorque(randomTorque);
        // Adds random spin to obstacles

    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame && !speedBoosted)
        {
            rb.linearVelocity *= 3f;
            speedBoosted = true;

        }
    }
}
