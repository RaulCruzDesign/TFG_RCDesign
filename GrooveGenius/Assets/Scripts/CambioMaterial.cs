using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CambioMaterial : MonoBehaviour
{
    public Material primerMaterial;   // Primer material a aplicar
    public Material segundoMaterial;  // Segundo material a aplicar
    public float duracionCambio = 2.0f;  // Duración en segundos para cada cambio de material

    private Renderer rend;  // Referencia al renderer del objeto

    private void Start()
    {
        rend = GetComponent<Renderer>();  // Obtener el renderer del objeto al que está adjunto este script
    }

    // Estas funciones serán asignadas a eventos del nuevo sistema de inputs
    public void CambiarAlPrimerMaterial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(CambiarMaterialTemporariamente(primerMaterial));
        }
    }

    public void CambiarAlSegundoMaterial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(CambiarMaterialTemporariamente(segundoMaterial));
        }
    }

    private IEnumerator CambiarMaterialTemporariamente(Material nuevoMaterial)
    {
        // Guardar el material original
        Material materialOriginal = rend.material;

        // Aplicar el nuevo material
        rend.material = nuevoMaterial;

        // Esperar la duración del cambio
        yield return new WaitForSeconds(duracionCambio);

        // Restaurar el material original
        rend.material = materialOriginal;
    }
}
