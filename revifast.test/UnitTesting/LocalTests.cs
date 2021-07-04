using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Local;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class LocalTests : GeneralInfo
    {


        [SetUp]
        public void Init()
        {


        }


        [Test]
        public async Task AddaSchedule()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newlocal = new CreateLocalViewModel()
            {
                Direccion = "Av. La Molina",
                HorarioId = 1,
                EmpresaId = 1,
                DistritoId = 1,
            };

            var controller = new LocalsController(context);

            var response = await controller.Create(newlocal);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListLocal()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakelocal = new Local()
            {
                Direccion = "Av. La Molina",
                HorarioId = 1,
                EmpresaId = 1,
                DistritoId = 1,
            };

            var fakelocal2 = new Local()
            {
                Direccion = "Av. La Molina",
                HorarioId = 1,
                EmpresaId = 1,
                DistritoId = 1,
            };



            context.Locales.Add(fakelocal);
            context.Locales.Add(fakelocal2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new LocalsController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


    }
}


