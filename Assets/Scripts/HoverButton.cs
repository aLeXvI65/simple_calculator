using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverButton : MonoBehaviour {
    public Button targetButton; // Asigna el botón desde el Inspector
    //public UnityEvent onHighlightEnd; // Evento a llamar cuando termine el Highlight

    private float elappsedTime = 0;
    private float endTime = 1;
    private bool isEnter = false;

    private bool isHighlighted = false; // Indica si el botón está en estado Highlight

    private void Start() {
        targetButton = GetComponent<Button>();
        endTime = targetButton.colors.fadeDuration;

        // Registrar eventos al interactuar con el botón
        EventTrigger trigger = targetButton.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) {
            trigger = targetButton.gameObject.AddComponent<EventTrigger>();
        }

        // Evento para entrar en el Highlight
        EventTrigger.Entry onPointerEnter = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerEnter
        };
        onPointerEnter.callback.AddListener((data) => StartHighlight());
        trigger.triggers.Add(onPointerEnter);

        // Evento para salir del Highlight
        EventTrigger.Entry onPointerExit = new EventTrigger.Entry {
            eventID = EventTriggerType.PointerExit
        };
        onPointerExit.callback.AddListener((data) => EndHighlight());
        trigger.triggers.Add(onPointerExit);
    }

    private void Update() {
        if (isEnter) {
            elappsedTime += Time.deltaTime;
        }
        // Verificar si el botón está siendo resaltado
        if (targetButton != null && targetButton.targetGraphic != null) {
            //var colors = targetButton.colors;
            //var currentColor = targetButton.targetGraphic.color;
            //Debug.Log(currentColor + " , "+colors.highlightedColor);
            //if (currentColor == colors.highlightedColor && !isHighlighted) {
            //    isHighlighted = true;
            //    StartCoroutine(HandleHighlightEnd(colors.fadeDuration));
            //}
            if (elappsedTime >= endTime) {
                elappsedTime = 0;
                isHighlighted = true;
                StartCoroutine(HandleHighlightEnd(endTime));
            }
        }
    }

    private void StartHighlight() {
        Debug.Log("Enter");
        isEnter = true;
        elappsedTime = 0;
    }

    private void EndHighlight() {
        Debug.Log("Exit");
        isEnter = false;
        elappsedTime = 0;
    }

    private IEnumerator HandleHighlightEnd(float duration) {
        Debug.Log("Higlight");
        targetButton.onClick?.Invoke();

        yield return new WaitForSeconds(duration); // Esperar el tiempo de Highlight
        isHighlighted = false; // Resetear el estado
        //onHighlightEnd?.Invoke(); // Llamar al evento
    }
}
