using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sanch.Serialization_XML_JSON
{
    class Program
    {
        static void Main(string[] args)
        {
            //Сериализация позволяет нам преобразовывать объекты класснов в потоковый формат (в файлы) в каком то определенном формате 
            //где будет сохранено все свойства и значения и мы сможем положить на жесткий диск или отправить кому то 
            //Сериализация - процесс автоматизации сохранения (сохрянялки в игре - это сериализация)


            var groups = new List<Group>();
            var students = new List<Student>();
            for (int i = 0; i < 10; i++)
            {
                groups.Add(new Group(i, "Группа " + i));
            }

            for (int i = 0; i < 300; i++)
            {
                var student = new Student(Guid.NewGuid().ToString().Substring(0, 5), i % 100)
                {
                    Group = groups[i % 9]
                };
                students.Add(student);
            }

            #region binFormatter
            //бинарный сериализатор
            var binFormatter = new BinaryFormatter();

            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                //FileMode.OpenOrCreate - создаст фаqл или откроет, его если уже есть
                binFormatter.Serialize(file, groups);
            }

            //десериализование
            using (var file = new FileStream("groups.bin", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                var newGroups = binFormatter.Deserialize(file) as List<Group>; // приведение в нужную нам форму
                if (newGroups != null)
                {
                    foreach (var item in newGroups)
                    {
                        Console.WriteLine(item);
                    }
                }
            }
            #endregion

            Console.WriteLine();

            #region soapFormatter
            //соапсериализация - общепринятое
            var soapFormatter = new SoapFormatter();
            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                //FileMode.OpenOrCreate - создаст фаqл или откроет, его если уже есть
                soapFormatter.Serialize(file, groups.ToArray()); // работает с массивом, а не с коллекцией
            }

            //десериализование
            using (var file = new FileStream("groups.soap", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                var newGroups = soapFormatter.Deserialize(file) as Group[]; // приведение в нужную нам форму
                if (newGroups != null)
                {
                    foreach (var item in newGroups)
                    {
                        Console.WriteLine(item);
                    }
                }
            }

            #endregion

            Console.WriteLine();

            #region xmlFormatter
            ///XML - сериализация
            var xmlFormatter = new XmlSerializer(typeof(List<Group>));//нужно указать тип данных в которую мы будет сериализовывать

            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                //FileMode.OpenOrCreate - создаст фаqл или откроет, его если уже есть
                xmlFormatter.Serialize(file, groups);
            }

            //десериализование
            using (var file = new FileStream("groups.xml", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                var newGroups = xmlFormatter.Deserialize(file) as List<Group>; // приведение в нужную нам форму
                if (newGroups != null)
                {
                    foreach (var item in newGroups)
                    {
                        Console.WriteLine(item);
                    }
                }
            }

            #endregion

            Console.WriteLine();

            #region jsonFormatter
            //JSON - сериализация (совсем другаая сериализация, не такая как у всех выше)
            var jsonFormatter = new DataContractJsonSerializer(typeof(List<Student>));// нужно указать тип в каком формате мы хотим 

            using (var file = new FileStream("students.json", FileMode.Create)) // для безопасной работы с потоками
            {
                //FileMode.OpenOrCreate - создаст фаqл или откроет, его если уже есть
                jsonFormatter.WriteObject(file, students);
            }

            //десериализование
            using (var file = new FileStream("students.json", FileMode.OpenOrCreate)) // для безопасной работы с потоками
            {
                var newStudents = jsonFormatter.ReadObject(file) as List<Student>; // приведение в нужную нам форму
                if (newStudents != null)
                {
                    foreach (var item in newStudents)
                    {
                        Console.WriteLine(item);
                    }
                }
            }


            #endregion

            Console.ReadLine();
        }
    }
}
