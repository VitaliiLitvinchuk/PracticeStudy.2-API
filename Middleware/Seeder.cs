using Core.Entities.Identity;
using Core;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Middleware.Managers.Interfaces;
using Core.ViewModels.Machine;
using Core.Entities.Machine;
using Middleware.Managers;
using Core.Helpers;

namespace Middleware
{
    public static class Seeder
    {
        public static async Task SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            try
            {
                //logger.LogInformation("Database initialization success");
                var context = scope.ServiceProvider.GetRequiredService<PracticeDBContext>();
                context.Database.Migrate();

                await InitRolesAndUsers(scope);

                await InitProperties(scope);
                await InitBrands(scope);
                await InitYears(scope);
                await InitCars(scope);
                await InitPhotos(scope);
                await InitCharacteristic(scope);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n\n\n\n{ex.Message}\n\n\n\n");
                //logger.LogError("Problem seed database " + ex.Message);
            }
        }

        private static async Task InitCharacteristic(IServiceScope scope)
        {
            var carManager = scope.ServiceProvider.GetRequiredService<IManager<CarViewModel, Car, Guid>>();
            var propertyManager = scope.ServiceProvider.GetRequiredService<IManager<PropertyViewModel, Property, string>>();
            var characteristicManager = scope.ServiceProvider.GetRequiredService<IManager<CharacteristicViewModel, Characteristic, Guid>>();

            if (!(await characteristicManager.Get()).Any())
                await characteristicManager.AddEntities([
                        new CharacteristicViewModel { Value = "2.31", CarId = (await carManager.Get()).ToList()[0].Id, PropertyName = (await propertyManager.Get()).ToList()[Random.Shared.Next(0, (await propertyManager.Get()).Count())].Name},
                        new CharacteristicViewModel { Value = "4.31", CarId = (await carManager.Get()).ToList()[0].Id, PropertyName = (await propertyManager.Get()).ToList()[Random.Shared.Next(0, (await propertyManager.Get()).Count())].Name},
                        new CharacteristicViewModel { Value = "12.5", CarId = (await carManager.Get()).ToList()[0].Id, PropertyName = (await propertyManager.Get()).ToList()[Random.Shared.Next(0, (await propertyManager.Get()).Count())].Name},
                        new CharacteristicViewModel { Value = "232.123", CarId = (await carManager.Get()).ToList()[0].Id, PropertyName = (await propertyManager.Get()).ToList()[Random.Shared.Next(0, (await propertyManager.Get()).Count())].Name},
                    ]);
        }

        private static async Task InitPhotos(IServiceScope scope)
        {

        }

