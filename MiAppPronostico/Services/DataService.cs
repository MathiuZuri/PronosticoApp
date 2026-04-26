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

    // Método auxiliar privado para elegir el lector correcto
    private IExcelDataReader GetReader(Stream fileStream, string fileName)
    {
        if (fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return ExcelReaderFactory.CreateCsvReader(fileStream);
        }
        
        return ExcelReaderFactory.CreateReader(fileStream);
    }

    // NUEVO: Método sanitizador para limpiar números sucios (M, %, comillas, comas)
    private double? ParsearNumeroRobusto(string? valorCelda)
    {
        if (string.IsNullOrWhiteSpace(valorCelda)) return null;

        // Limpieza básica: quitar comillas y porcentajes
        string limpio = valorCelda.Replace("\"", "").Replace("%", "").Trim();

        // Manejo de multiplicadores financieros (M = Millones, K = Miles)
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

        // Unificar todo a punto decimal
        limpio = limpio.Replace(",", ".");

        // Parseo seguro
        if (double.TryParse(limpio, NumberStyles.Any, CultureInfo.InvariantCulture, out double resultado))
        {
            return resultado * multiplicador;
        }

        return null; // Si no se pudo salvar el dato, devolvemos null
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

public (List<DataPoint> Datos, int FilasIgnoradas, int TotalFilas) ParseData(Stream fileStream, string fileName, string dateColumnName, string valueColumnName, string explicitDateFormat = "Automático")
    {
        var dataPoints = new List<DataPoint>();
        int filasIgnoradas = 0;
        int totalFilas = 0;

        using (var reader = GetReader(fileStream, fileName))
        {
            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (_) => new ExcelDataTableConfiguration() { UseHeaderRow = true }
            });

            var dataTable = result.Tables[0];

            if (!dataTable.Columns.Contains(dateColumnName) || !dataTable.Columns.Contains(valueColumnName))
            {
                throw new Exception("Las columnas seleccionadas no existen en el archivo.");
            }

            foreach (DataRow row in dataTable.Rows)
            {
                totalFilas++; // Contamos cada fila que intentamos leer
                
                try
                {
                    var cellDate = row[dateColumnName];
                    var cellValue = row[valueColumnName];

                    if (cellDate != DBNull.Value && cellValue != DBNull.Value)
                    {
                        var valorLimpio = ParsearNumeroRobusto(cellValue.ToString());
                        if (valorLimpio == null)
                        {
                            filasIgnoradas++; // Sumamos si el número era basura
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
                            dataPoints.Add(new DataPoint
                            {
                                Fecha = fechaFinal,
                                Valor = valorFinal
                            });
                        }
                        else
                        {
                            filasIgnoradas++; // Sumamos si la fecha era inválida
                        }
                    }
                    else
                    {
                        filasIgnoradas++; // Sumamos si alguna celda estaba vacía
                    }
                }
                catch 
                { 
                    filasIgnoradas++; // Sumamos si hubo una explosión inesperada
                    continue; 
                }
            }
        }
        
        return (dataPoints.OrderBy(d => d.Fecha).ToList(), filasIgnoradas, totalFilas);
    }
}