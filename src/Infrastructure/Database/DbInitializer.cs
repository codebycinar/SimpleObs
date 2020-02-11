using Core.Data.Entity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Database
{
    public static class DbInitializer
    {
        public static void Initialize(SchoolDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
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

            //Öğrenciye sınav ve sonuçları ata
            var studentExams = CreateStudentExams(students, exams);
            context.AddRangeAsync(studentExams);
            context.SaveChangesAsync();

            //Öğrenciye ders sonuçlarını ata
            var studentLessons = CreateStudentLessons(students, lessons);
            context.AddRangeAsync(studentLessons);
            context.SaveChangesAsync();

            var gradeLessons = CreateGradeLessons(grades, lessons, studentLessons);
            context.AddRangeAsync(gradeLessons);
            context.SaveChangesAsync();

            CreateRoles(roleManager);
            CreateLogins(userManager, context.Students.ToList());
            #endregion

        }

        #region Data Creation Helpers
        private static Grade[] CreateGrades()
        {
            string[] sections = new string[] { "A", "B", "C", "D" };
            var entities = new List<Grade>();
            int id = 1;
            for (int i = 0; i < 4; i++)
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
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    entities.Add(
                        new Student()
                        {
                            Id = id,
                            GradeId= i + 1,
                            StudentCode = (100 + id).ToString(),
                            FirstName = GetName(),
                            LastName = GetSurname()

                        });
                    id++;

                }

            }
            return entities.ToArray();
        }
        private static StudentExam[] CreateStudentExams(Student[] students, Exam[] exams)
        {
            var entities = new List<StudentExam>();
            foreach (var student in students)
            {
                student.StudentExams = new List<StudentExam>();
                foreach (var exam in exams)
                {
                    var stExam = new StudentExam
                    {
                        ExamId = exam.Id,
                        StudentId = student.Id,
                        Result = GetResult()
                    };
                    entities.Add(stExam);
                    student.StudentExams.Add(stExam);
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
                    var lessonExams = student.StudentExams.Where(x => x.Exam.LessonId.Equals(lesson.Id));
                    var writtenExams = lessonExams.Where(x => x.Exam.ExamType == ExamTypes.Yazili).ToList();
                    var speechExams = lessonExams.Where(x => x.Exam.ExamType == ExamTypes.Sozlu);
                    var result = (writtenExams.Average(x => x.Result) + speechExams.Sum(x => x.Result)) / (1 + speechExams.Count());
                    
                    entities.Add(new StudentLesson
                    {
                        LessonId = lesson.Id,
                        StudentId = student.Id,
                        Result = result
                    });
                }

            }
            return entities.ToArray();
        }
        private static GradeLesson[] CreateGradeLessons(Grade[] grades, Lesson[] lessons, StudentLesson[] lessonResults)
        {
            var entities = new List<GradeLesson>();
            foreach (var grade in grades)
            {
                foreach (var lesson in lessons)
                {
                    var results = lessonResults.Where(x => x.Student.GradeId.Equals(grade.Id) && x.LessonId.Equals(lesson.Id));
                    entities.Add(new GradeLesson
                    {
                        GradeId = grade.Id,
                        LessonId = lesson.Id,
                        Average = results.Average(x => x.Result)
                    });
                }
            }
            return entities.ToArray();
        }
        private static void CreateLogins(UserManager<ApplicationUser> userManager, List<Student> students)
        {
            foreach (var student in students)
            {
                try
                {
                    var studentUser = new ApplicationUser
                    {
                        StudentId = student.Id,
                        Email = $"abc{student.Id.ToString()}@school.edu.tr",
                        EmailConfirmed = true,
                        NormalizedUserName = $"abc{student.Id.ToString()}",
                        PhoneNumber = "212 111 1111",
                        UserName = $"abc{student.Id.ToString()}",
                    };

                    if (userManager.FindByEmailAsync(studentUser.Email).Result == null)
                    {
                        IdentityResult result = userManager.CreateAsync(studentUser, "123456").Result;

                        if (result.Succeeded)
                        {
                            userManager.AddToRoleAsync(studentUser, "Student").Wait();
                        }
                    }
                }
                catch (Exception ex) 
                {

                    throw;
                }
               
            }
            try
            {
                if (userManager.FindByEmailAsync("admin@school.edu.tr").Result == null)
                {
                    var adminUser = new ApplicationUser
                    {
                        Email = $"admin@school.edu.tr",
                        EmailConfirmed = true,
                        NormalizedUserName = $"admin",
                        PhoneNumber = "212 111 1111",
                        UserName = $"admin"
                    };

                    IdentityResult result = userManager.CreateAsync(adminUser, "123456").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
      
        }
        private static void CreateRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("Student").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Student";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
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