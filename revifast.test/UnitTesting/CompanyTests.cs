using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Empresa;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class CompanyTests : GeneralInfo
    {


        [SetUp]
        public void InitCompany()
        {


        }


        [Test]
        public async Task AddaCompany()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newcompany = new CreateEmpresaViewModel()
            {
                Nombre = "Farenet",
                Ruc = "123456",
                Telefono = "9852412",
                Correo = "Farenet@gmail.com",
            };

            var controller = new EmpresasController(context);

            var response = await controller.Create(newcompany);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListCompany()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakecompany = new Empresa()
            {
                Nombre = "Farenet",
                Ruc = "123456",
                Telefono = "9852412",
                Correo = "Farenet@gmail.com",
            };

            var fakecompany2 = new Empresa()
            {
                Nombre = "RTP",
                Ruc = "65471",
                Telefono = "9652132",
                Correo = "RTP@gmail.com",
            };



            context.Empresas.Add(fakecompany);
            context.Empresas.Add(fakecompany2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new EmpresasController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


        [Test]
        public async Task DeleteCompany()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakecompany = new Empresa()
            {
                Nombre = "Farenet",
                Ruc = "123456",
                Telefono = "9852412",
                Correo = "Farenet@gmail.com",
            };

            context.Empresas.Add(fakecompany);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new ReservasController(context2);

            var response = await controller.Delete(1);
            var cancel = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(404, cancel.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Reservas.FindAsync(1);
            Assert.IsNull(exist);
        }

    }
}
