using GAE.AIQ.API.Data;
using API.Data.Entities;

using System.Linq;
using System.Threading.Tasks;
using System;

namespace API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Set<User>().Any())
            {
                return;
            }

            Role[] Roles = new Role[]
            {
                new Role{Id=1,Name="Administrador"},
                new Role{Id=2,Name="Editor"},
                new Role{Id=3,Name="Asistente"},
                new Role{Id=4,Name="Visitante"},
            };

            foreach (Role m in Roles)
            {
                context.Set<Role>().Add(m);
            }

            context.SaveChanges();

            User[] users = new User[]
            {
                new User{UserName="admon",FullName="Administrador del Sistema",Age=99,Address="Company",Phone="55555",Email="admin@company.com",RoleId=1},
            };

            foreach (User a in users)
            {
                context.Set<User>().Add(a);
            }

            context.SaveChanges();

            FunctionalCharacteristic[] functionalCharacteristics = new FunctionalCharacteristic[]
            {
                new FunctionalCharacteristic{Id=1,Name="Crear usuario"},
                new FunctionalCharacteristic{Id=2,Name="Eliminar usuario"},
                new FunctionalCharacteristic{Id=3,Name="Editar usuario"},
                new FunctionalCharacteristic{Id=4,Name="Listar usuarios"},
                new FunctionalCharacteristic{Id=5,Name="filtrar usuario"},
            };

            foreach (FunctionalCharacteristic f in functionalCharacteristics)
            {
                context.Set<FunctionalCharacteristic>().Add(f);
            }

            context.SaveChanges();

            FunctionalCharacteristicByRole[] functionalCharacteristicByRoles = new FunctionalCharacteristicByRole[]
            {
                new FunctionalCharacteristicByRole{
                    PublicationDate=DateTime.Now,
                    RoleId=1,
                    FunctionalCharacteristicId=1
                },
                 new FunctionalCharacteristicByRole{
                    PublicationDate=DateTime.Now,
                    RoleId=1,
                    FunctionalCharacteristicId=2
                },
                  new FunctionalCharacteristicByRole{
                    PublicationDate=DateTime.Now,
                    RoleId=1,
                    FunctionalCharacteristicId=3
                },
                   new FunctionalCharacteristicByRole{
                    PublicationDate=DateTime.Now,
                    RoleId=1,
                    FunctionalCharacteristicId=4
                },
                    new FunctionalCharacteristicByRole{
                    PublicationDate=DateTime.Now,
                    RoleId=1,
                    FunctionalCharacteristicId=5
                },
            };

            foreach (FunctionalCharacteristicByRole fcRol in functionalCharacteristicByRoles)
            {
                context.Set<FunctionalCharacteristicByRole>().Add(fcRol);
            }

            context.SaveChanges();

        }
    }
}
