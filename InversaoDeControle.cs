using System;
using System.Collections.Generic;

// Interface do observador
interface IObserver
{
    // Callback: o Subject chama este método quando o estado muda.
    void Update(double temperatura, double ph, double umidade);
}

// Classe base do Subject
abstract class Subject
{
    private readonly List<IObserver> observers = new List<IObserver>();

    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    // Inversão de controle:
    // o Subject decide o momento da notificação e chama os observadores.
    protected void NotifyObservers(double temperatura, double ph, double umidade)
    {
        foreach (IObserver observer in observers)
        {
            observer.Update(temperatura, ph, umidade); // callback
        }
    }
}

// Coletor de dados na Amazônia (Subject)
class Coletor : Subject
{
    private double temp;
    private double ph;
    private double umidade;

    public void SetDados(double temp, double ph, double umidade)
    {
        this.temp = temp;
        this.ph = ph;
        this.umidade = umidade;

        // Ao mudar o estado, o Subject chama os observadores.
        NotifyObservers(this.temp, this.ph, this.umidade);
    }
}

// Universidade que recebe os dados (Observer)
class Universidade : IObserver
{
    private readonly string nome;

    public Universidade(string nome)
    {
        this.nome = nome;
    }

    // Callback: este método é chamado pelo Subject quando os dados mudam.
    public void Update(double temperatura, double ph, double umidade)
    {
        Console.WriteLine(
            $"{nome} recebeu -> Temp: {temperatura}, pH: {ph}, Umidade: {umidade}"
        );
    }
}

class Program
{
    static void Main(string[] args)
    {
        Coletor coletor = new Coletor();

        coletor.AddObserver(new Universidade("São José"));
        coletor.AddObserver(new Universidade("São Paulo"));
        coletor.AddObserver(new Universidade("Porto Alegre"));

        coletor.SetDados(31.4, 6.8, 89.2);
    }
}
