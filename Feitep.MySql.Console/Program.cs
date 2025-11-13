using Feitep.MySql.Models;
using Feitep.MySql.Repository;

char opcao = '\0';

do
{
    ApresentarMenu();
    opcao = Console.ReadKey().KeyChar;

    switch (opcao)
    {
        case '1':
            ListarClientes();
            break;
        case '2':
            IncluirCliente();
            break;
        case '3':
            break;
        case '4':
            break;
        case '9':
            break;
        default:
            ShowComandoInvalido();
            break;
    }
} while (opcao != '9');


static SistemaContext RetornarContext()
{
    var contextFactory = new SistemaContextFactory();
    var context = contextFactory.CreateDbContext();
    return context;
}

static void ApresentarMenu()
{
    Console.Clear();
    Console.WriteLine("1 - Listar Clientes");
    Console.WriteLine("2 - Cadastrar Clientes");
    Console.WriteLine("3 - Editar Cliente");
    Console.WriteLine("4 - Excluir Cliente");
    Console.WriteLine("9 - Sair");
    Console.Write("Opção: ");
}

static void ShowComandoInvalido()
{
    Console.WriteLine("\n\nComando inválido!");
    System.Console.WriteLine("Pressione qualquer tecla para continuar ...");
    Console.ReadKey();
}

static void IncluirCliente()
{
    Console.Clear();
    Console.WriteLine("=== INCLUSÃO DE CLIENTE ===\n\n");

    var cliente = new Cliente();
    string? strNacimento = null;
    DateTime dataNacimento;

    do
    {
        Console.Write("Nome: ");
        cliente.Nome = Console.ReadLine();

    } while (String.IsNullOrWhiteSpace(cliente.Nome));

    do
    {
        Console.Write("Data de Nascimento: ");
        strNacimento = Console.ReadLine();
    } while (!DateTime.TryParse(strNacimento, out dataNacimento));

    cliente.DataNascimento = dataNacimento;

    var context = RetornarContext();
    context.Clientes!.Add(cliente);
    context.SaveChanges();
}

static void ListarClientes()
{
    var context = RetornarContext();
    var clientes = context.Clientes!;
    var count = clientes.Count();
    int pages = count / 10;

    var idLen = 10;
    var nomeLen = 30;
    var dataNacimentoLen = 20;
    var totalLen = idLen + nomeLen + dataNacimentoLen;

    ConsoleKey? key = null;

    if (count % 10 > 0)
    {
        pages++;
    }

    int currentPage = 1;

    do
    {
        int offset = (currentPage - 1) * 10;
        var registros = clientes.OrderBy(cliente => cliente.Id)
                                .Skip(offset)
                                .Take(10)
                                .ToList();
        Console.Clear();
        Console.WriteLine("=== LISTAR CLIENTES ===\n\n");
        Console.WriteLine($"|{new String('-', idLen)}|{new String('-', nomeLen)}|{new String('-', dataNacimentoLen)}|");
        Console.WriteLine($"|{" Id".PadRight(idLen)}|{" Nome".PadRight(nomeLen)}|{" Data Nascimento".PadRight(dataNacimentoLen)}|");
        Console.WriteLine($"|{new String('-', idLen)}|{new String('-', nomeLen)}|{new String('-', dataNacimentoLen)}|");

        registros.ForEach(cliente =>
        {
            var id = cliente.Id.ToString();
            var nome = cliente.Nome!;
            var dataNascimento = cliente.DataNascimento.ToString("dd/MM/yyyy");
            Console.WriteLine($"| {id.PadRight(idLen-1)}| {nome.PadRight(nomeLen-1)}| {dataNascimento.PadRight(dataNacimentoLen-1)}|");
        });

        Console.WriteLine($"{new String('-', totalLen + 4)}");

        var textPaginacao = $"Página {currentPage}, {offset+1} de {offset + 10} de {count} registros.";
        Console.WriteLine($"|{textPaginacao.PadRight(totalLen + 2)}|");
        Console.WriteLine($"{new String('-', totalLen + 4)}");


        Console.WriteLine($"^ Próxima página | V Página anterior.");
        key = Console.ReadKey().Key;

        switch (key)
        {
            case ConsoleKey.UpArrow:
                if (currentPage > 1)
                {
                    currentPage--;
                }
                break;

            case ConsoleKey.DownArrow:
                if (currentPage < pages)
                {
                    currentPage++;
                }
                break;
            default:
                break;
        }
    } while (key != ConsoleKey.Escape);
}