using MiAppPronostico.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace MiAppPronostico.Services;

public class NarrativeService
{
    // Definimos el glosario como una constante privada para reutilizarlo en ambos lados
    private const string GlosarioTecnico = @"
        <hr style='border-top: 1px solid var(--sigepp-border); margin: 15px 0;' />
        <div style='font-size: 0.85em; color: var(--sigepp-text-muted); line-height: 1.4;'>
            <strong style='color: var(--sigepp-text);'>📚 Glosario Técnico (Para principiantes):</strong>
            <ul style='margin-top: 5px; margin-bottom: 0; padding-left: 20px;'>
                <li style='margin-bottom: 4px;'><strong>Error Medio (MAE):</strong> Mide en <em>unidades reales</em> cuánto se equivocó el modelo en promedio. Si predices ventas de zapatos y tu MAE es 20, significa que el cálculo falló por unas 20 cajas.</li>
                <li style='margin-bottom: 4px;'><strong>Precisión (RMSE):</strong> Es similar al MAE, pero su fórmula matemática <em>castiga duramente los errores extremos</em>. Si este número es mucho más alto que el MAE, significa que el modelo suele ser bueno, pero a veces tiene días donde falla de forma espectacular.</li>
                <li><strong>Margen de Error (MAPE):</strong> Es la métrica reina. Expresa el error en <em>porcentaje</em>, lo que lo hace universal. Un MAPE del 10% significa que tus proyecciones tienen una precisión del 90%.</li>
            </ul>
        </div>";

    public string GenerarInterpretacion(ForecastResult result)
    {
        if (result == null || !result.ForecastedData.Any()) 
            return "No hay datos suficientes para generar una interpretación.";

        var ultimoReal = result.HistoricalData.Last().Valor;
        var ultimoPronostico = result.ForecastedData.Last().Valor;
        
        // 1. Análisis de Tendencia Humano
        bool esCreciente = ultimoPronostico > ultimoReal;
        var tendencia = esCreciente ? "al alza 📈" : "a la baja 📉";
        var cambioPorcentual = Math.Abs((ultimoPronostico - ultimoReal) / ultimoReal) * 100;

        // 2. Explicación Educativa del Modelo (Basado en tus clases)
        string nombreModelo = result.MethodName.ToLower();
        string explicacionModelo = "Este algoritmo analiza el comportamiento pasado para proyectar el futuro más probable."; // Por defecto
        
        if (nombreModelo.Contains("seasonal") || nombreModelo.Contains("estacional"))
            explicacionModelo = "Este método busca patrones que se repiten en el tiempo (como ventas altas todos los fines de mes o diciembres) y los proyecta hacia el futuro.";
        else if (nombreModelo.Contains("naive") || nombreModelo.Contains("ingenuo"))
            explicacionModelo = "Este es un método muy conservador. Asume que lo que pasará mañana será exactamente igual a lo que pasó hoy, sin complicarse.";
        else if (nombreModelo.Contains("moving") || nombreModelo.Contains("móvil"))
            explicacionModelo = "Este algoritmo suaviza los picos y caídas recientes de tus datos para mostrarte hacia dónde va la tendencia real a corto plazo.";
        else if (nombreModelo.Contains("mean") || nombreModelo.Contains("promedio"))
            explicacionModelo = "Este cálculo toma toda tu historia y saca un promedio general. Es muy útil si tus datos son estables y no tienen cambios bruscos a lo largo del tiempo.";
        else if (nombreModelo.Contains("drift") || nombreModelo.Contains("tendencia"))
            explicacionModelo = "Este algoritmo traza una línea directa desde tu primer dato histórico hasta el último, mostrando el crecimiento o caída general, ignorando el ruido del día a día.";

        // 3. Traducción del MAPE a "Nivel de Confianza"
        string nivelRiesgo;
        string consejo;
        
        if (result.MAPE < 5)
        {
            nivelRiesgo = "<span style='color: #28a745; font-weight: bold;'>Muy Alto (Excelente)</span>";
            consejo = "Puedes confiar bastante en esta proyección para tomar decisiones.";
        }
        else if (result.MAPE < 15)
        {
            nivelRiesgo = "<span style='color: #fd7e14; font-weight: bold;'>Moderado (Bueno)</span>";
            consejo = "Los datos son confiables, pero te sugerimos mantener un margen de seguridad.";
        }
        else if (result.MAPE < 25)
        {
            nivelRiesgo = "<span style='color: #dc3545; font-weight: bold;'>Bajo (Precaución)</span>";
            consejo = "La proyección marca un rumbo, pero tus datos históricos son inestables. Úsalo solo como una guía general.";
        }
        else
        {
            nivelRiesgo = "<span style='color: #6c757d; font-weight: bold;'>Muy Bajo (Alta Incertidumbre)</span>";
            consejo = "Tus datos históricos son demasiado impredecibles. No recomendamos tomar decisiones importantes basadas únicamente en este gráfico.";
        }

        // 4. Explicación simple de las métricas (si existen)
        string explicacionMetricas = "";
        string glosario = "";
        if (result.MAE > 0)
        {
            explicacionMetricas = $@"
                <div style='background-color: var(--sigepp-bg-alt); padding: 10px; border-radius: 6px; margin-top: 10px; font-size: 0.9em;'>
                    <strong>📊 Para los curiosos de los números:</strong><br/>
                    En promedio, nuestras simulaciones fallaron por <strong>{result.MAE:N2} unidades</strong> (Error Medio). 
                    Además, el porcentaje general de equivocación del modelo es del <strong>{result.MAPE:N2}%</strong>.
                </div>";
                
            // Solo agregamos el glosario si hay métricas para explicar
            glosario = GlosarioTecnico;
        }

        // 5. Construcción del mensaje final en HTML
        return $@"
            <p style='margin-bottom: 8px;'>Has analizado tus datos con el modelo <strong>{result.MethodName}</strong>. <br/><em>¿Cómo funciona?</em> {explicacionModelo}</p>
            
            <ul style='margin-bottom: 8px; padding-left: 20px;'>
                <li><strong>Proyección:</strong> Se espera que la tendencia general vaya <strong>{tendencia}</strong>.</li>
                <li><strong>Estimación:</strong> El valor proyectado hacia el final del período es de <strong>{ultimoPronostico:N2}</strong>, lo que representa un cambio del <strong>{cambioPorcentual:N1}%</strong> respecto a tu situación actual ({ultimoReal:N2}).</li>
            </ul>

            <p style='margin-bottom: 0;'><strong>Nivel de confianza en este resultado: {nivelRiesgo}</strong>. <br/>
            {consejo}</p>
            {explicacionMetricas}
            {glosario}
        ";
    }

