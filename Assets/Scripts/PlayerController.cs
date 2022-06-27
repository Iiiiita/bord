using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private AudioSource playerAudio;
    private Animator playerAnim;

    public float gravity = 0.017f;
    private float fallSpeed = 0;
    public AudioClip jumpSound;
    public AudioClip deathSound;
    public AudioClip pointSound;
    public AudioClip clickSound;
    public float jumpForce;
    public bool isGameOver;
    private GameManager scoreCounter;
    private int pointValue = 1;

    public Leaderboards leaderboards;
    private string _userID;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        scoreCounter = GameObject.Find("Canvas").GetComponent<GameManager>();
        playerAudio.PlayOneShot(clickSound, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            fallSpeed -= gravity * Time.deltaTime;
            transform.position += new Vector3(0, (fallSpeed * Time.deltaTime), 0);

            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                fallSpeed = +jumpForce;
                playerAnim.Play("PlayerAnim", -1, 0f);
                playerAudio.PlayOneShot(jumpSound, 1.0f);
            }
        }
        else
        {

        }
    }

    void OnCollisionEnter(Collision other)
    {
        if ((other.gameObject.CompareTag("Pillar") || other.gameObject.CompareTag("Ground")))
        {
            isGameOver = true;
            playerAudio.PlayOneShot(deathSound, 1.0f);
            playerAnim.SetBool("isDead", true);
            playerAnim.Play("dead", -1, 0f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Score"))
        {
            scoreCounter.UpdateScore(pointValue);
            playerAudio.PlayOneShot(pointSound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
