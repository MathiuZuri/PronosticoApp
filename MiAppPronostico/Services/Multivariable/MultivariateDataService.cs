
using System.Globalization;

using ExcelDataReader;
using MiAppPronostico.Models;


namespace MiAppPronostico.Services.Multivariable; // Usamos el namespace consistente

public class MultivariateDataService
{
    public MultivariateDataService()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }

    // --- MEJORA: SANITIZACIÓN FINANCIERA AVANZADA ---
    private double? ParsearNumeroRobusto(string? valorCelda)
    {
        if (string.IsNullOrWhiteSpace(valorCelda)) return null;

        string limpio = valorCelda.Trim();

        // Manejo de negativos contables en paréntesis: "(150.50)" -> "-150.50"
        if (limpio.StartsWith("(") && limpio.EndsWith(")"))
        {
            limpio = "-" + limpio.Substring(1, limpio.Length - 2);
        }

        // Limpieza de símbolos de moneda, comillas, porcentajes y espacios
        limpio = limpio.Replace("\"", "")
                       .Replace("%", "")
                       .Replace("$", "")
                       .Replace("S/", "")
                       .Replace("€", "")
                       .Trim();

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

    // Lector dinámico principal
    public List<MultivariateDataRow> ParsearDatos(Stream fileStream, string fileName)
    {
        var dataset = new List<MultivariateDataRow>();
        int indice = 0;

        using (var reader = fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase) 
            ? ExcelReaderFactory.CreateCsvReader(fileStream) 
            : ExcelReaderFactory.CreateReader(fileStream))
        {
            if (!reader.Read()) return dataset;

            // Extraer cabeceras
            var cabeceras = new List<string>();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                cabeceras.Add(reader.GetValue(i)?.ToString()?.Trim() ?? $"Columna_{i}");
            }

            // Leer filas
            while (reader.Read())
            {
                var fila = new MultivariateDataRow { IndiceFila = indice++ };

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var valorCrudo = reader.GetValue(i)?.ToString()?.Trim();
                    string cabecera = cabeceras[i];

                    if (string.IsNullOrWhiteSpace(valorCrudo)) continue;

                    // APLICAMOS LA SANITIZACIÓN ROBUSTA AQUÍ
                    double? valorLimpio = ParsearNumeroRobusto(valorCrudo);
                    
                    if (valorLimpio.HasValue)
                    {
                        // Si la función logró extraer un número real (limpiando K, M, $, etc.)
                        fila.ValoresNumericos[cabecera] = valorLimpio.Value;
                    }
                    else
                    {
                        // Si retornó null, significa que es puro texto categórico o fecha.
                        // Usamos el valor original crudo para no perder la palabra (ej: "Sube").
                        fila.ValoresCategoricos[cabecera] = valorCrudo;
                    }
                }
                dataset.Add(fila);
            }
        }
        return dataset;
    }

    // Transformación: Convertir variable Cuantitativa a Cualitativa (Binning)
    public void TransformarNumericoACategorico(List<MultivariateDataRow> datos, string columnaNumerica, string nuevaColumnaCategorica, int cantidadGrupos)
    {
        var valores = datos.Where(d => d.ValoresNumericos.ContainsKey(columnaNumerica))
                           .Select(d => d.ValoresNumericos[columnaNumerica]).ToList();

        if (!valores.Any()) return;

        double min = valores.Min();
        double max = valores.Max();
        double tamañoRango = (max - min) / cantidadGrupos;

        foreach (var fila in datos)
        {
            if (fila.ValoresNumericos.TryGetValue(columnaNumerica, out double valor))
            {
                // Calculamos a qué grupo pertenece
                int grupo = (int)((valor - min) / tamañoRango);
                if (grupo == cantidadGrupos) grupo--; // Ajuste para el valor máximo exacto

                double limiteInferior = min + (grupo * tamañoRango);
                double limiteSuperior = limiteInferior + tamañoRango;

                fila.ValoresCategoricos[nuevaColumnaCategorica] = $"Grupo {grupo + 1} ({limiteInferior:0.0} a {limiteSuperior:0.0})";
            }
        }
    }
}