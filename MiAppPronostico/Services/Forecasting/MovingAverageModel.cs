using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class MovingAverageModel : IForecastModel
{
    public string Name => "Método de la Media Móvil Simple";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null)
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        
        // Si no nos pasan una ventana, por defecto usamos 5
        int windowSize = seasonality ?? 5; 
        
        if (data == null || data.Count < windowSize) return result;

        // Extraemos los valores iniciales para nuestra ventana
        List<double> movingWindow = data.TakeLast(windowSize).Select(d => d.Valor).ToList();
        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            // Calculamos el promedio de la ventana actual
            double currentAverage = movingWindow.Average();
            
            result.ForecastedData.Add(new DataPoint
            {
                Fecha = lastDate.AddDays(i),
                Valor = currentAverage
            });

            // Actualizamos la ventana: quitamos el elemento más viejo y agregamos la nueva predicción
            movingWindow.RemoveAt(0);
            movingWindow.Add(currentAverage);
        }

        return result;
    }
}