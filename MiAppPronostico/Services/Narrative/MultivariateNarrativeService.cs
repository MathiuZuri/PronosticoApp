using System.Text;
using MiAppPronostico.Models;

namespace MiAppPronostico.Services.Narrative;

public class MultivariateNarrativeService
{
    // INTERPRETACIÓN AUTOMÁTICA DE LA REGRESIÓN MÚLTIPLE
    public string GenerarDiagnosticoRegresion(RegressionResult resultado, string varDependiente)
    {
        var sb = new StringBuilder();
        double r2Porcentaje = resultado.RSquared * 100;

        sb.Append("<div class='rz-p-2'>");
        sb.Append($"<p style='font-size:1.1em; margin-bottom:12px;'>El modelo analítico diseñado para evaluar <strong>{varDependiente}</strong> presenta un Coeficiente de Determinación <strong>(R²) de {r2Porcentaje:0.02}%</strong>.</p>");

        // Diagnóstico según el umbral de R²
        if (resultado.RSquared >= 0.90)
        {
            sb.Append("<p style='color:#2e7d32; font-weight:600;'>✔️ AJUSTE EXCELENTE:</p>");
            sb.Append("<p>Las variables independientes seleccionadas contienen prácticamente toda la información necesaria para explicar el comportamiento del sistema. El nivel de error residual es despreciable, haciendo al modelo altamente confiable para auditorías y toma de decisiones.</p>");
        }
        else if (resultado.RSquared >= 0.70)
        {
            sb.Append("<p style='color:#1976d2; font-weight:600;'>✔️ AJUSTE BUENO:</p>");
            sb.Append("<p>El modelo captura la tendencia principal de forma sólida. Aunque existen factores externos menores no controlados por el dataset actual, las predicciones mantienen un alto grado de certeza práctica.</p>");
        }
        else if (resultado.RSquared >= 0.50)
        {
            sb.Append("<p style='color:#f57c00; font-weight:600;'>⚠️ AJUSTE MODERADO:</p>");
            sb.Append("<p>El modelo identifica la dirección general de la tendencia, pero carece de precisión fina. Se recomienda enriquecer el archivo Excel con variables predictoras adicionales para estabilizar los coeficientes.</p>");
        }
        else
        {
            sb.Append("<p style='color:#c62828; font-weight:600;'>❌ AJUSTE BAJO / INSIGNIFICANTE:</p>");
            sb.Append("<p>Las variables elegidas no logran explicar el fenómeno analizado. Los coeficientes actuales son inestables y no deben usarse para planeamiento estratégico sin antes realizar una limpieza de ruido o cambiar el enfoque del experimento.</p>");
        }

        // Análisis de Coeficientes e Impacto Colectivo
        if (resultado.Coeficientes.Any())
        {
            sb.Append("<h5 style='margin-top:20px; font-weight:700; color:var(--sigepp-primary);'>🔍 Desglose Estadístico de Factores:</h5>");
            sb.Append("<ul>");
            sb.Append($"<li><strong>Condición Base (Intercepto):</strong> Si todas las variables independientes operasen en cero, el valor estimado inicial para {varDependiente} sería de <strong>{resultado.Intercepto:0.04}</strong> unidades.</li>");

            // Identificar el factor dominante (el de mayor impacto absoluto)
            var factorDominante = resultado.Coeficientes.OrderByDescending(c => Math.Abs(c.Value)).First();
            sb.Append($"<li><strong>Factor Crítico de Impacto:</strong> La variable <strong>{factorDominante.Key}</strong> es el elemento de mayor peso en la ecuación. Cada incremento unitario en este factor altera el resultado final en <strong>{factorDominante.Value:0.04}</strong> unidades.</li>");
            sb.Append("</ul>");

            sb.Append("<p style='margin-top:10px;'><strong>Dinámica de Comportamiento Individual:</strong></p><ul>");
            foreach (var coef in resultado.Coeficientes)
            {
                string direccion = coef.Value >= 0 ? "un incremento" : "una reducción";
                sb.Append($"<li>Por cada unidad que sube el factor <strong>{coef.Key}</strong>, se genera {direccion} de <strong>{Math.Abs(coef.Value):0.04}</strong> unidades en <strong>{varDependiente}</strong>, asumiendo que los demás componentes permanecen estáticos.</li>");
            }
            sb.Append("</ul>");
        }

        sb.Append("</div>");
        return sb.ToString();
    }

