using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;




//Tapşırıq
//Telefon kitabçası yaradın
//Console ekranda menuda 1. Əlavə et, 2. Siyahıya bax, 3. Siyahıda axtar. 4. Sil, 5. Dəyiş göstərilsin
//Kitabçada Ad, Soyad, telefon nömrəsi, Şirkət məlumatları olsun
//Məlumatlar file şəklində saxlanılsın ki, proqram bağlanıb açılanda məlumatlar itməsin
//Bizim məlumatlar çox dəyərlidir, ona görə, məlumatların saxlanılması 2 faylda yazılsın (phone.txt, phonebackup.txt)
//Kodu dinamik yazın ki, istənilən vaxt (növbəti dərslərimizdə) məlumatları faylda yox, DB-də saxlayaq və bunun üçün kodu çox dəyişdirməyək. (FileLog, DbLog nümunələrini yada salın)

namespace phone_Contacts
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> persons = new List<Person>();
            List<Person> persons2 = new List<Person>();

            persons.Add(new Person("Besir", "Nagiyev", 0509932013, " Badam D'Art"));
            persons.Add(new Person("Ulvi", "Ibrahimov", 0519980797, "Socar"));
            persons.Add(new Person("Nurlan", "Huseynov", 0706987361));
            persons.Add(new Person("Kamal", "Mirzeyev", 0557893641));
            persons.Add(new Person("Ayxan", "Enverli", 0125447896));
            persons.Add(new Person("Resul", "Mentor", 0556457931));


            var ContactFile = "contact.txt";

            if (File.Exists(ContactFile))
            {

                File.Delete(ContactFile);
               
            }
            else
            {
                var f = File.Create(ContactFile);
                f.Close();

            }





            foreach (var contact in persons)
            {
                File.AppendAllText(ContactFile, contact.FullInfo() + Environment.NewLine);
            }

            while (true)
            {


                Console.WriteLine();
                Console.WriteLine("                                  PhoneBook                    ");
                Console.WriteLine(" 1. Create");
                Console.WriteLine(" 2. Contacts");
                Console.WriteLine(" 3. Search");
                Console.WriteLine(" 4. Delete");
                Console.WriteLine(" 5. Change");

                Console.WriteLine();






                Console.WriteLine();






                var choose = Console.ReadLine();
                if (choose == 1.ToString())
                {

                    Console.WriteLine("Zehmet olmasa ad, soyad, nomre ve sirketi elave edin");
                    Console.WriteLine();
                    var name = Console.ReadLine();
                    var surname = Console.ReadLine();
                    long number = long.Parse(Console.ReadLine());
                    var company = Console.ReadLine();
                    persons.Add(new Person(name, surname, number, company));
                    File.AppendAllText(ContactFile, $"{name},{surname},{number},{company}" + Environment.NewLine);




                }
                Console.WriteLine();





                if (choose == 2.ToString())
                {
                    var myArray = new StreamReader(ContactFile);
                    Console.WriteLine();


                    while (true)
                    {

                        var info = myArray.ReadLine();


                        if (info != null)
                        {
                            var splitInfo = info.Split(',');
                            if (!persons2.Any(p => p.Name.ToLower() == splitInfo[0].ToLower() && p.Surname.ToLower() == splitInfo[1].ToLower()))
                                persons2.Add(new Person(splitInfo[0], splitInfo[1], long.Parse(splitInfo[2]), splitInfo[3]));


                        }
                        else
                        {
                            break;
                        }
                        Console.WriteLine();
                    }


                    foreach (var con in persons2)
                    {
                        Console.WriteLine(con.FullInfo());

                    }
                    myArray.Close();
                }

                if (choose == 3.ToString())

                   
                {
                    var findedName = Console.ReadLine().ToLower();


                    var sName = persons2.Where(w => w.Name.ToLower() == findedName|| w.Surname.ToLower()==findedName|| w.Company.ToLower()==findedName);

                    foreach (var item in sName)
                    {
                        Console.WriteLine(item.FullInfo());
                    }
                  
                    
                    
                }

                if (choose == 4.ToString())
               {
                    var findedName = Console.ReadLine().ToLower();

                    var sr2 = new StreamReader(ContactFile);

                   
                    //try
                    //{
                    //    foreach (var item in persons2)
                    //    {
                    //        if (item.Name.Contains(findedName))
                    //        {
                    //            Console.WriteLine(item.FullInfo());

                    //            var t = persons2.IndexOf(item);
                    //            persons2.RemoveAt(t-1);

                    //        }
                    //    }

                    //}
                    //catch (Exception)
                    //{

                    //    Console.WriteLine("essi");
                    //}
                    
                    //var sName = persons2.Where(w => w.Name.ToLower() == findedName);

                    //foreach (var item in sName)
                    //{
                    //    Console.WriteLine(item.FullInfo());
                    //   var f= sName.ToList();
                    //    f.Remove(item);
                    //}



                }




                if (choose==5.ToString())
                {
                    var findedName = Console.ReadLine().ToLower();

                    var sName = persons2.Where(w => w.Name.ToLower() == findedName);

                   

                    foreach (var item in sName)
                    {
                        Console.WriteLine(item.FullInfo());

                       
                    }

                    var changeOb = new StreamWriter(ContactFile);

                    changeOb.WriteLine("");

                    changeOb.Close();
                    

                    var name = Console.ReadLine();
                    var surname = Console.ReadLine();
                    var phone = Console.ReadLine();
                    var company = Console.ReadLine();


                   

                    foreach (var item in sName)
                    {
                        item.Name=name;
                        item.Surname = surname;
                        item.Number = int.Parse(phone);
                        item.Company = company;
                    }

                   

                }
            }
        }
    }

    class Person
    {
        public Person(string na, string sr, long nm, string C = "CodeAcademy")
        {
            Name = na;
            Surname = sr;
            Number = nm;
            Company = C;

        }
        public string Name { get; set; }
        public string Surname { get; set; }
        public long Number { get; set; }
        public string Company { get; set; }

        public string FullInfo()
        {
            return $"{Name},{Surname},{Number},{Company}";
        }
    }
}