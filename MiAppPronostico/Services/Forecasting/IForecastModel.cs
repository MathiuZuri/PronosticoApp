using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public interface IForecastModel
{
    string Name { get; }

    // Añadimos el parámetro de frecuencia al final
    ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null, string frecuencia = "Diaria");
}