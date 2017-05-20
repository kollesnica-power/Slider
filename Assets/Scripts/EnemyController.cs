using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    [SerializeField] private Transform m_StartPoint;
    [SerializeField] private Transform m_EndPoint;
    [SerializeField] private float m_Speed = 2.0f;

    private float m_FadeDistanse = 2.0f;
    private float m_FadeSpeed;
    private Renderer m_Renderer;
    private int m_FadeDirection = 1;

    // Use this for initialization
    void Start () {

        m_Renderer = GetComponentInChildren<Renderer>();

        m_FadeSpeed = 1 / (m_FadeDistanse / m_Speed);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (Vector3.Distance(transform.position, m_EndPoint.position) >= 0.2f) {

            transform.Translate(Vector3.right * m_Speed * Time.fixedDeltaTime);

        } else {

            transform.position = m_StartPoint.position;

        }

        if (Vector3.Distance(transform.position, m_EndPoint.position) <= m_FadeDistanse) {
            m_FadeDirection = -1;
        } else {
            m_FadeDirection = 1;
        }

        Fade();

    }

    private void Fade() {

        float alpha = m_Renderer.material.color.a;
        alpha += m_FadeDirection * m_FadeSpeed * Time.deltaTime;

        alpha = Mathf.Clamp01(alpha);

        m_Renderer.material.color = new Color(m_Renderer.material.color.r, m_Renderer.material.color.g, m_Renderer.material.color.b, alpha);

    }

}
