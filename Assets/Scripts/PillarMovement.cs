using UnityEngine;

public class PillarMovement : MonoBehaviour
{
    public float speed;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isGameOver == false)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
    }
}
