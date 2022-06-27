using UnityEngine;

public class PillarSpawner : MonoBehaviour
{
    public float maxTime = 1;
    private float timer = 0;
    public GameObject Pillars;
    public float height;
    public float heightOriginal;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        height = heightOriginal;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer > maxTime && playerController.isGameOver == false)
        {
            GameObject newPillars = Instantiate(Pillars);
            newPillars.transform.position = transform.position + new Vector3(0, Random.Range(-height, height) - 1, 0);
            height += 0.05f;
            Destroy(newPillars, 15);

            timer = 0;
        }

        timer += Time.deltaTime;
    }
}
