# ValidaBrasil

ValidaBrasil é um projeto para validação e formatação de documentos e informações brasileiras, como CPF, CNPJ, RENAVAM, Título Eleitoral e outros.

## Estrutura do Projeto

A estrutura do projeto foi planejada para ser modular e escalável, permitindo fácil manutenção e extensão.

```text
ValidaBrasil
├── Enums
│   ├── CodigosEstados.cs      # Códigos de estados do Brasil
│   ├── TipoDocumento.cs       # Enumeração para os tipos de documentos
│   └── TipoPlacaVeiculo.cs    # Enumeração para tipos de placa (Antiga e Mercosul)
├── Interfaces
│   └── IOperacao.cs           # Interface para operações de validação e formatação
├── Modelos
│   ├── Cpf.cs                 # Lógica para CPF
│   ├── Cnpj.cs                # Lógica para CNPJ
│   ├── Renavam.cs             # Lógica para RENAVAM
│   ├── PlacaVeiculo.cs        # Lógica para placas de veículos
│   ├── Telefone.cs            # Lógica para telefones
│   └── TituloEleitoral.cs     # Lógica para Título Eleitoral
├── Servicos
│   ├── FormatacaoServico.cs   # Serviço de formatação
│   └── ValidacaoServico.cs    # Serviço de validação
└── ValidaBrasil.cs            # Classe principal que abstrai os serviços
```

## Arquitetura

O projeto segue uma arquitetura baseada em **design modular** com aplicação de **princípios SOLID** e padrões de projeto. Ele é composto por três camadas principais:

- **Modelos**: Implementações específicas para cada tipo de dado/documento, com validação e formatação.
- **Serviços**: Classes para orquestrar a lógica de negócios e delegar operações aos modelos.
- **Fachada** (`ValidaBrasil`): Ponto de entrada simplificado para os consumidores do sistema.

### Componentes Principais

1. **Modelos**: Contêm a implementação de cada tipo de documento ou dado. Por exemplo:
   - `Cpf.cs`: Implementação para CPF, com métodos de validação e formatação.
   - `Cnpj.cs`: Implementação para CNPJ, com métodos de validação e formatação.
   - `Renavam.cs`: Implementação para RENAVAM, com métodos de validação e formatação.

2. **Serviços**: Gerenciam a orquestração entre a validação e formatação dos dados. As classes `FormatacaoServico` e `ValidacaoServico` fazem o trabalho de delegar as operações para os modelos apropriados.

3. **Fachada**: A classe `ValidaBrasil.cs` fornece uma interface unificada que simplifica o uso do sistema. Ela expõe métodos estáticos para validação e formatação de diferentes tipos de documentos.

---

## Padrões de Projeto

O projeto segue os seguintes padrões de projeto:

- **Factory Method**:
  O serviço `FormatacaoServico` utiliza um padrão de fábrica simples para criar instâncias de classes baseadas no tipo de documento, abstraindo a lógica de criação e manutenção de objetos específicos de cada tipo.

- **Interface Segregation**:
  A interface `IOperacao` define contratos claros para validação, formatação e remoção de formatação. Cada classe que implementa essa interface deve prover sua própria implementação, garantindo flexibilidade e manutenção facilitada.

- **Fachada**:
  A classe estática `ValidaBrasil` atua como uma fachada, oferecendo uma interface simples para o consumidor, escondendo a complexidade do processo de validação e formatação por trás da interface unificada.

---

## Exemplo de Uso

Abaixo estão exemplos de como utilizar as funcionalidades do projeto:

### Validação de um CPF

```csharp
using ValidaBrasil;
using ValidaBrasil.Enums;

class Program
{
    static void Main()
    {
        string cpf = "123.456.789-09";

        bool isValido = ValidaBrasil.Validacao(TipoDocumento.CPF, cpf);

        Console.WriteLine(isValido ? "CPF válido" : "CPF inválido");
    }
}
```

### Validação de Placa de PlacaVeiculo

```csharp
using ValidaBrasil;
using ValidaBrasil.Enums;

class Program
{
    static void Main()
    {
        string placa = "ABC1234";

        string placaFormatada = ValidaBrasil.Formatacao(TipoDocumento.PLACAVEICULO, placa);

        Console.WriteLine($"Placa formatada: {placaFormatada}");
    }
}
```

### Remoção de formatação de um CNPJ
```csharp
using ValidaBrasil;
using ValidaBrasil.Enums;

class Program
{
    static void Main()
    {
        string cnpjFormatado = "12.345.678/0001-99";

        string cnpjSemFormatacao = ValidaBrasil.RemoverFormatacao(TipoDocumento.CNPJ, cnpjFormatado);

        Console.WriteLine($"CNPJ sem formatação: {cnpjSemFormatacao}");
    }
}
```

## Dependências
Este projeto não possui dependências externas, exceto as bibliotecas padrão do .NET.
- .NET Standard 2.0

## Como Contribuir

1. Faça um fork do projeto.
2. Crie uma branch para sua feature ou correção (`git checkout -b minha-feature`).
3. Comite suas mudanças (`git commit -am 'Adicionando minha feature'`).
4. Envie para o repositório remoto (`git push origin minha-feature`).
5. Crie um pull request.

---

## Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

---

## Contato

- **Desenvolvedor**: Natanael

