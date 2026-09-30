using CalMedAcad.Api.Models;

namespace CalMedAcad.Api.Services;

public class CalculadoraMediaService
{
    public CalculoMediaResponse Calcular(CalculoMediaRequest request)
    {
        ValidarNota(request.Nota1, nameof(request.Nota1));
        ValidarNota(request.Nota2, nameof(request.Nota2));
        ValidarNota(request.Nota3, nameof(request.Nota3));

        ValidarFrequencia(request.Frequencia);

        double media =
            (request.Nota1 +
             request.Nota2 +
             request.Nota3) / 3.0;

        string situacao =
            DeterminarSituacao(media, request.Frequencia);

        return new CalculoMediaResponse
        {
            Media = Math.Round(media, 2),
            Frequencia = request.Frequencia,
            Situacao = situacao
        };
    }

    public string DeterminarSituacao(
        double media,
        double frequencia)
    {
        if (frequencia < 75)
        {
            return "Reprovado por falta";
        }

        if (media >= 7)
        {
            return "Aprovado";
        }

        if (media >= 5)
        {
            return "Recuperação";
        }

        return "Reprovado por nota";
    }

    public bool NotaValida(double nota)
    {
        return nota >= 0 && nota <= 10;
    }

    public bool FrequenciaValida(double frequencia)
    {
        return frequencia >= 0 &&
               frequencia <= 100;
    }

    private void ValidarNota(
        double nota,
        string nomeCampo)
    {
        if (!NotaValida(nota))
        {
            throw new ArgumentOutOfRangeException(
                nomeCampo,
                "A nota deve estar entre 0 e 10."
            );
        }
    }

    private void ValidarFrequencia(double frequencia)
    {
        if (!FrequenciaValida(frequencia))
        {
            throw new ArgumentOutOfRangeException(
                nameof(frequencia),
                "A frequência deve estar entre 0 e 100."
            );
        }
    }
}