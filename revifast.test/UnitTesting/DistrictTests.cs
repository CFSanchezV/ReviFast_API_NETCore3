using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Distrito;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class DistrictTests : GeneralInfo
    {


        [SetUp]
        public void InitDistrict()
        {


        }


        [Test]
        public async Task AddaDistrict()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newdepartment = new CreateDistritoViewModel()
            {
                Nombre = "Lima",
            };

            var controller = new DistritosController(context);

            var response = await controller.Create(newdepartment);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListDistrict()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakedistrict = new Distrito()
            {
                Nombre = "Lima",
            };

            var fakedistrict2 = new Distrito()
            {
                Nombre = "Surco",
            };



            context.Distritos.Add(fakedistrict);
            context.Distritos.Add(fakedistrict2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new DistritosController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }

    }
}
