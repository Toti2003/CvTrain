using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvTrain {
    class TestTrain {
        public static void Mainx(string[] args) {

            EconomyVagon ew1 = new EconomyVagon(21, 10);

            EconomyVagon ew2 = new EconomyVagon(22, 10);

            Person st1 = new Person("Lenka", "Kozakova");   //
            BusinessVagon bw1 = new BusinessVagon(st1, 11, 20); //

            Person st2 = new Person("Lukas", "Kozak");   //
            BusinessVagon bw2 = new BusinessVagon(st2, 12, 25); //

            NightVagon nw1 = new NightVagon(31, 5, 10);
            NightVagon nw2 = new NightVagon(32, 5, 10);

            Hopper hw1 = new Hopper(41, 100.5);

            Hopper hw2 = new Hopper(42, 110.5);

            Hopper hw4 = new Hopper(44, 110.5);

            Hopper hw5 = new Hopper(45, 110.5);

            Person d1 = new Person("Karel", "Novak");   //
            Engine e1 = new Engine("Diesel");           //
            Locomotive l1 = new Locomotive(d1, e1);     //

            Person d2 = new Person("Pepa", "Parnik");   //
            Engine e2 = new Engine("Parni");            //
            Locomotive l2 = new Locomotive(d2, e2);     //


            Train t1 = new Train(l1);
            t1.ConnectWagon(ew1);
            t1.ConnectWagon(ew2);
            t1.ConnectWagon(ew2);
            t1.ConnectWagon(bw1);
            t1.ConnectWagon(nw1);
            t1.ConnectWagon(hw1);
            t1.ConnectWagon(hw1);
            t1.ConnectWagon(hw2);
            t1.ConnectWagon(hw4);
            t1.ConnectWagon(hw5);
            t1.ConnectWagon(hw5);
            Console.WriteLine(t1);
            Console.WriteLine();

            t1.DisconnectWagon(ew1);
            Console.WriteLine(t1);
            Console.WriteLine();

            t1.DisconnectWagon(ew1);
            Console.WriteLine();

            Train t2 = new Train(l2);
            t2.ConnectWagon(ew1);
            t2.ConnectWagon(ew2);
            t2.ConnectWagon(bw2);
            t2.ConnectWagon(nw1);
            t2.ConnectWagon(nw1);
            t2.ConnectWagon(nw2);
            t2.ConnectWagon(hw1);
            t2.ConnectWagon(hw2);
            t2.ConnectWagon(hw4);
            t2.ConnectWagon(hw5);
            Console.WriteLine(t2);
            Console.WriteLine();

            t1.ReserveChair(21, 5);
            t1.ReserveChair(21, 6);
            t1.ReserveChair(22, 7);
            t1.ReserveChair(22, 7);
            t1.ReserveChair(11, 4);
            t1.ReserveChair(11, 5);
            t1.ReserveChair(41, 5);
            t1.ReservedChairs();
            Console.WriteLine();
        }
    }
    interface IWagon {
        void ConnectWagon(IWagon vag);

        void DisconnectWagon(IWagon vag);
    }
    class Train : IWagon {
        Locomotive locomotive;

        List<IWagon> vagony = new List<IWagon>();
        public string s = "";
        public Train() { }
        public Train(Locomotive locomotive) {
            this.locomotive = locomotive;
        }
        public Train(Locomotive locomotive, List<IWagon> vagony) {
            this.locomotive = locomotive;
            this.vagony = vagony;
        }

        public override string ToString() {
            s = locomotive.ToString() + "\n";
            foreach (Vagon v in vagony) {
                s += v.ToString() + "\n";
            }
            return s;
        }

        public void ConnectWagon(IWagon vag) {
            if (vagony.Contains(vag)) {
                Console.WriteLine("Zadany vagon je jiz pripojen.");
            }
            else if (locomotive.Engine.Type is "Parni" && vagony.Count() > 4) {
                Console.WriteLine("Nelze pripojit.");
            }
            else { this.vagony.Add(vag); }

        }
        public void DisconnectWagon(IWagon vag) {
            if (this.vagony.Contains(this))
                this.vagony.Remove(this);
            else
                Console.WriteLine("Zadany vagon neni zarazen ve vlaku.");
        }

        public void ReserveChair(int wagonNumber, int chairNumber) {
            foreach (IWagon wagon in vagony) {
                if (wagon.GetType().Name != "Hopper") {
                    if (((PersonalVagon)wagon).WagonNu == wagonNumber && ((PersonalVagon)wagon).NumberOfChairs >= chairNumber) {
                        foreach (Chair chair in ((PersonalVagon)wagon).chairs) {
                            if (chair.Number == chairNumber && chair.Reserved == true)
                                Console.WriteLine("Neni moznorezervovat sedadlo: " + chairNumber + " ve vagonu: " + ((PersonalVagon)wagon).WagonNu);
                            else if (chair.Number == chairNumber && chair.Reserved == false)
                                chair.Reserved = true;
                        }
                    }
                }
            }
        }

        public void ReservedChairs() {   //dodelat
            foreach (IWagon wagon in vagony) {
                if (wagon.GetType().Name != "Hopper") {
                    //Console.WriteLine((PersonalVagon)wagon+" pokusssss");
                    foreach (Chair chair in ((PersonalVagon)wagon).chairs) {
                        if (chair.Reserved == true)
                            Console.WriteLine("Ve vagonu: " + ((PersonalVagon)wagon).WagonNu + " je rezervovano sedadlo: " + chair.Number);
                    }
                }
            }
        }

    }


    

    class Locomotive {
        private Person driver;
        private Engine engine;

        public Person Driver {
            get { return driver; }
            set { driver = value; }
        }
        public Engine Engine {
            get { return engine; }
            set { engine = value; }
        }

        public Locomotive() { }
        public Locomotive(Person driver, Engine engine) {
            this.engine = engine;
            this.driver = driver;
        }
        public override string ToString() {
            return "Lokomotiva: " + this.engine + " Ridic: " + this.driver;
        }

    }
    class Engine {
        private string type;
        public string Type {
            get { return type; }
            set { type = value; }
        }
        public Engine() { }
        public Engine(string type) {
            this.type = type;
        }
        public override string ToString() {
            return "Druh : " + GetType().Name + "\t";
        }
    }
    class Person {
        private string firstName;
        public string FirstName {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;
        public string LastName {
            get { return lastName; }
            set { lastName = value; }
        }
        public Person(string firstName, string lastName) {
            this.firstName = firstName;
            this.lastName = lastName;

        }
        public override string ToString() {
            return "Jmeno ridice: " + firstName + " " + lastName;
        }
    }
    class Chair {
        private bool nearWindow;
        public bool NearWindow {
            get { return nearWindow; }
            set { nearWindow = value; }
        }
        private int number;
        public int Number {
            get { return number; }
            set { number = value; }
        }
        private bool reserved;
        public bool Reserved {
            get { return reserved; }
            set { reserved = value; }
        }
        public Chair() { }
        public Chair(int number, bool nearWindow, bool reserved) {
            this.number = number;
            this.nearWindow = nearWindow;
            this.reserved = reserved;
        }

    }
    class Bed {
        private bool reserved;
        public bool Reserved {
            get { return reserved; }
            set { reserved = value; }
        }
        private int number;
        public int Number {
            get { return number; }
            set { number = value; }
        }
        public Bed() { }
        public Bed(int number, bool reserved) {
            this.number = number;
            this.reserved = reserved;
        }
    }
    class Door {
        private double height;
        public double Height {
            get { return height; }
            set { height = value; }
        }
        private double width;
        public double Width {
            get { return width; }
            set { width = value; }
        }
    }
    class Vagon : IWagon {
        List<IWagon> vagony = new List<IWagon>();
        public void ConnectWagon(IWagon vag) {
            this.vagony.Add(vag);
        }
        public void DisconnectWagon(IWagon vag) {
            this.vagony.Remove(vag);
        }
        public int[] poleCh;
        //public virtual string ToString() {
        //    return "";
        //}
    }
    class PersonalVagon : Vagon {
        public int wagonNu;
        public int WagonNu {
            get { return wagonNu; }
            set { wagonNu = value; }
        }
        public int numberOfChairs;
        public int NumberOfChairs {
            get { return numberOfChairs; }
            set { numberOfChairs = value; }
        }
        public List<Door> doors = new List<Door>();
        public List<Chair> chairs = new List<Chair>();
        public PersonalVagon() { }

        public PersonalVagon(int wagonNu, int numberOfChairs) {
            this.wagonNu = wagonNu;
            this.numberOfChairs = numberOfChairs;
            for (int i = 1; i < numberOfChairs; i++) {
                chairs.Add(new Chair(i, true, false));
                chairs.Add(new Chair(i + 1, false, false));
                i++;
            }
        }

        public int GetPoleCH(int i) {
            return poleCh[i];

        }




        //public int[] poleCh {
        //    get { return numberOfChairs; }
        //    set { numberOfChairs = value; }
        //}

    }
    class EconomyVagon : PersonalVagon {

        public EconomyVagon() { }
        public EconomyVagon(int wagonNu, int numberOfChairs) : base(wagonNu, numberOfChairs) {

        }
        public override string ToString() {
            return "Druh vagonu: " + GetType().Name + " Pocet sedadel: " + this.NumberOfChairs;
        }

    }
    class BusinessVagon : PersonalVagon {
        private Person steward;
        public Person Steward {
            get { return steward; }
            set { steward = value; }
        }
        public BusinessVagon() { }
        public BusinessVagon(Person steward, int wagonNu, int numberOfChairs) : base(wagonNu, numberOfChairs) {
            this.steward = steward;
        }
        public override string ToString() {
            return "Druh vagonu: " + GetType().Name + " Pocet sedadel: " + this.NumberOfChairs + " Steward: " + this.Steward;
        }

    }
    class NightVagon : PersonalVagon {

        private int numberOfBeds;
        public int NumberOfBeds {
            get { return numberOfBeds; }
            set { numberOfBeds = value; }
        }
        List<Bed> beds = new List<Bed>();
        public NightVagon() { }
        public NightVagon(int wagonNu, int numberOfChairs, int numberOfBeds) : base(wagonNu, numberOfChairs) {    //identifikace
            this.numberOfBeds = numberOfBeds;
            for (int i = 1; i <= numberOfBeds; i++) {
                beds.Add(new Bed(i, false));
            }
        }

        public override string ToString() {
            return "Druh vagonu: " + GetType().Name + " Pocet sedadel: " + this.NumberOfChairs + " Pocet posteli: " + this.NumberOfBeds;
        }

    }
    class Hopper : Vagon {
        private double loadingCapacity;
        public double LoadingCapacity {
            get { return loadingCapacity; }
            set { loadingCapacity = value; }
        }
        public int wagonNu;
        public int WagonNu {
            get { return wagonNu; }
            set { wagonNu = value; }
        }
        public Hopper(int wagonNu, double loadingCapacity) {
            this.wagonNu = wagonNu;
            this.loadingCapacity = loadingCapacity;
        }
        public override string ToString() {
            return "Druh vagonu: " + GetType().Name + " Tonaz: " + LoadingCapacity;
        }

    }
}
