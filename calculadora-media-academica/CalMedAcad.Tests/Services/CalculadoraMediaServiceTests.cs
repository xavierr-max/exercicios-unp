using CalMedAcad.Api.Models;
using CalMedAcad.Api.Services;

namespace CalMedAcad.Tests.Services;

public class CalculadoraMediaServiceTests
{
    private readonly CalculadoraMediaService _service = new();

    [Fact]
    public void Calcular_DeveRetornarAprovado_QuandoMediaForMaiorOuIgualASete()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 8,
            Nota2 = 7,
            Nota3 = 9,
            Frequencia = 90
        };

        // Act
        var resultado = _service.Calcular(request);

        // Assert
        Assert.Equal(8, resultado.Media);
        Assert.Equal(90, resultado.Frequencia);
        Assert.Equal("Aprovado", resultado.Situacao);
    }

    [Fact]
    public void Calcular_DeveRetornarRecuperacao_QuandoMediaEstiverEntreCincoESete()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 5,
            Nota2 = 6,
            Nota3 = 7,
            Frequencia = 80
        };

        // Act
        var resultado = _service.Calcular(request);

        // Assert
        Assert.Equal(6, resultado.Media);
        Assert.Equal("Recuperação", resultado.Situacao);
    }

    [Fact]
    public void Calcular_DeveRetornarReprovadoPorNota_QuandoMediaForMenorQueCinco()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 2,
            Nota2 = 3,
            Nota3 = 4,
            Frequencia = 90
        };

        // Act
        var resultado = _service.Calcular(request);

        // Assert
        Assert.Equal(3, resultado.Media);
        Assert.Equal("Reprovado por nota", resultado.Situacao);
    }

    [Fact]
    public void Calcular_DeveRetornarReprovadoPorFalta_MesmoComMediaAlta()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 10,
            Nota2 = 10,
            Nota3 = 10,
            Frequencia = 74
        };

        // Act
        var resultado = _service.Calcular(request);

        // Assert
        Assert.Equal(10, resultado.Media);
        Assert.Equal("Reprovado por falta", resultado.Situacao);
    }

    [Theory]
    [InlineData(7.00, 75, "Aprovado")]
    [InlineData(6.99, 75, "Recuperação")]
    [InlineData(5.00, 75, "Recuperação")]
    [InlineData(4.99, 75, "Reprovado por nota")]
    [InlineData(10.00, 74.99, "Reprovado por falta")]
    public void DeterminarSituacao_DeveRespeitarValoresLimite(
        double media,
        double frequencia,
        string situacaoEsperada)
    {
        // Act
        var resultado = _service.DeterminarSituacao(
            media,
            frequencia
        );

        // Assert
        Assert.Equal(situacaoEsperada, resultado);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(5)]
    [InlineData(10)]
    public void NotaValida_DeveRetornarTrue_QuandoNotaForValida(
        double nota)
    {
        // Act
        var resultado = _service.NotaValida(nota);

        // Assert
        Assert.True(resultado);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    [InlineData(10.01)]
    [InlineData(11)]
    public void NotaValida_DeveRetornarFalse_QuandoNotaForInvalida(
        double nota)
    {
        // Act
        var resultado = _service.NotaValida(nota);

        // Assert
        Assert.False(resultado);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(75)]
    [InlineData(100)]
    public void FrequenciaValida_DeveRetornarTrue_QuandoFrequenciaForValida(
        double frequencia)
    {
        // Act
        var resultado =
            _service.FrequenciaValida(frequencia);

        // Assert
        Assert.True(resultado);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(-0.01)]
    [InlineData(100.01)]
    [InlineData(101)]
    public void FrequenciaValida_DeveRetornarFalse_QuandoFrequenciaForInvalida(
        double frequencia)
    {
        // Act
        var resultado =
            _service.FrequenciaValida(frequencia);

        // Assert
        Assert.False(resultado);
    }

    [Fact]
    public void Calcular_DeveLancarExcecao_QuandoNotaForMenorQueZero()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = -1,
            Nota2 = 5,
            Nota3 = 5,
            Frequencia = 80
        };

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _service.Calcular(request)
        );
    }

    [Fact]
    public void Calcular_DeveLancarExcecao_QuandoNotaForMaiorQueDez()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 11,
            Nota2 = 5,
            Nota3 = 5,
            Frequencia = 80
        };

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _service.Calcular(request)
        );
    }

    [Fact]
    public void Calcular_DeveLancarExcecao_QuandoFrequenciaForMenorQueZero()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 7,
            Nota2 = 7,
            Nota3 = 7,
            Frequencia = -1
        };

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _service.Calcular(request)
        );
    }

    [Fact]
    public void Calcular_DeveLancarExcecao_QuandoFrequenciaForMaiorQueCem()
    {
        // Arrange
        var request = new CalculoMediaRequest
        {
            Nota1 = 7,
            Nota2 = 7,
            Nota3 = 7,
            Frequencia = 101
        };

        // Act + Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => _service.Calcular(request)
        );
    }
}