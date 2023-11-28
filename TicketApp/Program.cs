using System.Net.Sockets;

namespace TicketApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Route> routeList = new List<Route>() { new Route("Ankara", 100), new Route("Istanbul", 150), new Route("Izmir", 200) };
            List<Ticket> ticketList = new List<Ticket>();
            TicketSystem ticketSystem = new TicketSystem(routeList, ticketList);

            bool menuDongusu = true;

            Console.WriteLine("1. Bilet Satisi");
            Console.WriteLine("2. Bilet Iptali");
            Console.WriteLine("3. Rapor");
            Console.WriteLine("4. Cikis");

            while (menuDongusu)
            {
                Console.Write("Menuden Secimi Yapiniz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        ticketSystem.BiletSatisi();
                        break;

                    case "2":
                        ticketSystem.BiletIptali();
                        break;

                    case "3":
                        ticketSystem.RaporOlustur();
                        break;

                    case "4":
                        Console.WriteLine("Program Kapatiliyor...");
                        menuDongusu = false;
                        break;

                    default:
                        Console.WriteLine("Lutfen 1-4 arasi secim yapin");
                        break;
                }
            }
        }
    }
    class Ticket
    {
        public string CustomerName { get; set; }
        public string RouteName { get; set; }
        public int Age { get; set; }
        public double Price { get; set; }

        public Ticket(string customerName, string routeName, int age)
        {
            CustomerName = customerName;
            RouteName = routeName;
            Age = age;
        }

        public void BasePriceCalculation(double basePrice)
        {
            double discountRate = Age < 18 ? 0.5 : 1.0;
            Price = basePrice * discountRate;
        }
    }
    class Route
    {
        public string RouteName { get; set; }
        public double BasePrice { get; set; }

        public Route(string routeName, double basePrice)
        {
            RouteName = routeName;
            BasePrice = basePrice;
        }
    }

    class TicketSystem
    {
        private List<Route> RouteList { get; set; }
        private List<Ticket> TicketList { get; set; }

        public TicketSystem(List<Route> routeList, List<Ticket> ticketList)
        {
            RouteList = routeList;
            TicketList = ticketList;
        }

        public void BiletSatisi()
        {
            Console.Write("Isim Giriniz: ");
            string customerName = Console.ReadLine();

            Console.WriteLine("Rotalar");
            foreach (var route in RouteList)
            {
                Console.WriteLine($"{route.RouteName} - {route.BasePrice} TL");
            }

            Console.Write("Rota Adini giriniz: ");
            string routeName = Console.ReadLine();

            Console.Write("Yasinizi Giriniz: ");
            int age = int.Parse(Console.ReadLine());

            foreach (var route in RouteList)
            {
                if (routeName == route.RouteName)
                {
                    Console.WriteLine("Rota Bulundu");
                    Ticket ticket = new Ticket(customerName, routeName, age);
                    ticket.BasePriceCalculation(route.BasePrice);
                    TicketList.Add(ticket);
                    Console.WriteLine("Satis Basarili");
                    return;
                }
            }

            Console.WriteLine("Girilen rota adi bulunamadi.");
        }

        public void BiletIptali()
        {
            if (TicketList.Count > 0)
            {
                foreach (var ticket in TicketList)
                {
                    Console.WriteLine($"Biletiniz \nIsim: {ticket.CustomerName} \nGuzergah: {ticket.RouteName} \nUcret: {ticket.Price}");
                }
            }
            else
            {
                Console.WriteLine("Gosterilecek bilet bulunmamaktadir");
            }
            Console.WriteLine("Bileti iptal edilecek kullanicinin adini giriniz: ");
            string ticketOwnerName = Console.ReadLine();
            if (TicketList.Count > 0)
            {
                foreach (var ticket in TicketList.ToList())
                {
                    if (ticket.CustomerName == ticketOwnerName)
                    {
                        TicketList.Remove(ticket);
                        Console.WriteLine("Bilet iptal edildi.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Gosterilecek bilet bulunmamaktadir");
            }
        }

        public void RaporOlustur()
        {
            if (TicketList.Count > 0)
            {
                double totalPrice = 0;
                foreach (var ticket in TicketList)
                {
                    Console.WriteLine($"Biletiniz \nIsim: {ticket.CustomerName} \nGuzergah: {ticket.RouteName} \nUcret: {ticket.Price}");
                    totalPrice += ticket.Price;
                }
                Console.WriteLine($"Toplam Fiyat: {totalPrice} TL");
            }
            else
            {
                Console.WriteLine("Gosterilecek bilet bulunmamaktadir");
            }
        }
    }
}
