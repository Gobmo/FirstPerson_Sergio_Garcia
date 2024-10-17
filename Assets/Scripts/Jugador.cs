using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    [SerializeField] float velocidadMov;
    CharacterController cC;
    Vector2 input = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        cC = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        input = new Vector2(h, v).normalized;

        if (input.magnitude != 0)
        {
            Movimiento();
        }
    }

    void Movimiento()
    {
        
        float angulorotacion = Mathf.Atan2(input.x, input.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
        transform.eulerAngles = new Vector3(0, angulorotacion, 0);
        Vector3 movimiento = Quaternion.Euler(0, angulorotacion, 0) * Vector3.forward;
        cC.Move(movimiento * velocidadMov * Time.deltaTime);
    }
}
