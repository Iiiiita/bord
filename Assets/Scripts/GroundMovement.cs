using UnityEngine;

public class GroundMovement : MonoBehaviour
{
    public float speed;
    private PlayerController playerController;
    private Vector3 startPos;
    private double repeatWidth;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        // Set the start position.
        startPos = transform.position;
        // Set the width where the repositioning happens.
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isGameOver == false)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        // Reset position if backround moves too far.
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
