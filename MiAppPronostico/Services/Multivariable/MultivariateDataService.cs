using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using ExcelDataReader;
using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Multivariable;

public class MultivariateDataService
{
    public MultivariateDataService()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }

    // SANITIZACIÓN FINANCIERA AVANZADA
    private double? ParsearNumeroRobusto(string? valorCelda)
    {
        if (string.IsNullOrWhiteSpace(valorCelda)) return null;

        string limpio = valorCelda.Trim();

        if (limpio.StartsWith("(") && limpio.EndsWith(")"))
        {
            limpio = "-" + limpio.Substring(1, limpio.Length - 2);
        }

        limpio = limpio.Replace("\"", "").Replace("%", "").Replace("$", "").Replace("S/", "").Replace("€", "").Trim();

        double multiplicador = 1;
        if (limpio.EndsWith("M", StringComparison.OrdinalIgnoreCase))
        {
            multiplicador = 1000000;
            limpio = limpio.Substring(0, limpio.Length - 1);
        }
        else if (limpio.EndsWith("K", StringComparison.OrdinalIgnoreCase))
        {
            multiplicador = 1000;
            limpio = limpio.Substring(0, limpio.Length - 1);
        }

        limpio = limpio.Replace(",", ".");

        if (double.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultado))
        {
            return resultado * multiplicador;
        }
        return null; 
    }

    // LECTOR DINÁMICO
    public List<MultivariateDataRow> ParsearDatos(Stream fileStream, string fileName)
    {
        var dataset = new List<MultivariateDataRow>();
        int indice = 0;

        using (var reader = fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) 
            ? ExcelReaderFactory.CreateCsvReader(fileStream) 
            : ExcelReaderFactory.CreateReader(fileStream))
        {
            if (!reader.Read()) return dataset;

            var cabeceras = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++) cabeceras.Add(reader.GetValue(i)?.ToString()?.Trim() ?? $"Columna_{i}");

            while (reader.Read())
            {
                var fila = new MultivariateDataRow { IndiceFila = indice++ };

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var valorCrudo = reader.GetValue(i)?.ToString()?.Trim();
                    string cabecera = cabeceras[i];

                    if (string.IsNullOrWhiteSpace(valorCrudo)) continue;

                    double? valorLimpio = ParsearNumeroRobusto(valorCrudo);
                    if (valorLimpio.HasValue) fila.ValoresNumericos[cabecera] = valorLimpio.Value;
                    else fila.ValoresCategoricos[cabecera] = valorCrudo;
                }
                dataset.Add(fila);
            }
        }
        return dataset;
    }

    // --- NUEVO PIPELINE INTELIGENTE: STURGES + CUANTILES + SIMETRÍA ---
    public int PreprocesarCruceAutomatico(List<MultivariateDataRow> datos, string colReal, string colPredicha)
    {
        // 1. Extraer Ground Truth (Realidad) para basar los percentiles
        var valoresReales = datos.Where(d => d.ValoresNumericos.ContainsKey(colReal))
                                 .Select(d => d.ValoresNumericos[colReal])
                                 .OrderBy(v => v).ToList();

        if (!valoresReales.Any()) throw new Exception("La columna de Realidad no tiene datos numéricos válidos para procesar.");

        // 2. REGLA DE STURGES: Cálculo automatizado de grupos
        int cantidadGrupos = (int)Math.Ceiling(1 + Math.Log2(valoresReales.Count));
        
        // Control de seguridad por si hay pocos datos variables
        int valoresUnicos = valoresReales.Distinct().Count();
        cantidadGrupos = Math.Min(cantidadGrupos, Math.Max(2, valoresUnicos)); // Garantiza mínimo 2 grupos

        // 3. CUANTILES (Equi-frecuencia): Calcula los límites de corte exactos
        List<double> umbrales = new List<double>();
        for (int i = 1; i < cantidadGrupos; i++)
        {
            double percentil = (double)i / cantidadGrupos;
            int indice = (int)Math.Round(percentil * (valoresReales.Count - 1));
            umbrales.Add(valoresReales[indice]);
        }
        umbrales.Add(double.MaxValue); // Umbral infinito para el grupo más alto

        // 4. APLICACIÓN SIMÉTRICA (Evita comparar peras con manzanas)
        string colRealGenerada = colReal + "_AutoCat";
        string colPredichaGenerada = colPredicha + "_AutoCat";

        AplicarUmbrales(datos, colReal, colRealGenerada, umbrales);
        AplicarUmbrales(datos, colPredicha, colPredichaGenerada, umbrales);

        return cantidadGrupos;
    }

    private void AplicarUmbrales(List<MultivariateDataRow> datos, string colOrigen, string colDestino, List<double> umbrales)
    {
        foreach (var fila in datos)
        {
            if (fila.ValoresNumericos.TryGetValue(colOrigen, out double valor))
            {
                for (int i = 0; i < umbrales.Count; i++)
                {
                    if (valor <= umbrales[i]) 
                    {
                        // Se nombran simplemente "Grupo 1", "Grupo 2", etc.
                        fila.ValoresCategoricos[colDestino] = $"Grupo {i + 1}";
                        break;
                    }
                }
            }
        }
    }
}