using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour
{
    private int key = 0;
    public AudioClip keySfx;
    private AudioSource audioSource;

    public GameObject clearPanelObj;

    // XR ��Ʈ�ѷ� �Է¿�
    public ContinuousMoveProvider moveProvider; // XR Origin�� �پ��ִ� ������Ʈ
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (clearPanelObj != null)
        {
            clearPanelObj.SetActive(false); // ���� �� ��Ȱ��ȭ
        }
    }


    void Update()
    {
        
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

        if(collision.gameObject.tag == "door")
        {
            if (key < 3)
            {
                Debug.Log("���� 3���� ã�ƿ�����");
            }
            else
            {
                Debug.Log("Ż�⼺��!");
                ShowClearPanel();
            }
        }
    }
    private void ShowClearPanel()
    {
        if (clearPanelObj != null)
        {
            clearPanelObj.SetActive(true);
            Invoke("QuitGame", 2.0f); // 2�� �� ���� ����
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ����� Play ��� ����
#else
        Application.Quit(); // ����� ���ӿ����� ����
#endif
    }
}
