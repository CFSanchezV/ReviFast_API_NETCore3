using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Revifast.Api.Controllers;
using System.Threading.Tasks;
using Revifast.Entities;
using Revifast.Api.Models.Conductor;

namespace revifast.test.UnitTesting
{
    class ConductoresTests : GeneralInfo
    {


        [SetUp]
        public void InitLogin()
        {


        }


        [Test]
        public async Task CreateanAccount()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newconductor = new CreateConductorViewModel()
            {
                Nombre = "Hillary",
                Apellido = "Moscoso",
                Dni = "76378521",
                Direccion = "Av. Los Olivos",
                Celular = "98346982",
                Correo = "Hmoscoso@gmail.com",
            };

            var controller = new ConductoresController(context);

            var response = await controller.Create(newconductor);

            //Assert

            Assert.IsNotNull(response);
        }



        [Test]
        public async Task DeleteAccount()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakeaccount = new Conductor()
            {
                Nombre = "Hillary",
                Apellido = "Moscoso",
                Dni = "76378521",
                Direccion = "Av. Los Olivos",
                Celular = "98346982",
                Correo = "Hmoscoso@gmail.com",
            };

            context.Conductores.Add(fakeaccount);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new ConductoresController(context2);

            var response = await controller.Delete(1);
            var cancel = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(200, cancel.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Conductores.FindAsync(1);
            Assert.IsNull(exist);
        }

    }
}
