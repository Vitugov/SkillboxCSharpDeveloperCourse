using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees
{
    public class Worker : IStoredData
    {
        public string FullName { get; }
        public int HeightInCm { get; }
        public DateOnly BirthDate { get; }
        public string BirthPlace { get; }
        public int Age => GetAge();

        private int GetAge()
        {
            return (DateTime.Now.Year - BirthDate.Year - 1) +
                (((DateTime.Now.Month > BirthDate.Month) ||
                ((DateTime.Now.Month == BirthDate.Month) &&
                (DateTime.Now.Day >= BirthDate.Day))) ? 1 : 0);
        }

        public Worker(string fullName, int heightInCm, DateOnly birthDate, string birthPlace) : base(true)
        {
            FullName = fullName;
            HeightInCm = heightInCm;
            BirthDate = birthDate;
            BirthPlace = birthPlace;
        }

        public static new IStoredData Add()
        {
            Console.WriteLine("Пожалуйста введите данные нового сотрудника:");
            Console.Write("Введите Фамилию Имя Отчество: ");
            var fullName = Console.ReadLine();
            Console.Write("Введите рост в сантиметрах: ");
            var heightInCm = Int32.Parse(Console.ReadLine());
            Console.Write("Введите дату рождения: ");
            var birthDate = DateOnly.Parse(Console.ReadLine());
            Console.Write("Введите место рождения: ");
            var birthPlace = Console.ReadLine();
            Console.WriteLine();
            var result = new Worker(fullName, heightInCm, birthDate, birthPlace);
            return result;
        }

        public static new void Print(List<IStoredData> workers)
        {
            Console.WriteLine("{0, -25} {1, 6} {2, 6} {3, 15} {4, 20}", "Фамилия Имя Отчетсво", "Возраст", "Рост", "Дата рождения", "Место рождения");
            foreach (Worker worker in workers)
            {
                Console.WriteLine("{0, -25} {1, 6} {2, 6} {3, 15} {4, 20}", worker.FullName, worker.Age, worker.HeightInCm, worker.BirthDate, worker.BirthPlace);
            }
        }
    }
}
