using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Modelo;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class ModelTests : GeneralInfo
    {


        [SetUp]
        public void InitModel()
        {


        }


        [Test]
        public async Task AddaModel()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newmodel = new CreateModeloViewModel()
            {
                Nombre = "Subaru Legacy",
            };

            var controller = new ModeloesController(context);

            var response = await controller.Create(newmodel);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListModel()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakemodel = new Modelo()
            {
                Nombre = "Subaru Legacy",
            };

            var fakemodel2 = new Modelo()
            {
                Nombre = "SsangYong Tivoli",
            };



            context.Modelos.Add(fakemodel);
            context.Modelos.Add(fakemodel2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new ModeloesController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


    }
}
