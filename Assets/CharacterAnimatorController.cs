using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    public GameObject PersonajePrincipal; // Referencia al personaje principal
    public List<GameObject> PersonajesSecundarios; // Lista de personajes secundarios

    private Animator personajePrincipalAnimator; // Animator del personaje principal
    private List<Animator> personajesSecundariosAnimators; // Lista de animadores secundarios

    // Lista para mapear teclas con triggers
    private List<KeyTriggerMapping> keyTriggerMappings = new List<KeyTriggerMapping>
    {
        new KeyTriggerMapping(KeyCode.LeftArrow, "FlechaIzqAplastada"),
        new KeyTriggerMapping(KeyCode.RightArrow, "FlechaDerAplastada")
    };

    void Start()
    {
        // Obtener el Animator del personaje principal
        personajePrincipalAnimator = PersonajePrincipal.GetComponent<Animator>();

        if (personajePrincipalAnimator == null)
        {
            Debug.LogError("No se encontró el componente Animator en el Personaje Principal.");
        }

        // Inicializar la lista de animadores de los personajes secundarios
        personajesSecundariosAnimators = new List<Animator>();
        foreach (GameObject personaje in PersonajesSecundarios)
        {
            Animator animator = personaje.GetComponent<Animator>();
            if (animator != null)
            {
                personajesSecundariosAnimators.Add(animator);
            }
        }
    }

    void Update()
    {
        // Detectar entradas y activar triggers en el personaje principal y secundarios
        foreach (var mapping in keyTriggerMappings)
        {
            if (Input.GetKeyDown(mapping.Key))
            {
                ReproducirAnimacion(mapping.Trigger);
            }
        }

        // Si se presiona la tecla de espacio, volver al estado Idle
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReproducirAnimacion("Idle");
        }
    }

    void ReproducirAnimacion(string triggerName)
    {
        // Activar la animación en el personaje principal
        personajePrincipalAnimator.SetTrigger(triggerName);

        // Activar la misma animación en todos los personajes secundarios
        foreach (Animator anim in personajesSecundariosAnimators)
        {
            anim.SetTrigger(triggerName);
        }
    }
}

// Clase auxiliar para vincular teclas con triggers
[System.Serializable]
public class KeyTriggerMapping
{
    public KeyCode Key;       // Tecla que activa el trigger
    public string Trigger;    // Nombre del trigger en el Animator

    public KeyTriggerMapping(KeyCode key, string trigger)
    {
        Key = key;
        Trigger = trigger;
    }
}
