using MiAppPronostico.Models;
using MathNet.Numerics.LinearAlgebra;
using System.Collections.Generic;
using System.Linq;
using System;
using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Multivariable;

public class MultivariateEngine
{
    public RegressionResult CalcularRegresionMultiple(List<MultivariateDataRow> datos, string variableDependiente, List<string> variablesIndependientes)
    {
        // Filtramos solo las filas que tengan todas las variables numéricas necesarias
        var datosValidos = datos.Where(d => 
            d.ValoresNumericos.ContainsKey(variableDependiente) && 
            variablesIndependientes.All(v => d.ValoresNumericos.ContainsKey(v))
        ).ToList();

        if (datosValidos.Count <= variablesIndependientes.Count)
            throw new Exception("No hay suficientes datos para realizar la regresión múltiple.");

        int rows = datosValidos.Count;
        int cols = variablesIndependientes.Count;

        // Extraemos Y (Vector dependiente)
        double[] yArray = datosValidos.Select(d => d.ValoresNumericos[variableDependiente]).ToArray();

        // Construimos la Matriz de Diseño X incorporando una columna de 1s al inicio para el Intercepto (B0)
        double[,] xData = new double[rows, cols + 1];
        for (int i = 0; i < rows; i++)
        {
            xData[i, 0] = 1.0; // Columna del Intercepto
            for (int j = 0; j < cols; j++)
            {
                xData[i, j + 1] = datosValidos[i].ValoresNumericos[variablesIndependientes[j]];
            }
        }

        // Inicializamos los constructores de MathNet
        var matrixM = Matrix<double>.Build;
        var vectorV = Vector<double>.Build;

        var matrixX = matrixM.DenseOfArray(xData);
        var vectorY = vectorV.Dense(yArray);

        // MAGIA MATEMÁTICA: La Pseudo-inversa filtra decimales ruidosos inferiores a la tolerancia de la máquina
        var coeficientesVector = matrixX.PseudoInverse() * vectorY;

        var resultado = new RegressionResult 
        { 
            Intercepto = coeficientesVector[0] // B0 es el primer elemento
        };

        // Asignamos los coeficientes B1, B2, B3... a sus respectivas variables
        for (int j = 0; j < cols; j++)
        {
            resultado.Coeficientes[variablesIndependientes[j]] = coeficientesVector[j + 1];
        }

        // Calculamos las predicciones del modelo de forma segura
        List<double> prediccionesList = new List<double>();
        for (int i = 0; i < rows; i++)
        {
            double pred = resultado.Intercepto;
            for (int j = 0; j < cols; j++)
            {
                pred += resultado.Coeficientes[variablesIndependientes[j]] * datosValidos[i].ValoresNumericos[variablesIndependientes[j]];
            }
            prediccionesList.Add(pred);
        }
        resultado.Predicciones = prediccionesList;

        // Calculamos R Cuadrado (R²) de manera manual y controlada anti-NaN
        double promedioY = yArray.Average();
        double ssTot = yArray.Sum(val => Math.Pow(val - promedioY, 2));
        double ssRes = 0;
        for (int i = 0; i < rows; i++)
        {
            ssRes += Math.Pow(yArray[i] - resultado.Predicciones[i], 2);
        }

        resultado.RSquared = ssTot > 0 ? 1.0 - (ssRes / ssTot) : 1.0;

        return resultado;
    }

    // 2. MATRIZ DE CONFUSIÓN (Se mantiene impecable como estaba)
    public ConfusionMatrixResult CalcularMatrizConfusion(List<MultivariateDataRow> datos, string columnaReal, string columnaPredicha, string valorPositivo)
    {
        var resultado = new ConfusionMatrixResult { ClasePositiva = valorPositivo };

        foreach (var fila in datos)
        {
            if (fila.ValoresCategoricos.ContainsKey(columnaReal) && fila.ValoresCategoricos.ContainsKey(columnaPredicha))
            {
                string real = fila.ValoresCategoricos[columnaReal].Trim();
                string predicho = fila.ValoresCategoricos[columnaPredicha].Trim();

                bool esPositivoReal = real.Equals(valorPositivo, StringComparison.OrdinalIgnoreCase);
                bool esPositivoPredicho = predicho.Equals(valorPositivo, StringComparison.OrdinalIgnoreCase);

                if (esPositivoReal && esPositivoPredicho) resultado.VerdaderosPositivos++;
                else if (!esPositivoReal && !esPositivoPredicho) resultado.VerdaderosNegativos++;
                else if (!esPositivoReal && esPositivoPredicho) resultado.FalsosPositivos++;
                else if (esPositivoReal && !esPositivoPredicho) resultado.FalsosNegativos++;
            }
        }

        return resultado;
    }
}