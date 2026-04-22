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
        // Si es CSV, usamos el lector especializado de texto
        if (fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
        {
            return ExcelReaderFactory.CreateCsvReader(fileStream);
        }
        
        // Si es xlsx o xls, usamos el lector estándar
        return ExcelReaderFactory.CreateReader(fileStream);
    }

    public List<string> GetHeaders(Stream fileStream, string fileName)
    {
        var headers = new List<string>();

        // Usamos nuestro nuevo método auxiliar
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

public List<DataPoint> ParseData(Stream fileStream, string fileName, string dateColumnName, string valueColumnName, string explicitDateFormat = "Automático")
    {
        var dataPoints = new List<DataPoint>();

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
                try
                {
                    var cellDate = row[dateColumnName];
                    var cellValue = row[valueColumnName];

                    if (cellDate != DBNull.Value && cellValue != DBNull.Value)
                    {
                        // 1. Lectura del número (Maneja perfectamente el 169.2799988)
                        string valString = cellValue.ToString()!.Trim().Replace(",", ".");
                        if (!double.TryParse(valString, NumberStyles.Any, CultureInfo.InvariantCulture, out double valorFinal))
                        {
                            continue;
                        }
                        
                        // 2. Lectura de la Fecha con formato elegido por el usuario
                        DateTime fechaFinal = DateTime.MinValue;
                        bool fechaEsValida = false;

                        if (cellDate is DateTime dt)
                        {
                            fechaFinal = dt; 
                            fechaEsValida = true;
                        }
                        else
                        {
                            string dateString = cellDate.ToString()!.Trim();

                            if (explicitDateFormat != "Automático")
                            {
                                // Extraemos el código de formato (ej. de "dd/MM/yyyy (Día/Mes/Año)" nos quedamos con "dd/MM/yyyy")
                                string formatCode = explicitDateFormat.Split(' ')[0];
                                fechaEsValida = DateTime.TryParseExact(dateString, formatCode, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFinal);
                            }
                            else
                            {
                                // Modo Automático (El diccionario que teníamos antes)
                                string[] formatosComunes = { "yyyy-MM-dd", "dd/MM/yyyy", "MM/dd/yyyy", "M/d/yyyy", "d/M/yyyy", "yyyy/MM/dd" };
                                fechaEsValida = DateTime.TryParseExact(dateString, formatosComunes, CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaFinal) || 
                                                DateTime.TryParse(dateString, CultureInfo.CurrentCulture, DateTimeStyles.None, out fechaFinal);
                            }
                        }

                        // 3. Validación de seguridad
                        if (fechaEsValida && fechaFinal.Year >= 1900)
                        {
                            dataPoints.Add(new DataPoint
                            {
                                Fecha = fechaFinal,
                                Valor = valorFinal
                            });
                        }
                    }
                }
                catch 
                { 
                    continue; 
                }
            }
        }
        
        return dataPoints.OrderBy(d => d.Fecha).ToList();
    }
}