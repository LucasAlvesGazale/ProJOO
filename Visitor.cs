using System;

// 1. Interface base que recebe a visita
public interface IRelatorio
{
    void Accept(IExportadorVisitor visitor);
}

// 2. Relatórios concretos
public class RelatorioA : IRelatorio
{
    public void Accept(IExportadorVisitor visitor)
    {
        visitor.VisitRelatorioA(this);
    }
}

public class RelatorioB : IRelatorio
{
    public void Accept(IExportadorVisitor visitor)
    {
        visitor.VisitRelatorioB(this);
    }
}

// 3. Interface do Visitor
public interface IExportadorVisitor
{
    void VisitRelatorioA(RelatorioA relatorio);
    void VisitRelatorioB(RelatorioB relatorio);
}

// 4. Visitors concretos
public class ExportadorPDF : IExportadorVisitor
{
    public void VisitRelatorioA(RelatorioA relatorio)
    {
        Console.WriteLine("Convertendo dados do Relatorio A para PDF... pronto.");
    }

    public void VisitRelatorioB(RelatorioB relatorio)
    {
        Console.WriteLine("Convertendo dados do Relatorio B para PDF... ta la.");
    }
}

public class ExportadorHTML : IExportadorVisitor
{
    public void VisitRelatorioA(RelatorioA relatorio)
    {
        Console.WriteLine("Convertendo dados do Relatorio A para HTML... <html><body>A</body></html>");
    }

    public void VisitRelatorioB(RelatorioB relatorio)
    {
        Console.WriteLine("Convertendo dados do Relatorio B para HTML... <html><body>B</body></html>");
    }
}

// 5. Teste
public class Program
{
    public static void Main(string[] args)
    {
        IRelatorio relA = new RelatorioA();
        IRelatorio relB = new RelatorioB();

        IExportadorVisitor exportaPDF = new ExportadorPDF();
        IExportadorVisitor exportaHTML = new ExportadorHTML();

        relA.Accept(exportaPDF);
        relB.Accept(exportaPDF);

        relA.Accept(exportaHTML);
        relB.Accept(exportaHTML);
    }
}
