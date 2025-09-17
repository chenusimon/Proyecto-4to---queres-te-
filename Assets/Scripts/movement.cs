using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] float sensibilidad = 300f;
    float verticalRotation;
    float verticalClampAngle = 90f;
    [SerializeField] GameObject playerCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DireccionCamaraHorizontal();
        DireccionCamaraVertical();
    }

    void DireccionCamaraHorizontal()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibilidad * Time. deltaTime;
        transform.Rotate(0f, mouseX, 0f);
    }

    void DireccionCamaraVertical()
    {
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidad * Time.deltaTime;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClampAngle, verticalClampAngle);

        playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
