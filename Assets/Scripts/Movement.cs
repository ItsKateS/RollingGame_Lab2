using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public float speed;
    public float jumpForce = 5f;

    public int nextLVL = 0;
    public GameObject loadingScreen;

    private bool isGrounded;

    public int objects;

    private Rigidbody rb;
    private int collects;

    public TMP_Text Score;
    public TMP_Text Result;
    public TMP_Text Timer;

    public GameObject[] Stars;

    private float timeSpent;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        collects = 0;

        SetText();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        timeSpent += Time.deltaTime; 
        int minutes = Mathf.FloorToInt(timeSpent / 60);
        int seconds = Mathf.FloorToInt(timeSpent % 60);
        Timer.text = "Time: " + string.Format("{0:0}:{1:00}", minutes, seconds);

        if (transform.position.y < -10) 
        {
            Result.text = "You Lose!";
            StartCoroutine(ReloadLevel());
        }
    }

    void FixedUpdate()
    {
        float moveHor = Input.GetAxis("Horizontal");
        float moveVer = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3 (moveHor, 0.0f, moveVer);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            collects++;
            SetText();
        }
    }

    private void SetText()
    {
        Score.text = "Score: " + collects.ToString();

        if (collects >= objects)
        {
            Result.text = "You Win!";

            if(timeSpent < 30)
            {
                Stars[0].SetActive(true);
                Stars[1].SetActive(true);
                Stars[2].SetActive(true);
            }

            if (timeSpent < 50)
            {
                Stars[0].SetActive(true);
                Stars[1].SetActive(true);
            }

            if (timeSpent < 70)
            {
                Stars[0].SetActive(true);
            }

            StartCoroutine(LoadNextLevel(nextLVL));

            if(nextLVL > PlayerPrefs.GetInt("currentLVL"))
                PlayerPrefs.SetInt("currentLVL", nextLVL);
        }
    }

    private IEnumerator LoadNextLevel(int level)
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(true);
        Time.timeScale = 1f;

        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(level);
        }
        
        else
            SceneManager.LoadScene(0);
    }

    private IEnumerator ReloadLevel()
    {
        yield return new WaitForSeconds(2f);
        loadingScreen.SetActive(true);
        Time.timeScale = 1f;
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
