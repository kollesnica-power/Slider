using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private const float PLAYER_TRANSITION = 3.0f;                               // Distance that player going while jumping forward

    [SerializeField] private float m_JumpSpeed = 3.0f;
    [SerializeField] private float m_MoveSpeed = 3.0f;
    [SerializeField] private Animator m_Animator;

    private bool isJumping = false;
    private bool isLevelEnd = false;
    private Vector3 m_StartLevelPosition;
    private Vector3 m_StartGamePosition;



    // Use this for initialization
    void Start () {
        m_StartLevelPosition = transform.position;
        m_StartGamePosition = m_StartLevelPosition;
    }
	
	// Update is called once per frame
	void Update () {

        if (!isJumping) {

            if (Input.GetKeyDown(KeyCode.W)) {

                StartCoroutine(MoveVectical(1.0f));
                m_Animator.Play("JumpForward_" + Random.Range(1, 3));            

            } else if (Input.GetKeyDown(KeyCode.S)) {

                StartCoroutine(MoveVectical(-1.0f));
                m_Animator.Play("JumpBackward_" + Random.Range(1, 3));

            }

        }

	}

    private void FixedUpdate() {

        float hInput = Input.GetAxisRaw("Horizontal");

        if (!isJumping) {
            transform.Translate(transform.right * hInput * m_MoveSpeed * Time.deltaTime);
        }

        m_Animator.SetFloat("speed", Mathf.Abs(hInput));

    }

    private IEnumerator MoveVectical(float direction) {

        isJumping = true;

        float destionationPos = transform.position.z + (PLAYER_TRANSITION * direction);       // Destionation coordinate in Z-axis aka next platform

        while (Mathf.Abs(transform.position.z - destionationPos) > 0.1f) {

            transform.Translate(Vector3.forward * direction * m_JumpSpeed * Time.deltaTime);  // Moving player forward

            yield return null;

        }
        transform.position = new Vector3(transform.position.x, transform.position.y, destionationPos);  // Smoothing translate
        isJumping = false;

    }

    private void ReturnToLevelStart() {

        transform.position = m_StartLevelPosition;
        StopAllCoroutines();
        isJumping = false;

    }

    private void ReturnToGameStart() {

        transform.position = m_StartGamePosition;
        StopAllCoroutines();
        isJumping = false;

    }

    private void OnTriggerEnter(Collider other) {

        if (other.CompareTag("Enemy")) {

            ReturnToLevelStart();

        }

    }

    private void OnTriggerExit(Collider other) {

        if (other.CompareTag("Edge") && !isLevelEnd) {

            ReturnToLevelStart();

        }

    }

    private void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.CompareTag("LevelEnd")) {

            isLevelEnd = true;

        } else if (collision.gameObject.CompareTag("LevelStart")) {

            isLevelEnd = false;
            m_StartLevelPosition = new Vector3(0.0f, 1.0f, collision.gameObject.transform.position.z);

        } else if (collision.gameObject.CompareTag("EndGame")) {

            ReturnToGameStart();

        }

    }

}
