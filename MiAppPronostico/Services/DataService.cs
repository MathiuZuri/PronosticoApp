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

    // MEJORA 3: Sanitización financiera avanzada
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
        // Eliminamos $, S/ (Soles), €, etc.
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

    // MEJORA 1: Lectura Row-by-Row sin AsDataSet()
    public (List<DataPoint> Datos, int FilasIgnoradas, int TotalFilas) ParseData(Stream fileStream, string fileName, string dateColumnName, string valueColumnName, string explicitDateFormat = "Automático")
    {
        var rawDataPoints = new List<DataPoint>();
        int filasIgnoradas = 0;
        int totalFilas = 0;

        using (var reader = GetReader(fileStream, fileName))
        {
            // 1. Ubicar los índices de las columnas en la primera fila (Cabeceras)
            int dateColIdx = -1;
            int valueColIdx = -1;

            if (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var header = reader.GetValue(i)?.ToString()?.Trim();
                    if (header == dateColumnName) dateColIdx = i;
                    if (header == valueColumnName) valueColIdx = i;
                }
            }

            if (dateColIdx == -1 || valueColIdx == -1)
            {
                throw new Exception("Las columnas seleccionadas no existen en el archivo.");
            }

            // 2. Leer fila por fila directamente del Stream (Ultra ligero en RAM)
            while (reader.Read())
            {
                totalFilas++;
                
                try
                {
                    var cellDate = reader.GetValue(dateColIdx);
                    var cellValue = reader.GetValue(valueColIdx);

                    if (cellDate != null && cellValue != null)
                    {
                        var valorLimpio = ParsearNumeroRobusto(cellValue.ToString());
                        if (valorLimpio == null)
                        {
                            filasIgnoradas++;
                            continue; 
                        }
                        
                        double valorFinal = valorLimpio.Value;
                        DateTime fechaFinal = DateTime.MinValue;
                        bool fechaEsValida = false;

                        if (cellDate is DateTime dt)
                        {
                            fechaFinal = dt; 
                            fechaEsValida = true;
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
                                string[] formatosComunes = { 
                                    "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", 
                                    "M/d/yyyy", "d/M/yyyy", "yyyy/MM/dd",
                                    "dd.MM.yyyy", "MM.dd.yyyy" 
                                };
                                
                                fechaEsValida = DateTime.TryParseExact(dateString, formatosComunes, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFinal) || 
                                                DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out fechaFinal);
                            }
                        }

                        if (fechaEsValida && fechaFinal.Year >= 1900)
                        {
                            // Guardamos solo la parte de la fecha (ignoramos horas/minutos para agrupar mejor)
                            rawDataPoints.Add(new DataPoint
                            {
                                Fecha = fechaFinal.Date,
                                Valor = valorFinal
                            });
                        }
                        else
                        {
                            filasIgnoradas++;
                        }
                    }
                    else
                    {
                        filasIgnoradas++;
                    }
                }
                catch 
                { 
                    filasIgnoradas++;
                    continue; 
                }
            }
        }
        
        // MEJORA 2: Agrupación y promedio de fechas duplicadas
        var datosAgrupados = rawDataPoints
            .GroupBy(d => d.Fecha)
            .Select(grupo => new DataPoint
            {
                Fecha = grupo.Key,
                Valor = grupo.Average(x => x.Valor) // Si hay 2 ventas el mismo día, saca el promedio
            })
            .OrderBy(d => d.Fecha)
            .ToList();

        // Calculamos cuántos registros se fusionaron por ser del mismo día
        int filasFusionadas = rawDataPoints.Count - datosAgrupados.Count;
        
        // Puedes sumar las fusionadas a las ignoradas si quieres reflejarlo en la UI, 
        // o simplemente devolver la lista procesada.
        
        return (datosAgrupados, filasIgnoradas, totalFilas);
    }
}