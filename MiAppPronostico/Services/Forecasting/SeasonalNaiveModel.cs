using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Forecasting;

public class SeasonalNaiveModel : IForecastModel
{
    public string Name => "Método Ingenuo Estacional";

    public ForecastResult Calculate(List<DataPoint> data, int horizon, int? seasonality = null, string frecuencia = "Diaria")
    {
        var result = new ForecastResult { MethodName = Name, HistoricalData = data };
        
        int m = seasonality ?? 7;

        if (data == null || data.Count < m) return result;

        DateTime lastDate = data.Last().Fecha;

        for (int i = 1; i <= horizon; i++)
        {
            int indexToCopy = data.Count - m + ((i - 1) % m);
            double valueToCopy = data[indexToCopy].Valor;

            DateTime futureDate = frecuencia switch
            {
                "Mensual" => lastDate.AddMonths(i),
                "Anual" => lastDate.AddYears(i),
                _ => lastDate.AddDays(i)
            };

            result.ForecastedData.Add(new DataPoint
            {
                Fecha = futureDate,
                Valor = valueToCopy
            });
        }

        return result;
    }
}