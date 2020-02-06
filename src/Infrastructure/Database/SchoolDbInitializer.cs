using Core.Data.Entity;
using Infrastructure.Database;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleObs.Infrastructure.Database
{
    public class SchoolDbInitializer
    {
        public static async Task Initialize(SchoolDbContext context)
        {
            // Örnek data oluşumundan önce DB'nin oluşturulduğundan emin oluyoruz.
            context.Database.EnsureCreated();
            
            // Db'de öğrenci var mı?
            if (context.Students.Any())
            {
                return;   // Db seed edilmiş
            }

            // Sınıfları oluşturuyoruz.
            if (!context.Grades.Any())
                await SeedGrades(context);

            // Öğrencileri oluşturuyoruz.
            if (!context.Students.Any())
                await SeedStudents(context);
        }

        private static async Task SeedGrades(SchoolDbContext context)
        {
            var entities = new Grade[]
            {
                new Grade()
                {
                    GradeName="5A",
                     Section = "",
                },
                  new Grade()
                {
                    GradeName="5B",
                     Section = "",
                },
                    new Grade()
                {
                    GradeName="5C",
                     Section = "",
                },
                      new Grade()
                {
                    GradeName="5D",
                     Section = "",
                }
            };

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        private static async Task SeedStudents(SchoolDbContext context)
        {
            var entities = new Student[]
            {
                new Student()
                {
                    Id = 1,
                     FirstName="A",
                     
                },
                new Student()
                {
                    Id = 2,
                     FirstName="B",

                },
                new Student()
                {
                    Id = 3,
                     FirstName="C",

                },
            };

            await context.AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }
    }
}
