using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]

public class Character : MonoBehaviour
{
    [Header("Velocidad de movimiento:")] [SerializeField] private float speed;

    [Header("Fuerza de salto")] [Tooltip("La fuerza debe ser una magnitud grande")]public float jumpForce;
    
    private Rigidbody2D _rigidbody;
    private AudioSource _audio;

    public AudioClip clipMorir;
    public AudioClip clipGameOver;

    
    [HideInInspector]
    public float _distGround;

    private bool isOnGround;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        isOnGround = false;
    }

    private void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo"))
        {
            isOnGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Suelo"))
        {
            isOnGround = false;
        }
    }

    // Update is called once per frame

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("KillZone"))
        {
            StartCoroutine(Perder());
           // Scene escena = SceneManager.GetActiveScene();
            //SceneManager.LoadScene(escena.name);
        }
        else if (other.CompareTag("WinZone"))
        {
            StartCoroutine(Ganar());
           // SceneManager.LoadScene("GameOverScene");
            
        }
    }


    private IEnumerator Perder()
    {
        _audio.clip = clipMorir;
        _audio.Play();
        yield return new WaitWhile(()=> _audio.isPlaying);
        Scene escena = SceneManager.GetActiveScene();
        SceneManager.LoadScene(escena.name);
        
    }
    
    private IEnumerator Ganar()
    {
        
        _audio.clip = clipGameOver;
        _audio.Play();
        yield return new WaitWhile(()=> _audio.isPlaying);
        SceneManager.LoadScene("GameOverScene");
    }
 
    
    void FixedUpdate()
    {
        float movHorizontal = Input.GetAxis("Horizontal");
        Vector2 movimiento = new Vector2(movHorizontal,0f);
        _rigidbody.AddForce(movimiento * speed);
        
        //saltar con un boton
        if (Input.GetButtonDown("Jump") && isOnGround)
        {
            _rigidbody.AddForce(Vector2.up * jumpForce * 100f, ForceMode2D.Force);
            _audio.Play();
        }
    }


}

