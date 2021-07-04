using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Departamento;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class DepartmentTests : GeneralInfo
    {


        [SetUp]
        public void InitDepartment()
        {


        }


        [Test]
        public async Task AddaDepartment()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newdepartment = new CreateDepartamentoViewModel()
            {
                Nombre = "Lima",
            };

            var controller = new DepartamentosController(context);

            var response = await controller.Create(newdepartment);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListDepartment()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakedepartment = new Departamento()
            {
                Nombre = "Lima",
            };

            var fakedepartment2 = new Departamento()
            {
                Nombre = "Ancash",
            };



            context.Departamento.Add(fakedepartment);
            context.Departamento.Add(fakedepartment2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new DepartamentosController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }

    }
}