    // INTERPRETACIÓN AUTOMÁTICA DE LA MATRIZ DE CONFUSIÓN
    public string GenerarDiagnosticoMatriz(ConfusionMatrixResult resultado)
    {
        var sb = new StringBuilder();
        double exactitudPorcentaje = resultado.Exactitud * 100;

        sb.Append("<div class='rz-p-2'>");
        sb.Append($"<p style='font-size:1.1em; margin-bottom:12px;'>La evaluación del modelo de clasificación para el evento objetivo <strong>'{resultado.ClasePositiva}'</strong> arrojó una <strong>Exactitud (Accuracy) del {exactitudPorcentaje:0.02}%</strong>, resolviendo de forma correcta <strong>{resultado.VerdaderosPositivos + resultado.VerdaderosNegativos}</strong> de los <strong>{resultado.Total}</strong> escenarios evaluados.</p>");

        sb.Append("<h5 style='font-weight:700; color:var(--sigepp-primary); margin-top:15px;'>🚨 Análisis de Riesgos e Impacto Operacional:</h5>");
        sb.Append("<ul>");
        sb.Append($"<li><strong>Aciertos Positivos (VP = {resultado.VerdaderosPositivos}):</strong> Se identificaron con éxito {resultado.VerdaderosPositivos} estados críticos reales. Esto demuestra la efectividad preventiva del algoritmo.</li>");
        sb.Append($"<li><strong>Aciertos Negativos (VN = {resultado.VerdaderosNegativos}):</strong> El sistema discriminó correctamente {resultado.VerdaderosNegativos} operaciones normales, previniendo falsas alarmas operativas.</li>");
        sb.Append("</ul>");

        // Diagnóstico crítico de errores (Tipo I y Tipo II)
        if (resultado.FalsosPositivos > 0 || resultado.FalsosNegativos > 0)
        {
            sb.Append("<div style='background-color:#fff3cd; padding:12px; border-left:4px solid #ffb300; border-radius:4px; margin-top:15px;'>");
            sb.Append("<strong>🔍 Evaluación de Márgenes de Error:</strong><br/><ul style='margin-top:5px; margin-bottom:0;'>");
            
            if (resultado.FalsosPositivos > 0)
            {
                sb.Append($"<li><strong>Falsos Positivos (Error Tipo I = {resultado.FalsosPositivos}):</strong> El algoritmo generó {resultado.FalsosPositivos} alertas de falsa alarma. En producción, esto se traduce en horas de auditoría en vano o revisiones técnicas redundantes de hardware/procesos estables.</li>");
            }
            if (resultado.FalsosNegativos > 0)
            {
                sb.Append($"<li><strong>Falsos Negativos (Error Tipo II = {resultado.FalsosNegativos}):</strong> <span style='color:#c62828; font-weight:bold;'>⚠️ CRÍTICO:</span> Se registraron {resultado.FalsosNegativos} fallas omitidas. El sistema reportó un estado seguro cuando en realidad la infraestructura estaba colapsada. En ingeniería, este error es el más peligroso porque enmascara vulnerabilidades inminentes.</li>");
            }
            sb.Append("</ul></div>");
        }
        else
        {
            sb.Append("<p style='color:#2e7d32; font-weight:600; margin-top:10px;'>✨ DESEMPEÑO IMPECABLE: El modelo no presentó desviaciones operacionales ni falsas omisiones en este lote de datos.</p>");
        }

        sb.Append("</div>");
        return sb.ToString();
    }
}