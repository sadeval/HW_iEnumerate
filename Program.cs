using System;
using System.Collections;
using System.Collections.Generic;

namespace StudentManagement
{
    class Student
    {
        private string? name;
        private string? patronymic;
        private string? surname;
        private DateTime birthDate;
        private string? address;
        private string? phone;

        private LinkedList<int> marks = new LinkedList<int>();
        private LinkedList<int> courseworks = new LinkedList<int>();
        private LinkedList<int> exams = new LinkedList<int>();
        private double rating;

        public Student() : this("Unknown", "Unknown", "Unknown", DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor without parameters");
        }

        public Student(string name, string patronymic, string surname) : this(name, patronymic, surname, DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor with name, patronymic, surname,");
        }

        public Student(string name, string patronymic, string surname, string address) : this(name, patronymic, surname, DateTime.MinValue, address, "Unknown")
        {
            Console.WriteLine("Constructor with name, surname, patronymic, address");
        }

        public Student(string name, string patronymic, string surname, DateTime birthDate, string address, string phone)
        {
            SetName(name);
            SetPatronymic(patronymic);
            SetSurname(surname);
            SetBirthDate(birthDate);
            SetAddress(address);
            SetPhone(phone);
            Console.WriteLine("Main constructor with all parameters");
        }

        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetPatronymic(string patronymic)
        {
            this.patronymic = patronymic;
        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            this.birthDate = birthDate;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        public void AddMark(int value)
        {
            if (value < 1 || value > 12) return;
            marks.AddLast(value);
            ResetRating();
        }

        public void AddCoursework(int value)
        {
            if (value < 1 || value > 12) return;
            courseworks.AddLast(value);
            ResetRating();
        }

        public void AddExam(int value)
        {
            if (value < 1 || value > 12) return;
            exams.AddLast(value);
            ResetRating();
        }

        public void EditMarks(List<int> newMarks, List<int> newCourseworks, List<int> newExams)
        {
            marks = new LinkedList<int>(newMarks);
            courseworks = new LinkedList<int>(newCourseworks);
            exams = new LinkedList<int>(newExams);
            ResetRating();
        }

        public void PrintStudent()
        {
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Фамилия: {surname}");
            Console.WriteLine($"Отчество: {patronymic}");
            Console.WriteLine($"Дата рождения: {birthDate.ToShortDateString()}");
            Console.WriteLine($"Адрес: {address}");
            Console.WriteLine($"Номер телефона: {phone}");
            Console.Write("Оценки по зачётам: ");
            foreach (var mark in marks)
            {
                Console.Write($"{mark} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по курсовым: ");
            foreach (var coursework in courseworks)
            {
                Console.Write($"{coursework} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по экзаменам: ");
            foreach (var exam in exams)
            {
                Console.Write($"{exam} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Рейтинг оценок: {rating:F1}");
        }

        private void ResetRating()
        {
            int totalGradesCount = marks.Count + courseworks.Count + exams.Count;

            if (totalGradesCount == 0)
            {
                rating = 0;
                return;
            }

            int totalSum = CalculateSum(marks) + CalculateSum(courseworks) + CalculateSum(exams);
            rating = (double)totalSum / totalGradesCount;
        }

        private int CalculateSum(LinkedList<int> list)
        {
            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return sum;
        }

        public double GetRating()
        {
            return rating;
        }

        public string? GetName()
        {
            return name;
        }

        public string? GetSurname()
        {
            return surname;
        }
    }

    class GroupEnumerator : IEnumerator
    {
        private List<Student> students;
        private int position = -1;

        public GroupEnumerator(List<Student> students)
        {
            this.students = students;
        }

        public bool MoveNext()
        {
            position++;
            return (position < students.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return students[position];
            }
        }
    }

    class Group : IEnumerable
    {
        private List<Student> students;
        private string? groupName;
        private string? specialization;
        private int courseNumber;

        public Group()
        {
            students = new List<Student>();
            SetGroupName("Unknown");
            SetSpecialization("Unknown");
            SetCourseNumber(0);
        }

        public Group(string groupName, string specialization, int courseNumber)
        {
            students = new List<Student>();
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }

        public void SetGroupName(string groupName)
        {
            this.groupName = groupName;
        }

        public void SetSpecialization(string specialization)
        {
            this.specialization = specialization;
        }

        public void SetCourseNumber(int courseNumber)
        {
            this.courseNumber = courseNumber;
        }

        public string? GetGroupName()
        {
            return groupName;
        }

        public string? GetSpecialization()
        {
            return specialization;
        }

        public int GetCourseNumber()
        {
            return courseNumber;
        }

        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        public void EditGroup(string groupName, string specialization, int courseNumber)
        {
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }

        public void TransferStudent(Group anotherGroup, Student student)
        {
            if (students.Remove(student))
            {
                anotherGroup.AddStudent(student);
            }
            else
            {
                throw new Exception("Такого студента в группе не существует.");
            }
        }

        public void ExcludeWorstStudent()
        {
            if (students.Count == 0)
            {
                throw new Exception("Нет студента на отчисление.");
            }

            Student worstStudent = students[0];
            foreach (Student student in students)
            {
                if (student.GetRating() < worstStudent.GetRating())
                {
                    worstStudent = student;
                }
            }
            students.Remove(worstStudent);
        }

        public IEnumerator GetEnumerator()
        {
            return new GroupEnumerator(students);
        }

        public void PrintGroup()
        {
            Console.WriteLine($"Название группы: {groupName}");
            Console.WriteLine($"Специализация группы: {specialization}");
            Console.WriteLine($"Номер курса: {courseNumber}");
            Console.WriteLine("Студенты:");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].GetSurname()} {students[i].GetName()}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Group group1 = new Group("Группа 1", "Программирование", 1);
                Student student1 = new Student("Василий", "Алибабаевич", "Пупкин", new DateTime(1995, 02, 06), "ул. Литературная, д. 18", "+380955289873");
                Student student2 = new Student("Катя", "Ивановна", "Пушкарёва", new DateTime(1996, 05, 12), "ул. Пушкина, д. 20", "+380955289874");

                group1.AddStudent(student1);
                group1.AddStudent(student2);

                // Adding marks to students to see the ratings
                student1.AddMark(10);
                student1.AddCoursework(9);
                student1.AddExam(8);

                student2.AddMark(11);
                student2.AddCoursework(10);
                student2.AddExam(9);

                Console.WriteLine("Группа 1:");
                foreach (Student student in group1)
                {
                    student.PrintStudent();
                    Console.WriteLine();
                }

                Group group2 = new Group("Группа 2", "Дизайн", 2);
                group1.TransferStudent(group2, student1);

                Console.WriteLine("После перевода студента:");
                Console.WriteLine("Группа 1:");
                foreach (Student student in group1)
                {
                    student.PrintStudent();
                    Console.WriteLine();
                }
                Console.WriteLine("Группа 2:");
                foreach (Student student in group2)
                {
                    student.PrintStudent();
                    Console.WriteLine();
                }

                group1.ExcludeWorstStudent();
                Console.WriteLine("После отчисления самого неуспевающего студента:");
                foreach (Student student in group1)
                {
                    student.PrintStudent();
                    Console.WriteLine();
                }

                List<int> newMarks = new List<int> { 12, 10, 11 };
                List<int> newCourseworks = new List<int> { 9, 9, 10 };
                List<int> newExams = new List<int> { 10, 8, 9 };
                student2.EditMarks(newMarks, newCourseworks, newExams);

                Console.WriteLine("После редактирования оценок студента:");
                foreach (Student student in group1)
                {
                    student.PrintStudent();
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}

