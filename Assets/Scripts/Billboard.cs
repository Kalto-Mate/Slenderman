using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject Follow;

    private void LateUpdate()
    {
        // Usamos la posición "y" del propio objeto para el evctor "target" para así evitar que el billboard se incline en ese eje cuando nos acercamos
        Vector3 target = new Vector3(Follow.transform.position.x, transform.position.y, Follow.transform.position.z);
        transform.LookAt(target);
    }
}
