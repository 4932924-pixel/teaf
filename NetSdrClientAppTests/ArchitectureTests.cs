using NetArchTest.Rules;
using NetSdrClientApp.Networking; // Переконайся, що namespace правильний
using NUnit.Framework;
using System.Reflection;

namespace NetSdrClientAppTests
{
    public class ArchitectureTests
    {
        [Test]
        public void Networking_Should_Not_Depend_On_Program()
        {
            // 1. Arrange: Завантажуємо збірку (assembly), де лежить наш код
            // Ми беремо assembly, в якій знаходиться клас TcpClientWrapper
            var assembly = Assembly.GetAssembly(typeof(TcpClientWrapper));

            // 2. Act: Описуємо правило
            var result = Types.InAssembly(assembly)
                .That()
                .ResideInNamespace("NetSdrClientApp.Networking") // Всі класи в папці Networking
                .ShouldNot()
                .HaveDependencyOn("NetSdrClientApp.Program") // Не повинні використовувати Program.cs
                .GetResult();

            // 3. Assert: Перевіряємо, чи правило виконано
            Assert.IsTrue(result.IsSuccessful, "Architecture violation: Networking depends on Program!");
        }
        
        [Test]
        public void Interfaces_Should_Start_With_I()
        {
            // Додаткове правило: Всі інтерфейси мають починатися з "I"
            var assembly = Assembly.GetAssembly(typeof(TcpClientWrapper));

            var result = Types.InAssembly(assembly)
                .That()
                .AreInterfaces()
                .Should()
                .HaveNameStartingWith("I")
                .GetResult();

            Assert.IsTrue(result.IsSuccessful, "Interfaces must start with 'I'");
        }
    }
}
