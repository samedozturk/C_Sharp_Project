using System.Diagnostics.Contracts;
using System.Globalization;
class Program
{
    static void Main(string[] args)
    {
        // Sınıflardan nesnler oluşturuyorum
        Oyuncu[] O = new Oyuncu[2];
        O[0] = new();
        O[1] = new();
        Zar zar = new();
        Random random = new Random();
        Console.WriteLine($@"
     ---Zar Oyunu---


");
        Console.Write("1. oyuncu adini giriniz: ");
        O[0].Ad = Console.ReadLine();
        Console.Write("2. oyuncu adini giriniz: ");
        O[1].Ad = Console.ReadLine(); ;
        String respond;
        while (true) // doğru cevap verilmezse tekrar tekrar sormak için döngü kullandım
        {
            Console.Write($"ilk Kim baslasin ({O[0].Ad} / {O[1].Ad} / rastgele): "); // ilk başlayacak oyuncuyu belirliyorum
            respond = Console.ReadLine();
            if (respond == O[0].Ad || respond == O[1].Ad || respond.ToLower() == "rastgele")
            {
                if (respond.ToLower() == "rastgele")
                {
                    respond = O[random.Next(0, 2)].Ad; // oyuncu rastgele belirlemek isterse random ile oyuncuyu rastgele belirliyorum
                }
                break;
            }
            else
            {
                Console.WriteLine($"yanlıs secenek secenekleriniz : ({O[0].Ad} / {O[1].Ad} / rastgele)\n");
            }
        }
        Console.WriteLine($"ilk başlayacak oyuncu : {respond}\n");

        Console.WriteLine("-----------------------------------------------");
        int n;
        n = (respond == O[0].Ad) ? 0 : 1;

        for(int _ = 0; _ < 10; _++)
        {
            while (true) //doğru cevap verilmezse tekrar tekrar sormak için döngü kullandım
            {
                Console.WriteLine($"oyuncu {O[n].Ad} zar atmak icin herhangi bir tusa basin.");
                string u = Console.ReadLine();
                Console.Clear(); // daha güzel olması için konsolu temizledim
                if (u != null && u != "")
                {
                    zar.PuanYaz(O[n], zar.ZarYuzu[zar.ZarAt()]); 
                    Console.WriteLine($"{O[n].Ad}'ın kazandıgı puan: {zar.ZarYuzu[zar.ZarAt()]}\n");
                    n = (n == 0) ? 1 : 0; // sıranın diğer oyuncuya geçmesini sağladım
                    break;
                }
                else
                {
                    Console.WriteLine("gecerli bir tusa basiniz\n");
                }
            }
        }
        Console.Clear();
        string kazanan = (O[0].Puan > O[1].Puan) ? O[0].Ad : (O[0].Puan == O[1].Puan) ? "Beraberlik" : O[1].Ad; // kazananı belirledim
        Console.WriteLine($@"
---Oyun Bitti---
{O[0].Ad} puanı: {O[0].Puan}
{O[1].Ad} puanı: {O[1].Puan}

Kazanan: {kazanan}");
    }
}
class Oyuncu // oyuncunun puanını ve adını tutan bir sınıf
{
    public int Puan = 0;
    public string Ad; 
}

class Zar
{
    public int[] ZarYuzu = new int[6]; // zar yüzeyi oluşturdum
    Random r = new();
    public Zar() // yapıcı metod ile oluşturduğum zar yüzeyine 1-25 arasında rastgele değerler verdim. sınıftan bir örnek oluşturulduğunda yeni bir zar yüzeyi oluşacak
    {
        for (int i = 0; i < ZarYuzu.Length; i++)
        {
            ZarYuzu[i] = r.Next(1,26);
        }
    }

    public int ZarAt() // zar atma metodu
    {
        return r.Next(0,6);
    }

    public void PuanYaz(Oyuncu x, int ZarPuan) // oyuncu sınıfında bir nesnesinin puan değişkenine puan yaz ekleyen metod
    {
        x.Puan += ZarPuan;
    }
}