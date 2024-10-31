using System.Collections;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public float speed;
    public float size;
    public Color color;
    private Vector3 targetPosition;

    private void Start()
    {
        SetRandomTargetPosition();
        ApplyAttributes();
    }

    private void Update()
    {
        MoveTowardTarget();
    }

    // Mutate attributes randomly
    public void Mutate()
    {
        speed += Random.Range(-0.1f, 0.1f);
        size += Random.Range(-0.1f, 0.1f);
        size = Mathf.Clamp(size, 0.5f, 2.0f);
        color = new Color(Random.value, Random.value, Random.value);
    }

    // Apply the attributes
    private void ApplyAttributes()
    {
        transform.localScale = Vector3.one * size;
        GetComponent<Renderer>().material.color = color;
    }

    // Move toward a target
    private void MoveTowardTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetRandomTargetPosition();
        }
    }

    private void SetRandomTargetPosition()
    {
        targetPosition = new Vector3(Random.Range(-10f, 10f), transform.position.y, Random.Range(-10f, 10f));
    }

    // Fitness function based on some criteria (e.g., how close it gets to center)
    public float CalculateFitness()
    {
        return 1f / Vector3.Distance(transform.position, Vector3.zero);
    }
}