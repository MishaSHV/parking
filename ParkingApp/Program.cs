using System;
using System.Collections.Generic;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!123");
        }
    }

    class Settings
    {
        public TimeSpan TimeOut { get; set;}
        //public Dictionary
        public int ParkingSpace => 0;
        public decimal Fine => 0;
    }

    class Parking
    {
        List<Car> ListCar;
        List<Transaction> ListTransaction;
        decimal Balnce => 0;

    }

    class Car
    {
        public int Id { get; set; }
        public decimal Balance => 0;
        public CarType Ctype { get; set; }
    }

    enum CarType
    {
        Passenger,Truck,Bus,Motocycle
    }
    class Transaction
    {
        public DateTime DTtransaction { get; set; }
        public int CarId { get; set; }
        public decimal AmountMoney => 0;
    }
}
