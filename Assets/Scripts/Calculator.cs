using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Calculator : MonoBehaviour {

    public TMP_InputField inputField;

    void Start() {
        
    }

    void Update() {
        
    }

    public void clickValue(string value) {
        
        if(value == "=") {
            float res = (float)evaluateExpression(inputField.text);

            inputField.text = res.ToString("0.00");

            return;
        }
        if (value == "C") {
            inputField.text = "";
            return;
        }
        inputField.text += value;

    }

    public double evaluateExpression(string expression) {
        // Reemplazar 'x' por '*'
        expression = expression.Replace("x", "*");

        // Usar DataTable para evaluar la expresión
        try {
            DataTable table = new DataTable();
            table.CaseSensitive = false;
            var result = table.Compute(expression, string.Empty);
            return Convert.ToDouble(result);
        }
        catch (Exception ex) {
            throw new ArgumentException("La expresión no es válida.", ex);
        }
    }
}
