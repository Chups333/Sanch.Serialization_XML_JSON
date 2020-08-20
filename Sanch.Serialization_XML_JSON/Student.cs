using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Sanch.Serialization_XML_JSON
{
    [DataContract] // атрибут для сериализации JSON
    public class Student
    {
        [DataMember] // атрибут нужен для поля которого ты хочешь сериализовать, если ты этот атрибут не поставишь - оно не сериализуется
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Group Group { get; set; }

        public Student(string name, int age)
        {
            //Проверка входных параметров
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
