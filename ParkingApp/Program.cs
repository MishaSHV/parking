using System;
using System.Collections.Generic;

namespace ParkingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!123");
            Parking.Instance.Process();
        }
    }

    class Settings
    {
        public TimeSpan TimeOut { get; set; } = new TimeSpan(0, 0, 1);
        //public Dictionary
        public int ParkingSpace { get; private set; }
        public decimal Fine { get; private set; }
        public Settings(int pSpaces,decimal f)
        {
            ParkingSpace = pSpaces;
            Fine = f;
        }
    }

    public sealed class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking(new Settings(100,0.2M)  ) );
        List<Car> ListCar;
        List<Transaction> ListTransaction;
        decimal Balance { get; set;}
        private readonly Settings settings_;
        private Parking(Settings s)
        {
            settings_ = s;
        }
        public static Parking Instance{ get { return lazy.Value; } }
        //main func
        //process command menu
        private void SetupWithdrawPeriod()
        {
            int period;
            Console.WriteLine("Enter the new period value (positive,more equal 1 sec) :");
            if (Int32.TryParse(Console.ReadLine(), out period))
            {
                if(1<=period)
                {
                    settings_.TimeOut = new TimeSpan(0, 0, period);
                    Console.WriteLine($"New withdraw period: {settings_.TimeOut} sec");
                }
                else
                {
                    Console.WriteLine("Period is should be more 0");
                }
            }
            else
            {
                Console.WriteLine("Wrong period value");
            }
        }
        private bool ReadIdDialog(out int Id)
        {
            bool ret_val = false; 
            Console.WriteLine("Enter the car Id:");
            if (!Int32.TryParse(Console.ReadLine(), out Id))
            {
                Console.WriteLine("Wrong Id value");
                ret_val = false;
            }
            else
            {
                ret_val = true;
            }
            return ret_val;
        }
        private void AddCar()
        {

            int Id;
            if (ReadIdDialog(out Id))
            {
                //processing
            }
        }
        private void DeleteCar()
        {
            int Id;
            if (ReadIdDialog(out Id))
            {
                //processing
            }
        }
        private void AddCarBalance()
        {
            int Id;
            if (ReadIdDialog(out Id))
            {
                //processing

            }
        }
        public void Process()
        {
            Menu Menu=new Menu();
            Menus CurrentMenu = Menus.Initial;
            Menus PrevMenu = CurrentMenu;
            bool isContinue = true;
            void CheckMenuChange()
            {
                if (PrevMenu != CurrentMenu)
                {
                    Menu.isNeedChange = true;
                }
                else
                {
                    Menu.isNeedChange = false;
                }
                PrevMenu = CurrentMenu;
            }

            while (isContinue)
            {
                switch(CurrentMenu)
                {
                    case Menus.Initial:
                        if(Menu.isNeedChange)
                        {
                            Menu.Clear();
                            Menu.Add(new MenuItem(Menu.Count, "Add/delete car from parking", () => { CurrentMenu = Menus.AddDelCar; }));
                            Menu.Add(new MenuItem(Menu.Count, "Add car balance", () => { AddCarBalance(); }));
                            Menu.Add(new MenuItem(Menu.Count, "Display transaction history for the last minute", () => { Console.WriteLine("The transaction history displayed"); }));
                            Menu.Add(new MenuItem(Menu.Count, "Derive total parking revenue", () => { Console.WriteLine("The total revenue is: ..."); }));
                            Menu.Add(new MenuItem(Menu.Count, "Display the number of available parking spaces", () => { Console.WriteLine($"The total available parking spaces is: {settings_.ParkingSpace}"); }));
                            Menu.Add(new MenuItem(Menu.Count, "Display Transactions.log", () => { Console.WriteLine("Formating output Transaction.log ..."); }));
                            Menu.Add(new MenuItem(Menu.Count, "Options", () => { CurrentMenu = Menus.Options; }));
                            Menu.Add(new MenuItem(Menu.Count, "Exit", () => { isContinue = false;  }));

                            Menu.DisplayMenuItems();
                        }

                        Menu.SelectMenuItemDialog();
                        Menu.ProcessMenuItem();
                        CheckMenuChange();
                        break;

                    case Menus.AddDelCar:
                        if (Menu.isNeedChange)
                        {
                            Menu.Clear();
                            Menu.Add(new MenuItem(Menu.Count, "Add car from parking", () => { AddCar(); }));
                            Menu.Add(new MenuItem(Menu.Count, "Delete car from parking", () => { DeleteCar(); }));
                            Menu.Add(new MenuItem(Menu.Count, "Previous menu", () => { CurrentMenu = Menus.Initial; }));
                            Menu.Add(new MenuItem(Menu.Count, "Exit", () => { isContinue = false; }));
                            Menu.DisplayMenuItems();
                        }

                        Menu.SelectMenuItemDialog();
                        Menu.ProcessMenuItem();
                        CheckMenuChange();
                        break;

                    case Menus.Options:
                        if (Menu.isNeedChange)
                        {
                            Menu.Clear();
                            Menu.Add(new MenuItem(Menu.Count, "Print money withdraw period, sec", () => { Console.WriteLine($"Withdraw period: {settings_.TimeOut} sec"); }));
                            Menu.Add(new MenuItem(Menu.Count, "Setup money withdraw period, sec", () => { SetupWithdrawPeriod(); }));
                            Menu.Add(new MenuItem(Menu.Count, "Previous menu", () => { CurrentMenu = Menus.Initial; }));
                            Menu.Add(new MenuItem(Menu.Count, "Exit", () => { isContinue = false; }));
                            Menu.DisplayMenuItems();
                        }

                        Menu.SelectMenuItemDialog();
                        Menu.ProcessMenuItem();
                        CheckMenuChange();
                        break;
                }
            }
            
        }

    }
    enum Menus
    {
        Initial,AddDelCar,Options
    }
    class Menu
    {
        private List<MenuItem> ListMenu = new List<MenuItem>();
        private int selectedMenuItem = -1;
        public int Count { get { return ListMenu.Count; } }

        public bool isNeedChange { get; set; } = true;
        public void Add(MenuItem mi)
        {
            ListMenu.Add(mi);
        }
        public void Clear()
        {
            ListMenu.Clear();
        }
        public void DisplayMenuItems()
        {
            Console.WriteLine();
            foreach(var mi in ListMenu)
            {
                mi.Print();
            }
        }
        public void SelectMenuItemDialog()
        {
            Console.WriteLine("\nSelect menu item by menu item number");
            if( !Int32.TryParse(Console.ReadLine(), out selectedMenuItem) )
            {
                selectedMenuItem = -1;
                Console.WriteLine("\nWrong menu item number");
            }
        }
        public void ProcessMenuItem()
        {
            if( (0<= selectedMenuItem)&&(selectedMenuItem< ListMenu.Count) )
            {
                ListMenu[selectedMenuItem]?.Process?.Invoke();
            }
        }
    }
    class MenuItem
    {
        public Action Process { get; private set; }
        private readonly int n_;
        private readonly string text_;
        public MenuItem(int n,string text,Action action)
        {
            n_ = n;
            text_ = text;
            Process = action;
        }
        public void Print()
        {
            Console.WriteLine($"{n_,-5}: {text_,-20}");
        }
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
