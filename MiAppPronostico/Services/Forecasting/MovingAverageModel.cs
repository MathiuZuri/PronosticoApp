using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class MovingAverageModel : IForecastModel
{
    public string Name => "Método de la Media Móvil Simple";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null, string frecuencia = "Diaria")
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        
        int windowSize = seasonality ?? 5; 
        
        if (data == null || data.Count < windowSize) return result;

        List<double> movingWindow = data.TakeLast(windowSize).Select(d => d.Valor).ToList();
        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            double currentAverage = movingWindow.Average();
            
            DateTime futureDate = frecuencia switch
            {
                "Mensual" => lastDate.AddMonths(i),
                "Anual" => lastDate.AddYears(i),
                _ => lastDate.AddDays(i)
            };
            
            result.ForecastedData.Add(new DataPoint
            {
                Fecha = futureDate,
                Valor = currentAverage
            });

            movingWindow.RemoveAt(0);
            movingWindow.Add(currentAverage);
        }

        return result;
    }
}