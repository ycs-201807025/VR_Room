using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private float h = 0.0f;
    private float v = 0.0f;
    private float r = 0.0f;
    private float rotationSpeed = 100.0f;
    private float moveSpeed = 10.0f;
    private Transform playerTransform;
    private int key = 0;
    public AudioClip keySfx;
    private AudioSource audioSource;
    void Start()
    {
        playerTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        r = Input.GetAxis("Mouse X");

        //Debug.Log("Horizontal: " + h.ToString() + ", Vertical: " + v.ToString());
        playerTransform.Translate(new Vector3(h, 0, v) * moveSpeed * Time.deltaTime);
        playerTransform.Rotate(new Vector3(0, r, 0) * rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Key")
        {
            Destroy(collision.gameObject);
            key += 1;
            Debug.Log("Key collected! Total keys: " + key);
            audioSource.PlayOneShot(keySfx, 1.0f);
        }

        if(collision.gameObject.tag == "Box")
        {
            if (key < 3)
            {
                Debug.Log("열쇠 3개를 찾아오세요");
            }
            else
            {
                Debug.Log("탈출성공!");
                Destroy(collision.gameObject);
            }
        }
    }
}
