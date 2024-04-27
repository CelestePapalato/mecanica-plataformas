using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraObjective : MonoBehaviour
{
    [Header("Seguimiento al jugador")]
    [SerializeField]
    [Range(0f, 0.3f)]float suavizadoMovimiento;
    [Header("C�mara")]
    [SerializeField]
    float sensibilidadX;
    [SerializeField]
    float sensibilidadY;
    [SerializeField]
    [Range(1, 10)] float sentivityProportion;
    [SerializeField]
    [Range(0f, 0.3f)] float smoothing;
    [SerializeField]
    float lowerLookLimit;
    [SerializeField]
    float upperLookLimit;

    Vector2 input_vector = Vector2.zero;
    float rotacionEjeX;
    Vector2 suavidadV;

    GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rotacionEjeX = transform.localEulerAngles.x;
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

        //movimiento = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        suavidadV.x = Mathf.SmoothStep(suavidadV.x, input_vector.x * sensibilidadX * 1 / sentivityProportion, smoothing);
        suavidadV.y = Mathf.SmoothStep(suavidadV.y, input_vector.y * sensibilidadY * 1 / sentivityProportion, smoothing);


        // Rotamos verticalmente la c�mara
        // 1 - Obtenemos la rotaci�n objetivo de la c�mara
        rotacionEjeX += suavidadV.y;

        // 2- Limitamos la rotaci�n total a 90 grados
        rotacionEjeX = Mathf.Clamp(rotacionEjeX, lowerLookLimit, upperLookLimit);

        // 3- Rotamos en valores locales
        transform.localRotation = Quaternion.Euler(-rotacionEjeX, suavidadV.x + transform.localEulerAngles.y, 0);
    }
    void OnRotate(InputValue inputValue)
    {
        input_vector = inputValue.Get<Vector2>();
    }
}
