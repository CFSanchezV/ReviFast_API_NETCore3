using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Categoria;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class CategoryTests : GeneralInfo
    {


        [SetUp]
        public void InitCategory()
        {


        }


        [Test]
        public async Task AddaCategory()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newcategory = new CreateCategoriaViewModel()
            {
                Nombre = "A1",
                Descripcion = "Un tipo de vehiculo",
            };

            var controller = new CategoriasController(context);

            var response = await controller.Create(newcategory);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListCategories()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakecategory = new Categoria()
            {
                Nombre = "A1",
                Descripcion = "Un tipo de vehiculo",
            };

            var fakecategory2 = new Categoria()
            {
                Nombre = "A2",
                Descripcion = "Un tipo de vehiculo",
            };



            context.Categorias.Add(fakecategory);
            context.Categorias.Add(fakecategory2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new CategoriasController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }

    }
}