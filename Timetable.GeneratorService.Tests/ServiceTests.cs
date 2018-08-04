using System;
using Xunit;

namespace Timetable.GeneratorService.Tests
{
    public class ServiceTests
    {
        [Fact]
        public void AuditoriumsForExistingDepartment()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            short depId = 41;
            int ActualResult = gs.GetAuditoriumsForDepartment(depId).Count;
            var ExpectedResult = 15;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void AuditoriumsForNonExistingDepartment()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            short depId = 120;
            int ActualResult = gs.GetAuditoriumsForDepartment(depId).Count;
            var ExpectedResult = 0;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void CountNonExistingTeacherPesonalTime()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int teacherId = 3253;
            int ActualResult = gs.CountTeacherPersonalTime(teacherId);
            var ExpectedResult = 0;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void CountTeacherPesonalTime()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int teacherId = 13;
            int ActualResult = gs.CountTeacherPersonalTime(teacherId);
            var ExpectedResult = 4;
            Assert.Equal(ExpectedResult, ActualResult);
        }
        [Fact]
        public void GetCourses()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetCourses().Count;
            var ExpectedResult = 4;
            Assert.Equal(ExpectedResult, ActualResult);
        }
        [Fact]
        public void GetTeachers()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetTeachers().Count;
            var ExpectedResult = 3582;
            Assert.Equal(ExpectedResult, ActualResult);
        }
        [Fact]
        public void GetAuditoriums()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetAuditoriums().Count;
            var ExpectedResult = 561;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void GetGroups()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetGroups().Count;
            var ExpectedResult = 1549;
            Assert.Equal(ExpectedResult, ActualResult);
        }
        [Fact]
        public void GetDepartments()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetDepartments().Count;
            var ExpectedResult = 89;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void GetSubjects()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetSubjectTypes().Count;
            var ExpectedResult = 9;
            Assert.Equal(ExpectedResult, ActualResult);
        }
        [Fact]
        public void GetSubjectTypes()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetSubjectTypes().Count;
            var ExpectedResult = 9;
            Assert.Equal(ExpectedResult, ActualResult);
        }
        [Fact]
        public void GetWeeks()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetWeeks().Count;
            var ExpectedResult = 16;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void GetFaculties()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetFaculties().Count;
            var ExpectedResult = 42;
            Assert.Equal(ExpectedResult, ActualResult);
        }

        [Fact]
        public void GetLoad()
        {
            GeneratorServiceImpl gs = new GeneratorServiceImpl();
            int ActualResult = gs.GetLoad().Count;
            var ExpectedResult = 60;
            Assert.Equal(ExpectedResult, ActualResult);
        }
    }
}
