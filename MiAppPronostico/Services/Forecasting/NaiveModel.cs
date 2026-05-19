using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class NaiveModel : IForecastModel
{
    public string Name => "Método Ingenuo";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null, string frecuencia = "Diaria")
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        if (data == null || !data.Any()) return result;

        double lastValue = data.Last().Valor;
        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            DateTime futureDate = frecuencia switch
            {
                "Mensual" => lastDate.AddMonths(i),
                "Anual" => lastDate.AddYears(i),
                _ => lastDate.AddDays(i)
            };

            result.ForecastedData.Add(new DataPoint
            {
                Fecha = futureDate,
                Valor = lastValue
            });
        }

        return result;
    }
}