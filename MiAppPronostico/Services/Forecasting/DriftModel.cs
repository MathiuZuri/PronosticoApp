using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class DriftModel : IForecastModel
{
    public string Name => "Método de la Deriva";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null)
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        
        // Necesitamos al menos 2 puntos para calcular una pendiente
        if (data == null || data.Count < 2) return result;

        double firstValue = data.First().Valor;
        double lastValue = data.Last().Valor;
        int n = data.Count;
        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            // Fórmula: último + h * ((último - primero) / (n - 1))
            double forecastValue = lastValue + i * ((lastValue - firstValue) / (n - 1));

            result.ForecastedData.Add(new DataPoint
            {
                Fecha = lastDate.AddDays(i),
                Valor = forecastValue
            });
        }

        return result;
    }
}