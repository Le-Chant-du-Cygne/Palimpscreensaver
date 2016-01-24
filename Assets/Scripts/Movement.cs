using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public float minTimeToEnd;
    public float maxTimeToEnd;

    private Generator generator;
    private Transform corridors;

    private Vector3 lastPosition;
    private float currentSpeed;
    private float currentMaxSpeed;
    private float currentTimeToEnd;
    private float endTimer;
    private bool accelerating;

	void Start ()
    {
        generator = GameObject.FindObjectOfType<Generator>();
        corridors = GameObject.Find("Corridors").transform;

        lastPosition = Vector3.zero;
        currentSpeed = 0f;
        currentMaxSpeed = Random.Range(minSpeed, maxSpeed);
        currentTimeToEnd = Random.Range(minTimeToEnd, maxTimeToEnd);
        endTimer = 0f;
        accelerating = true;
    }
	
	void Update ()
    {
        // Movement
        float currentSpeed = 0f;
        if (accelerating)
        {
            currentSpeed = Interpolate.Ease(Interpolate.EaseType.EaseInQuad)(minSpeed, currentMaxSpeed, endTimer, currentTimeToEnd);
        }
        else
        {
            currentSpeed = Interpolate.Ease(Interpolate.EaseType.EaseInQuad)(currentMaxSpeed, minSpeed, endTimer, currentTimeToEnd);
        }
        transform.Translate(new Vector3(0f, 0f, currentSpeed) * Time.deltaTime);
        endTimer += Time.deltaTime;

        // Reset parameters
        if (endTimer >= currentTimeToEnd)
        {
            if (accelerating)
            {
                accelerating = false;
            }
            else
            {
                currentMaxSpeed = Random.Range(minSpeed, maxSpeed);
                currentTimeToEnd = Random.Range(minTimeToEnd, maxTimeToEnd);
                accelerating = true;
            }
            endTimer = 0f;
        }

        // Destroy old corridors
        if (transform.position.z - lastPosition.z > 4f * generator.maxScale)
        {
            Destroy(corridors.GetChild(0).gameObject);
            lastPosition = transform.position;
        }
	}
}
