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

    // XR 컨트롤러 입력용
    public ContinuousMoveProvider moveProvider; // XR Origin에 붙어있는 컴포넌트
    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (clearPanelObj != null)
        {
            clearPanelObj.SetActive(false); // 시작 시 비활성화
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
                Debug.Log("열쇠 3개를 찾아오세요");
            }
            else
            {
                Debug.Log("탈출성공!");
                ShowClearPanel();
            }
        }
    }
    private void ShowClearPanel()
    {
        if (clearPanelObj != null)
        {
            clearPanelObj.SetActive(true);
            Invoke("QuitGame", 2.0f); // 2초 후 게임 종료
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서는 Play 모드 종료
#else
        Application.Quit(); // 빌드된 게임에서는 종료
#endif
    }
}
