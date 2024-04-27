using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraObjective : MonoBehaviour
{
    [Header("Seguimiento al jugador")]
    [SerializeField]
    [Range(0f, 0.3f)]float suavizadoMovimiento;
    [Header("Cámara")]
    [SerializeField]
    float sensibilidadX;
    [SerializeField]
    float sensibilidadY;
    [SerializeField]
    [Range(1, 10)] float sentivityProportion;
    [SerializeField]
    [Range(0f, 0.3f)] float smoothing;
    [Header("Angles")]
    [SerializeField]
    [Range(-90, 90)] float upperLookLimit;
    [SerializeField]
    [Range(-90, 90)] float lowerLookLimit;

    Vector2 input_vector = Vector2.zero;
    float rotacionEjeX;
    Vector2 suavidadV;

    public static float RotationOnYAxis { get; private set; }
    public static float RotationOnXAxis { get; private set; }

    GameObject player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Start()
    {
        rotacionEjeX = transform.localEulerAngles.x;
        RotationOnYAxis = transform.eulerAngles.y;
        RotationOnXAxis = rotacionEjeX;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, (1f / suavizadoMovimiento) * Time.deltaTime);
        rotarCamara();
    }

    void rotarCamara()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        suavidadV.x = Mathf.SmoothStep(suavidadV.x, input_vector.x * sensibilidadX * 1 / sentivityProportion, smoothing);
        suavidadV.y = Mathf.SmoothStep(suavidadV.y, input_vector.y * sensibilidadY * 1 / sentivityProportion, smoothing);


        // Rotamos verticalmente la cámara
        // 1 - Obtenemos la rotación objetivo de la cámara
        rotacionEjeX += suavidadV.y;

        // 2- Limitamos la rotación total
        rotacionEjeX = Mathf.Clamp(rotacionEjeX, lowerLookLimit, upperLookLimit);

        // 3- Rotamos en valores locales
        transform.localRotation = Quaternion.Euler(-rotacionEjeX, suavidadV.x + transform.localEulerAngles.y, 0);
        RotationOnYAxis = transform.eulerAngles.y;
        RotationOnXAxis = -rotacionEjeX;
    }
    void OnRotate(InputValue inputValue)
    {
        input_vector = inputValue.Get<Vector2>();
    }
}
