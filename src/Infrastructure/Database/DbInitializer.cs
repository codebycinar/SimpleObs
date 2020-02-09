using Core.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Database
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolDbContext context)
        {
            // Örnek data oluşumundan önce DB'nin oluşturulduğundan emin oluyoruz.
            context.Database.EnsureCreated();

            // Db'de öğrenci var mı?
            if (context.Students.Any())
            {
                return;   // Db seed edilmiş
            }

            #region Db Seed
            //Sınıfları oluştur
            var grades = CreateGrades();
            context.AddRangeAsync(grades);
            context.SaveChangesAsync();

            //Dersleri oluştur
            var lessons = CreateLessons();
            context.AddRangeAsync(lessons);
            context.SaveChangesAsync();

            //Sınavları oluştur
            var exams = CreateExams();
            context.AddRangeAsync(exams);
            context.SaveChangesAsync();

            //Öğrencileri oluştur
            var students = CreateStudents();
            context.AddRangeAsync(students);
            context.SaveChangesAsync();

            //Öğrenciye dersleri ata
            var studentLessons = CreateStudentLessons(students, lessons);
            context.AddRangeAsync(studentLessons);
            context.SaveChangesAsync();

            //Öğrenciye sınav ve sonuçları ata
            var studentExams = CreateStudentExams(students, exams);
            context.AddRangeAsync(studentExams);
            context.SaveChangesAsync();
            #endregion

        }

        #region Data Creation Helpers
        private static Grade[] CreateGrades()
        {
            string[] sections = new string[] { "A", "B", "C", "D" };
            var entities = new List<Grade>();
            int id = 1;
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < sections.Length; j++)
                {
                    entities.Add(new Grade { Id = id, Class = i + 1, Section = sections[j] });
                    id++;
                }
            }

            return entities.ToArray();
        }
        private static Lesson[] CreateLessons()
        {
            var entities = new Lesson[]
            {
                new Lesson
                {
                    Id=1,
                    Name="Türkçe"
                },
                new Lesson
                {
                    Id=2,
                    Name="Matematik"
                },
                new Lesson
                {
                    Id=3,
                    Name="Fen Bilimleri"
                }
            };
            return entities;
        }
        private static Exam[] CreateExams()
        {
            /* Ders Id'leri : Türkçe:1 - Matematik:2 - Fen:3 */
            var entities = new Exam[]
            {
               new Exam()
               {
                   Id=1,
                   ExamType=ExamTypes.Yazili,
                   LessonId=1,
                   Name="Türkçe Yazılı - 1"
               },
               new Exam()
               {
                   Id=2,
                   ExamType=ExamTypes.Yazili,
                   LessonId=1,
                   Name="Türkçe Yazılı - 2"
               },
               new Exam()
               {
                   Id=3,
                   ExamType=ExamTypes.Yazili,
                   LessonId=1,
                   Name="Türkçe Yazılı - 3"
               },
               new Exam()
               {
                   Id=4,
                   ExamType=ExamTypes.Sozlu,
                   LessonId=1,
                   Name="Türkçe Sözlü - 1"
               },
               new Exam()
               {
                   Id=5,
                   ExamType=ExamTypes.Yazili,
                   LessonId=2,
                   Name="Matematik Yazılı - 1"
               },
               new Exam()
               {
                   Id=6,
                   ExamType=ExamTypes.Yazili,
                   LessonId=2,
                   Name="Matematik Yazılı - 2"
               },
               new Exam()
               {
                   Id=7,
                   ExamType=ExamTypes.Yazili,
                   LessonId=2,
                   Name="Matematik Yazılı - 3"
               },
               new Exam()
               {
                   Id=8,
                   ExamType=ExamTypes.Sozlu,
                   LessonId=2,
                   Name="Matematik Sözlü - 1"
               },
               new Exam()
               {
                   Id=9,
                   ExamType=ExamTypes.Sozlu,
                   LessonId=2,
                   Name="Matematik Sözlü - 2"
               },
               new Exam()
               {
                   Id=10,
                   ExamType=ExamTypes.Yazili,
                   LessonId=3,
                   Name="Fen Bilimleri Yazılı - 1"
               },
               new Exam()
               {
                   Id=11,
                   ExamType=ExamTypes.Yazili,
                   LessonId=3,
                   Name="Fen Bilimleri Yazılı - 2"
               },
               new Exam()
               {
                   Id=12,
                   ExamType=ExamTypes.Sozlu,
                   LessonId=3,
                   Name="Fen Bilimleri Sözlü - 1"
               },
            };
            return entities;
        }
        private static Student[] CreateStudents()
        {
            var entities = new List<Student>();
            int id = 1;
            for (int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    entities.Add(
                        new Student()
                        {
                            Id = id,
                            ClassId = i + 1,
                            StudentCode = (100 + id).ToString(),
                            FirstName = GetName(),
                            LastName = GetSurname()

                        });
                    id++;
                }

            }
            return entities.ToArray();
        }
        private static StudentLesson[] CreateStudentLessons(Student[] students, Lesson[] lessons)
        {
            var entities = new List<StudentLesson>();
            foreach (var student in students)
            {
                foreach (var lesson in lessons)
                {
                    entities.Add(new StudentLesson
                    {
                        LessonId = lesson.Id,
                        StudentId = student.Id
                    });
                }
            }
            return entities.ToArray();
        }
        private static StudentExam[] CreateStudentExams(Student[] students, Exam[] exams)
        {
            var entities = new List<StudentExam>();
            foreach (var student in students)
            {
                foreach (var exam in exams)
                {
                    entities.Add(new StudentExam
                    {
                        ExamId = exam.Id,
                        StudentId = student.Id,
                        Result = GetResult()
                    });
                }
            }
            return entities.ToArray();
        }

        #endregion

        #region RandomHelpers
        private static string GetName()
        {
            string[] names = new string[]
               {
                "Fatma",
                "Ayşe",
                "Emine",
                "Hatice",
                "Zeynep",
                "Pınar",
                "Işıl",
                "Merve",
                "Mehmet",
                "Mustafa",
                "Ahmet",
                "Ali",
                "Hüseyin",
                "Hikmet",
                "Nazım",
                "Kemal"
               };

            Random rnd = new Random();
            return names[rnd.Next(0, 15)];
        }
        private static string GetSurname()
        {
            string[] surnames = new string[]
            {
                "YILMAZ",
                "KAYA",
                "DEMİR",
                "ÇELİK",
                "ŞAHİN",
                "YILDIZ",
                "YILDIRIM",
                "ÖZTÜRK",
                "AYDIN",
                "ÖZDEMİR",
                "ARSLAN",
                "DOĞAN",
                "KILIÇ",
                "ASLAN",
                "ÇETİN",
                "KARA",
                "KOÇ"
            };

            Random rnd = new Random();
            return surnames[rnd.Next(0, 16)];
        }
        private static decimal GetResult()
        {
            Random random = new Random();
            return random.Next(50, 100);
        }
        #endregion
    }
}