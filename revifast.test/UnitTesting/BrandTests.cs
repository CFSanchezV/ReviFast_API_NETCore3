using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Marca;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class BrandTests : GeneralInfo
    {


        [SetUp]
        public void Init()
        {


        }


        [Test]
        public async Task AddaBrand()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newbrand = new CreateMarcaViewModel()
            {
                Nombre = "Nissan",
            };

            var controller = new MarcasController(context);

            var response = await controller.Create(newbrand);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListBrand()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakebrand = new Marca()
            {
                Nombre = "Nissan",
            };

            var fakebrand2 = new Marca()
            {
                Nombre = "Kia",
            };



            context.Marcas.Add(fakebrand);
            context.Marcas.Add(fakebrand2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new MarcasController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


    }
}
