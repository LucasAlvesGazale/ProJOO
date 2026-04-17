using System;
using System.Collections.Generic;

// Classe Subject
class Subject
{
    private List<Observer> observers = new List<Observer>();

    public void AddObserver(Observer observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(Observer observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers()
    {
        foreach (Observer obs in observers)
        {
            obs.Update(this);
        }
    }
}

// Interface Observer
interface Observer
{
    void Update(Subject s);
}

// Coletor de dados na Amazônia
class Coletor : Subject
{
    private double temp;
    private double ph;
    private double umidade;

    public double GetTemp()
    {
        return temp;
    }

    public double GetPH()
    {
        return ph;
    }

    public double GetUmidade()
    {
        return umidade;
    }

    public void SetDados(double temp, double ph, double umidade)
    {
        this.temp = temp;
        this.ph = ph;
        this.umidade = umidade;
        NotifyObservers();
    }
}

// Universidade que recebe os dados
class Universidade : Observer
{
    private string nome;

    public Universidade(string nome)
    {
        this.nome = nome;
    }

    public void Update(Subject s)
    {
        Coletor c = (Coletor)s;
        Console.WriteLine(
            nome + " recebeu -> " +
            "Temp: " + c.GetTemp() +
            ", pH: " + c.GetPH() +
            ", Umidade: " + c.GetUmidade()
        );
    }
}

class Program
{
    static void Main(string[] args)
    {
        Coletor c = new Coletor();

        c.AddObserver(new Universidade("São José"));
        c.AddObserver(new Universidade("São Paulo"));
        c.AddObserver(new Universidade("Porto Alegre"));

        c.SetDados(31.4, 6.8, 89.2);
    }
}
