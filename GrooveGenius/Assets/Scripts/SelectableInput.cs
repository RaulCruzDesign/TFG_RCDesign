using UnityEngine;
using UnityEngine.InputSystem;

public class SelectableInput : MonoBehaviour
{
/*    public RailActivation[] railActivations; // Lista de scripts RailActivation asociados a cada carril
    public InputActionReference selectedInput; // Referencia al input seleccionado por el jugador

    // Método para manejar la entrada del jugador (por ejemplo, clic del mouse o toque de pantalla)
    public void OnSelect(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Obtener el índice del carril desde el nombre del objeto que disparó el evento
            int laneIndex = int.Parse(gameObject.name);

            // Llamar al método PerformInputAndCalculateScore del script RailActivation correspondiente al carril
            if (laneIndex >= 0 && laneIndex < railActivations.Length)
            {
                railActivations[laneIndex].PerformInputAndCalculateScore();
            }
            else
            {
                Debug.LogWarning("Carril no encontrado para el objeto: " + gameObject.name);
            }
        }
    }*/
}