        private static async Task InitCars(IServiceScope scope)
        {
            var carManager = scope.ServiceProvider.GetRequiredService<IManager<CarViewModel, Car, Guid>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (!(await carManager.Get()).Any())
            {
                await carManager.AddEntities([
                    new CarViewModel { Year = new() { YearOfManufacture = 2020 }, Name = "Some", Brand = new() { Name = "Mercedes" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 2021 }, Name = "Some1", Brand = new() { Name = "Audi" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 2019 }, Name = "Some2", Brand = new() { Name = "Mazda" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 2000 }, Name = "Some3", Brand = new() { Name = "BMW" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 2120 }, Name = "Some4", Brand = new() { Name = "Ford" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 20421 }, Name = "Some5", Brand = new() { Name = "Nissan" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 202412 }, Name = "Some6", Brand = new() { Name = "Jeep" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                    new CarViewModel { Year = new() { YearOfManufacture = 2010 }, Name = "Some7", Brand = new() { Name = "Mercedes" }, UserId = (await userManager.Users.FirstAsync(x => x.Email == "qwerty@qwe.rty")).Id },
                ]);
            }

            await carManager.Update(new CarViewModel() { Year = new() { YearOfManufacture = 2010 }, Brand = new() { Name = "Mercedes" } }, Guid.Parse("9c1e1b14-72c8-4abb-9491-6a67cc00645e"));
        }
        private static async Task InitYears(IServiceScope scope)
        {
            var yearManager = scope.ServiceProvider.GetRequiredService<IManager<CarYearViewModel, CarYear, int>>()
                as SmallManager<CarYearViewModel, CarYear, int> ?? throw new Exception("Something bad with manager (Seeder, InitYears)");

            yearManager.Key = "YearOfManufacture";

            if (!(await yearManager.Get()).Any())
                await yearManager.AddEntities(Enumerable.Range(1900, DateTime.Now.Year - 1900 + 1).Select(x => new CarYearViewModel { YearOfManufacture = x }).ToArray());

        }
        private static async Task InitBrands(IServiceScope scope)
        {
            var brandManager = scope.ServiceProvider.GetRequiredService<IManager<CarBrandViewModel, CarBrand, string>>();

            if (!(await brandManager.Get()).Any())
                await brandManager.AddEntities([
                    new CarBrandViewModel { Name = "Toyota" },
                    new CarBrandViewModel { Name = "Ford" },
                    new CarBrandViewModel { Name = "Chevrolet" },
                    new CarBrandViewModel { Name = "Honda" },
                    new CarBrandViewModel { Name = "Nissan" },
                    new CarBrandViewModel { Name = "Jeep" },
                    new CarBrandViewModel { Name = "Hyundai" },
                    new CarBrandViewModel { Name = "Kia" },
                    new CarBrandViewModel { Name = "Subaru" },
                    new CarBrandViewModel { Name = "BMW" },
                    new CarBrandViewModel { Name = "Mazda" },
                    new CarBrandViewModel { Name = "Mercedes" },
                    new CarBrandViewModel { Name = "Audi" },
                ]);
        }

        private static async Task InitProperties(IServiceScope scope)
        {
            var propertyManager = scope.ServiceProvider.GetRequiredService<IManager<PropertyViewModel, Property, string>>();

            if (!(await propertyManager.Get()).Any())
                await propertyManager.AddEntities([
                    new PropertyViewModel { Name = "Об'єм двигуна", Description = "Літри" },
                    new PropertyViewModel { Name = "Кількість циліндрів", Description = "" },
                    new PropertyViewModel { Name = "Потужність", Description = "Кінські сили" },
                    new PropertyViewModel { Name = "Обертовий момент", Description = "Ньютон-метри" },
                    new PropertyViewModel { Name = "Кількість передач", Description = "" },
                    new PropertyViewModel { Name = "Регульований кут повороту", Description = "" },
                    new PropertyViewModel { Name = "Кількість вихлопних труб", Description = "" },
                    new PropertyViewModel { Name = "Наявність каталізатора", Description = "" },
                    new PropertyViewModel { Name = "Тип пального", Description = "Бензин, дизель, електрика, гібрид тощо" },
                    new PropertyViewModel { Name = "Тип коробки передач", Description = "Механічна, автоматична, роботизована" },
                    new PropertyViewModel { Name = "Тип приводу", Description = "Передній, задній, повний" },
                    new PropertyViewModel { Name = "Тип підвіски", Description = "Незалежна, торсіон, пружини" },
                    new PropertyViewModel { Name = "Тип гальм", Description = "Дискові, барабанні" },
                    new PropertyViewModel { Name = "Антиблокувальна система", Description = "ABS" },
                    new PropertyViewModel { Name = "Електронна система розподілу гальмівної сили", Description = "EBD" },
                    new PropertyViewModel { Name = "Тип системи керування", Description = "Гідроелектрична, електромеханічна" },
                    new PropertyViewModel { Name = "Каталізатор", Description = "" },
                    new PropertyViewModel { Name = "Тип системи впуску повітря", Description = "Турбонаддув, компресор" },
                    new PropertyViewModel { Name = "Напруга акумулятора", Description = "" },
                    new PropertyViewModel { Name = "Потужність генератора", Description = "" },
                    new PropertyViewModel { Name = "Тип диференціалу", Description = "Відкритий, LSD, блокування" },
                    new PropertyViewModel { Name = "Передаткове число", Description = "" },
                    new PropertyViewModel { Name = "Тип рейки керма", Description = "Зубчаста, кулькова" },
                    new PropertyViewModel { Name = "Варіаторне кермо", Description = "" }
                ]);
        }

        private static async Task InitRolesAndUsers(IServiceScope scope)
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            if (!roleManager.Roles.Any())
            {
                //var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                var result = await roleManager.CreateAsync(new Role
                {
                    Name = ENV.Roles.Admin
                });
                //if (result.Succeeded)
                //    logger.LogWarning($"Create role {Roles.Admin}");
                //else
                //    logger.LogError($"Problem crate role {Roles.Admin}");
                result = await roleManager.CreateAsync(new Role
                {
                    Name = ENV.Roles.User
                });

            }
            if (!userManager.Users.Any())
            {
                //var logger = scope.ServiceProvider.GetRequiredService<ILogger<RoleManager<User>>>();
                string email = "qwerty@qwe.rty";
                string username = "qwerty";
                var user = new User
                {
                    Email = email,
                    UserName = username,
                    FirstName = "Qwerty",
                    SecondName = "Qwerty",
                    PhoneNumber = "+38(098)232 34 22",
                    Photo = "1.jpg"
                };
                var result = await userManager.CreateAsync(user, "qwerty");

                if (result.Succeeded)
                {
                    //logger.LogWarning("Create user " + user.UserName);
                    result = await userManager.AddToRoleAsync(user, ENV.Roles.Admin);
                }
                else
                {
                    //logger.LogError("Faild create user " + user.UserName);
                }
            }
        }
    }
}
