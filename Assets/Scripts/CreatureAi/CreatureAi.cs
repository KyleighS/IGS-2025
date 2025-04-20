using UnityEngine;

public class CreatureAi : MonoBehaviour
{
    public Vector3 startingPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startingPos = transform.position;
    }

    private Vector3 RandomPosition()
    {
        Vector3 randomPos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, -1f)).normalized;
        return startingPos + randomPos * Random.Range(10f, 70f);
    }
}
