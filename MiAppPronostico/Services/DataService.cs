using ExcelDataReader;
using MiAppPronostico.Models;
using System.Data;
using System.Globalization;

namespace MiAppPronostico.Services;

public class DataService
{
    public DataService()
    {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
    }

    private IExcelDataReader GetReader(Stream fileStream, string fileName)
    {
        if (fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return ExcelReaderFactory.CreateCsvReader(fileStream);
        }
        
        return ExcelReaderFactory.CreateReader(fileStream);
    }

    // Sanitización financiera avanzada
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

    public List<string> GetHeaders(Stream fileStream, string fileName)
    {
        var headers = new List<string>();

        using (var reader = GetReader(fileStream, fileName))
        {
            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var headerName = reader.GetValue(i)?.ToString();
                    if (!string.IsNullOrWhiteSpace(headerName))
                    {
                        headers.Add(headerName.Trim());
                    }
                }
            }
        }
        return headers;
    }

    // ParseData con soporte para "Secuencial" (Sin Fechas)
    public (List<DataPoint> Datos, int FilasIgnoradas, int TotalFilas) ParseData(Stream fileStream, string fileName, string dateColumnName, string valueColumnName, string explicitDateFormat = "Automático", string frecuencia = "Diaria")
    {
        var rawDataPoints = new List<DataPoint>();
        int filasIgnoradas = 0;
        int totalFilas = 0;
        int contadorPasoLogico = 1; // Contador para la inyección de fechas falsas

        using (var reader = GetReader(fileStream, fileName))
        {
            int dateColIdx = -1;
            int valueColIdx = -1;

            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var header = reader.GetValue(i)?.ToString()?.Trim();
                    
                    // Si es secuencial, permitimos que el nombre de la columna de fecha esté vacío o no exista
                    if (!string.IsNullOrEmpty(dateColumnName) && header == dateColumnName) dateColIdx = i;
                    if (!string.IsNullOrEmpty(valueColumnName) && header == valueColumnName) valueColIdx = i;
                }
            }

            if (valueColIdx == -1)
            {
                throw new Exception("La columna de valores seleccionada no existe en el archivo.");
            }

            // Validación estricta solo si no estamos en modo Secuencial
            if (frecuencia != "Secuencial" && dateColIdx == -1)
            {
                throw new Exception("La columna de fechas seleccionada no existe en el archivo.");
            }

            while (reader.Read())
            {
                totalFilas++;
                try
                {
                    var cellValue = reader.GetValue(valueColIdx);

                    if (cellValue != null)
                    {
                        var valorLimpio = ParsearNumeroRobusto(cellValue.ToString());
                        if (valorLimpio == null) { filasIgnoradas++; continue; }
                        
                        double valorFinal = valorLimpio.Value;
                        DateTime fechaFinal = DateTime.MinValue;
                        bool fechaEsValida = false;

                        // INYECCIÓN DE SECUENCIA LÓGICA (DUMMY INDEXING)
                        if (frecuencia == "Secuencial")
                        {
                            // Inyectamos una fecha base (ej. año 2000) sumando el paso lógico.
                            // Esto asegura que la distancia entre puntos sea exactamente "1 unidad".
                            fechaFinal = new DateTime(2000, 1, 1).AddDays(contadorPasoLogico);
                            fechaEsValida = true;
                            contadorPasoLogico++;
                        }
                        else
                        {
                            // Lógica normal de extracción de fechas
                            var cellDate = reader.GetValue(dateColIdx);
                            if (cellDate != null)
                            {
                                if (cellDate is DateTime dt)
                                {
                                    fechaFinal = dt; fechaEsValida = true;
                                }
                                else
                                {
                                    string dateString = cellDate.ToString()!.Replace("\"", "").Trim();
                                    if (explicitDateFormat != "Automático")
                                    {
                                        string formatCode = explicitDateFormat.Split(' ')[0];
                                        fechaEsValida = DateTime.TryParseExact(dateString, formatCode, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFinal);
                                    }
                                    else
                                    {
                                        string[] formatosComunes = { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "M/d/yyyy", "d/M/yyyy", "yyyy/MM/dd", "dd.MM.yyyy", "MM.dd.yyyy" };
                                        fechaEsValida = DateTime.TryParseExact(dateString, formatosComunes, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFinal) || DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out fechaFinal);
                                    }
                                }
                            }
                        }

                        if (fechaEsValida && fechaFinal.Year >= 1900)
                        {
                            rawDataPoints.Add(new DataPoint { Fecha = fechaFinal.Date, Valor = valorFinal });
                        }
                        else { filasIgnoradas++; }
                    }
                    else { filasIgnoradas++; }
                }
                catch { filasIgnoradas++; continue; }
            }
        }
        
        var datosAgrupados = rawDataPoints
            .GroupBy(d => {
                if (frecuencia == "Secuencial") return d.Fecha; // No agrupa, respeta cada fila individual
                if (frecuencia == "Mensual") return new DateTime(d.Fecha.Year, d.Fecha.Month, 1);
                if (frecuencia == "Anual") return new DateTime(d.Fecha.Year, 1, 1);
                return d.Fecha.Date; // Por defecto es Diaria
            })
            .Select(grupo => new DataPoint
            {
                Fecha = grupo.Key,
                Valor = grupo.Sum(x => x.Valor) 
            })
            .OrderBy(d => d.Fecha)
            .ToList();

        return (datosAgrupados, filasIgnoradas, totalFilas);
    }
}