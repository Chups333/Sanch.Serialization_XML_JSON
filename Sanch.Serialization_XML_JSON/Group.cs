using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanch.Serialization_XML_JSON
{
    [Serializable] //атрибут для сериализации
    public class Group
    {
        [NonSerialized] // атрибут для рандома внизу . Мы его не хотим брать. и этот атриут для полей и свойств.
        private static readonly Random rnd = new Random(DateTime.Now.Millisecond);

        //private int priveteint = 12345678; приватное поле тоже сериализует
        public int Number { get; set; }
        public string Name { get; set; }

        public Group()
        {
            Number = rnd.Next(1, 10);
            Name = "Группа" + rnd;
        }
        public Group(int number, string name)
        {
            //Проверка входных данных
            Number = number;

            Name = name;

        }

        public override string ToString()
        {
            return Name;
        }
    }
}
