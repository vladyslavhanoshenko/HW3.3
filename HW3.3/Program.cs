using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW3._3
{
    class Program
    {
        public static Dictionary<string, List<Student>> StudentByGroup;

        public static Dictionary<string, List<Student>> GetDefaultDictionary()
        {
            var dictionary = new Dictionary<string, List<Student>>();
            Student s1 = new Student("Horbachenko", 1995);
            Student s2 = new Student("Hanoshenko", 2001);
            List<Student> students = new List<Student>();
            students.Add(s1);
            students.Add(s2);
            string key1 = "TV21";
            dictionary.Add(key1, students);
            return dictionary;
        }
        public static void DisplayDictionary(string _groupName)
        {
            StudentByGroup.TryGetValue(_groupName, out var studentList);
            studentList.ForEach(i => Console.Write(i.ToString() + " "));
            Console.WriteLine();
        }
        public static List<Student> AddStudent()
        {
            List<Student> students = new List<Student>();

            while (true)
            {
                
                List<string> surnames = new List<string>();
                List<int> years = new List<int>();

                Console.WriteLine("Please enter students in format 'Surname1:YearOfBirth1, Surname2:YearOfBirth2'");
                string input = Console.ReadLine();
                string[] mixSurnameAndYears = input.Split(new char[] { ':', ',' });

                try
                {

                    for (int i = 0; i < mixSurnameAndYears.Length; i++)
                    {
                        if (i % 2 == 0)
                        {
                            surnames.Add(mixSurnameAndYears[i]);
                        }
                        if (i % 2 != 0)
                        {
                            years.Add(Convert.ToInt32(mixSurnameAndYears[i]));
                        }
                    }
                    for (int i = 0; i < surnames.Count; i++)
                    {
                        students.Add(new Student(surnames[i], years[i]));
                    }
                    Console.WriteLine("Students added");
                    break;
                }
                catch (System.FormatException e)
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
                catch (System.ArgumentOutOfRangeException e1)
                {
                    Console.WriteLine("Wrong input");
                    continue;
                }
            }
            return students;
        }
        public static void AddGroup(string GroupName)
        {
            var students = new List<Student>();
            Console.WriteLine($"There are no group with {GroupName} code. Do you want to add it? (y/n)");
            string _answer = Console.ReadLine();
            if (_answer.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                students = AddStudent();
            }
            else if (_answer == "n" || _answer == "N")
            {
                return;
            }
            StudentByGroup.Add(GroupName, students);
        }
        public static void Run(string GroupName)
        {
            if (StudentByGroup.ContainsKey(GroupName))
            {
                DisplayDictionary(GroupName);
            }
            else
            {
                AddGroup(GroupName);
            }
        }
        static void Main(string[] args)
        {
            StudentByGroup = GetDefaultDictionary();
            while (true)
            {
                Console.WriteLine("Please enter group code. Enter 'exit' to exit:");
                string GroupName = Console.ReadLine();
                if (String.IsNullOrEmpty(GroupName))
                {
                    Console.WriteLine("You've entered an empty value");
                    continue;
                }
                if (GroupName == "exit")
                {
                    Environment.Exit(0);
                }
                Run(GroupName);
            }

            Console.ReadKey();
        }
    }

    class Student
    {
        private int yearOfBirth;
        private string surname;

        public Student() { }

        public Student(string surname, int yearOfBirth)
        {
            this.surname = surname;
            this.yearOfBirth = yearOfBirth;
        }
        
        public new string ToString()
        {
            return surname + ":" + yearOfBirth;
        }
    }
}
