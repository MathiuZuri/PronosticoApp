using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class MeanModel : IForecastModel
{
    public string Name => "Método de la Media";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null)
    {
        var result = new ForecastResult 
        { 
            MethodName = Name, 
            HistoricalData = data 
        };

        if (data == null || !data.Any()) return result;

        // 1. Calculamos el promedio exacto como lo hacías en Python
        double meanValue = data.Average(d => d.Valor);

        // 2. Obtenemos la última fecha para saber desde dónde proyectar
        DateTime lastDate = data.Last().Fecha;

        // 3. Generamos los puntos futuros
        for (int i = 1; i <= horizon; i++)
        {
            result.ForecastedData.Add(new DataPoint
            {
                // Asumimos saltos diarios por ahora (luego podemos hacerlo dinámico)
                Fecha = lastDate.AddDays(i), 
                Valor = meanValue
            });
        }

        return result;
    }
}