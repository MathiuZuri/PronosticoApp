using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class DriftModel : IForecastModel
{
    public string Name => "Método de la Deriva";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null, string frecuencia = "Diaria")
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        
        if (data == null || data.Count < 2) return result;

        double firstValue = data.First().Valor;
        double lastValue = data.Last().Valor;
        int n = data.Count;
        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            double forecastValue = lastValue + i * ((lastValue - firstValue) / (n - 1));

            // Lógica dinámica de salto temporal
            DateTime futureDate = frecuencia switch
            {
                "Mensual" => lastDate.AddMonths(i),
                "Anual" => lastDate.AddYears(i),
                _ => lastDate.AddDays(i)
            };

            result.ForecastedData.Add(new DataPoint
            {
                Fecha = futureDate,
                Valor = forecastValue
            });
        }

        return result;
    }
}