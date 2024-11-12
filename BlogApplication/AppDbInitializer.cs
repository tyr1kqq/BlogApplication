using System.Data;
using BlogApplication.Models;

namespace BlogApplication
{
    public class AppDbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            
            if (context.Roles.Any())
            {
                return;   
            }


            var roles = new RoleViewModel[]
            {
                new RoleViewModel { Name = "Admin" },
                new RoleViewModel { Name = "Moderator" },
                new RoleViewModel { Name = "User" }
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            var users = new UserViewModel[]
            {
                new UserViewModel { Username = "admin", Email = "admin@example.com", PasswordHash = "hashed_password_admin" },
                new UserViewModel { Username = "moderator", Email = "moderator@example.com", PasswordHash = "hashed_password_moderator" },
                new UserViewModel { Username = "user1", Email = "user1@example.com", PasswordHash = "hashed_password_user1" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            // Получение идентификаторов пользователей и ролей для их связывания
            var admin = users.First(u => u.Username == "admin");
            var moderator = users.First(u => u.Username == "moderator");
            var user1 = users.First(u => u.Username == "user1");

            var roleAdmin = context.Roles.First(r => r.Name == "Admin");
            var roleModerator = context.Roles.First(r => r.Name == "Moderator");
            var roleUser = context.Roles.First(r => r.Name == "User");

            // Создание связей между пользователями и ролями
            var userRoles = new UserRoleViewModel[]
            {
                new UserRoleViewModel { UserId = admin.UserID, RoleId = roleAdmin.Id },
                new UserRoleViewModel { UserId = moderator.UserID, RoleId = roleModerator.Id },
                new UserRoleViewModel { UserId = user1.UserID, RoleId = roleUser.Id }
            };

            context.UserRoles.AddRange(userRoles);
            context.SaveChanges();
        }
    }
}
