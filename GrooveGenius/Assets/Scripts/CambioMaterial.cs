using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class CambioMaterial : MonoBehaviour
{
    public Material primerMaterial;
    public Material segundoMaterial;
    public float duracionCambio = 2.0f;

    private Renderer rend;
    private Coroutine materialCoroutine;
    private Queue<Material> materialQueue = new Queue<Material>();

    private void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void CambiarAlPrimerMaterial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EnqueueMaterial(primerMaterial);
        }
    }

    public void CambiarAlSegundoMaterial(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            EnqueueMaterial(segundoMaterial);
        }
    }

    private void EnqueueMaterial(Material nuevoMaterial)
    {
        materialQueue.Enqueue(nuevoMaterial);

        if (materialCoroutine == null)
        {
            materialCoroutine = StartCoroutine(ProcessMaterialQueue());
        }
    }

    private IEnumerator ProcessMaterialQueue()
    {
        while (materialQueue.Count > 0)
        {
            Material nuevoMaterial = materialQueue.Dequeue();

            Material materialOriginal = rend.material;

            rend.material = nuevoMaterial;

            yield return new WaitForSeconds(duracionCambio);

            rend.material = materialOriginal;
        }

        materialCoroutine = null;
    }
}
