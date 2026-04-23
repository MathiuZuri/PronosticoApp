using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class NaiveModel : IForecastModel
{
    public string Name => "Método Ingenuo";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null)
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        if (data == null || !data.Any()) return result;

        double lastValue = data.Last().Valor;
        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            result.ForecastedData.Add(new DataPoint
            {
                Fecha = lastDate.AddDays(i),
                Valor = lastValue
            });
        }

        return result;
    }
}