using UnityEngine;

public class Collectables : MonoBehaviour
{
    public float rotatioanlSpeed =0.5f;
    private GameManager gameManager;
    public GameObject collectableEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, rotatioanlSpeed, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.AddScore(1);
            Instantiate(collectableEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