    public string GenerarConclusionTorneo(List<ForecastResult> resultados)
    {
        if (!resultados.Any()) return "";
        
        var ganador = resultados.First();
        var perdedor = resultados.Last();
        
        // Calculamos cuánto mejor es el ganador comparado con el peor modelo
        double mejoraPorcentual = perdedor.MAPE.GetValueOrDefault() - ganador.MAPE.GetValueOrDefault();

        return $@"
            <p>Hemos puesto a competir a <strong>{resultados.Count} algoritmos matemáticos</strong> intentando predecir el comportamiento de tus datos.</p>
            
            <p>🏆 El ganador indiscutible de esta prueba es el modelo <strong>{ganador.MethodName}</strong>. 
            Fue el algoritmo que mejor logró entender tu historia y replicarla con la menor cantidad de errores.</p>
            
            <ul style='margin-bottom: 0; padding-left: 20px;'>
                <li><strong>¿Por qué ganó?:</strong> Su margen de error promedio es de apenas el <strong>{ganador.MAPE:N2}%</strong>.</li>
                <li><strong>¿Por qué es útil este torneo?:</strong> Si hubieras elegido el peor algoritmo para tu caso (como el {perdedor.MethodName}), tu riesgo de equivocarte habría sido un <strong>{mejoraPorcentual:N2}% mayor</strong>.</li>
            </ul>
            <p style='margin-top: 10px; margin-bottom: 15px; font-size: 0.9em; color: var(--sigepp-text-muted);'>
                <em>Recomendación: Regresa al 'Panel de Pronósticos' y selecciona este modelo ganador para exportar tus datos finales.</em>
            </p>
            
            {GlosarioTecnico}
        ";
    }
}