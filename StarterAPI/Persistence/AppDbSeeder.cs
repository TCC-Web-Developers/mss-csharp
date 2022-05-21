using Newtonsoft.Json;
using StarterAPI.Entities;

namespace StarterAPI.Persistence
{
    public static class AppDbSeeder
    {
        public static async void CreateMockStudentsIfNotExists(this WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                try
                {

                    var services = serviceScope.ServiceProvider;

                    var context = services.GetRequiredService<ApplicationDbContext>();


                    context.Database.EnsureCreated();
                    if(context.Students.Count() < 900) {
                        CreateMockStudents(context);
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }

        }

        private static void CreateMockStudents(ApplicationDbContext context)
        {
            List<Student> items = null; 



            using (StreamReader r = new StreamReader("MOCK_DATA_STUDENTS.json"))
            {
                string json = r.ReadToEnd();
                items = JsonConvert.DeserializeObject<List<Student>>(json) ?? new List<Student>();
            }

            foreach (var student in items)
            {
                context.Students.Add(student);
                context.SaveChanges();

                string generatedStudentNo
                    = Convert.ToDateTime(student.DateEnrolled).ToString("yyyyMM") + "-" + student.StudentId;

                student.StudentNo = generatedStudentNo;
                context.SaveChanges();
            }

            




        }
    }
}
