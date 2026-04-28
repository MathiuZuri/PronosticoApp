namespace MiAppPronostico.Services;
using MiAppPronostico.Models;

public interface IForecastModel
{
    string Name { get; }

    // El método principal que recibirá los datos y devolverá la proyección
    ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null, string frecuencia = "Diaria");
}
