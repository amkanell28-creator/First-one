using UnityEditor.Callbacks;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    public float thrustForce = 1f;
    public float maxSpeed = 5f;

    public GameObject boosterFlame;
    public GameObject TieFighter;
    Rigidbody2D rb;
    private float elapsedTime = 0f;
    private float score = 0f;
    public float scoreMultiplier = 10f;
    public UIDocument uiDocument;
    private Label scoreText;
    public GameObject explosionEffect;
    private Button restartButton;
    public GameObject parentBorder;
    private Label highScore;
    private float highScoreValue = 0f;
    private float collisionSafeTime = 1f;
    private float lastHitTime = -10f;

    private bool invincible = false;
    public int maxHealth = 3;
    private int currentHealth;
    private ProgressBar healthBar;
    void Start()
    {
        scoreText = uiDocument.rootVisualElement.Q<Label>("ScoreLabel");
        rb = GetComponent<Rigidbody2D>();
        restartButton = uiDocument.rootVisualElement.Q<Button>("RestartButton");
        restartButton.style.display = DisplayStyle.None;
        restartButton.clicked += ReloadScene;
        highScore = uiDocument.rootVisualElement.Q<Label>("HighScore");
        highScoreValue = PlayerPrefs.GetFloat("HighScore", 0f);
        highScore.text = "High Score: " + highScoreValue;
        currentHealth = maxHealth;
        healthBar = uiDocument.rootVisualElement.Q<ProgressBar>("HealthBar");
        healthBar.highValue = maxHealth;
        healthBar.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        score = Mathf.FloorToInt(elapsedTime * scoreMultiplier);
        if (Mouse.current.leftButton.isPressed)
        {

            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.value);

            // Determines player direction and moves them forward
            Vector2 direction = (mousePos - transform.position).normalized;
            transform.up = direction;
            rb.AddForce(direction * thrustForce);

        }

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            boosterFlame.SetActive(true);
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            boosterFlame.SetActive(false);
        }

        if (score > 200)
        {
            TieFighter.SetActive(true);
        }
        else if (score > 200)
        {
            TieFighter.SetActive(true);
        }

        scoreText.text = "Score: " + score;

        if (Keyboard.current.vKey.isPressed)
        {
            invincible = true;
        }

        if (Keyboard.current.wKey.isPressed)
        {
            Destroy(gameObject);
        }

        if (Time.time - lastHitTime > collisionSafeTime)
        {
            invincible = false;
        }


    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (score > highScoreValue)
        {
            highScoreValue = score;
            PlayerPrefs.SetFloat("HighScore", highScoreValue);
            highScore.text = "High Score: " + highScoreValue;
            PlayerPrefs.Save();
        }
       
        if (!invincible)
        {
            currentHealth--;
            healthBar.value = currentHealth;
            lastHitTime = Time.time;
            invincible = true;
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
                Instantiate(explosionEffect, transform.position, transform.rotation);
                parentBorder.SetActive(false);
                restartButton.style.display = DisplayStyle.Flex;
            }
        }
    }

    void ReloadScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

